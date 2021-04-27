using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.DTO.Package;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Services.PackageService
{
    public class PackageService : IPackageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public PackageService(IConfiguration config, DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private int GetDistance(string original, string target)
        {

            string url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + original + "&destinations=" + target + "&key=AIzaSyBi5dqUpdZT2yRVs013U_sGX3Rar51y-j8";

            WebRequest request = WebRequest.Create(url);

            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    var test = dsResult.ReadXml(reader);
                    var check = dsResult.Tables["distance"].Rows[0]["value"].ToString();
                    return Int32.Parse(check);
                }
            }
        }

        public async Task<ResponseServiceModel<InitPackageResponse>> InitPackage(InitPackageDTO request)
        {
            var response = new ResponseServiceModel<InitPackageResponse>();
            var newPackage = _mapper.Map<PackageModel>(request);
            var deliveryType = await _context.DeliveryTypeModels.FirstOrDefaultAsync(c => c.TypeName == request.DeliveryType);

            if (deliveryType == null)
            {
                response.Message = "Something wrongs!";
                response.Success = false;
                return response;
            };
            if (request.PaymentType == "Cash")
            {
                newPackage.IsPaid = true;
            }

            var userId = GetUserId();
            var user = await _context.UserModels.FirstOrDefaultAsync(c => c.UserId == userId);
            newPackage.User = user;

            var status = await _context.PackageStatusModels.FirstOrDefaultAsync(c => c.Status == "Picked up");
            status.Packages.Add(newPackage);
            newPackage.PackageStatus = status;

            newPackage.DeliveryType = deliveryType;
            deliveryType.Packages.Add(newPackage);
            try
            {
                var distance = GetDistance(request.SenderAddress, request.ReceiveAddress);
                newPackage.Distance = distance;
                newPackage.TotalPrice = distance * deliveryType.UnitPrice + newPackage.Weight * deliveryType.UnitPrice * 2;

            }
            catch (Exception ex)
            {
                response.Message = "Someting wrongs!" + ex.Message;
                response.Success = false;
                return response;
            }


            await _context.PackageModels.AddAsync(newPackage);
            await _context.SaveChangesAsync();
            var savedPackage = await _context.PackageModels.Select(c => new InitPackageResponse
            {
                DateSent = c.DateSent,
                DeliveryType = c.DeliveryType.TypeName,
                Distance = c.Distance,
                IsPaid = c.IsPaid,
                PackageId = c.PackageId,
                Pincode = c.Pincode,
                ReceiveAddress = c.ReceiveAddress,
                ReceiveName = c.ReceiveName,
                SenderAddress = c.SenderAddress,
                SenderName = c.SenderName,
                TotalPrice = c.TotalPrice,
                Weight = c.Weight
            }).FirstOrDefaultAsync(c => c.PackageId == newPackage.PackageId);
            response.Data = savedPackage;
            return response;
        }

        public async Task<ResponseServiceModel<List<GetPackageListDTO>>> GetPackageList()
        {
            var response = new ResponseServiceModel<List<GetPackageListDTO>>();

            var packageList = await _context.PackageModels.OrderBy(c => c.DateSent).Select(c => new GetPackageListDTO
            {
                DateSent = c.DateSent,
                IsPaid = c.IsPaid,
                PackageId = c.PackageId,
                ReceiveName = c.ReceiveName,
                SenderName = c.SenderName,
                TotalPrice = c.TotalPrice
            }).ToListAsync();
            response.Data = packageList;
            return response;
        }

        public async Task<ResponseServiceModel<UserGetPackageDTO>> UserGetPackageInfo(int packageId)
        {
            var response = new ResponseServiceModel<UserGetPackageDTO>();
            var package = await _context.PackageModels.Include(c => c.DeliveryType).Include(c => c.PackageStatus).Select(c => new UserGetPackageDTO
            {
                DateReceived = c.DateReceived,
                CreatedBy = c.User.Username,
                DateSent = c.DateSent,
                DeliveryType = c.DeliveryType.TypeName,
                Distance = c.Distance,
                IsPaid = c.IsPaid,
                Pincode = c.Pincode,
                PackageId = c.PackageId,
                ReceiveAddress = c.ReceiveAddress,
                ReceiveName = c.ReceiveName,
                SenderAddress = c.SenderAddress,
                SenderName = c.SenderName,
                Status = c.PackageStatus.Status,
                TotalPrice = c.TotalPrice,
                Weight = c.Weight

            }).FirstOrDefaultAsync(c => c.PackageId == packageId);

            if (package == null)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            response.Data = package;
            return response;

        }

        public async Task<ResponseServiceModel<List<DeliveryTypeModel>>> GetDeliveryType()
        {
            var response = new ResponseServiceModel<List<DeliveryTypeModel>>();

            response.Data = await _context.DeliveryTypeModels.ToListAsync();
            return response;
        }

        public async Task<ResponseServiceModel<string>> UserUpdatePackageStatus(UserUpdatePackageStatus txtStatus, int packageId)
        {
            var response = new ResponseServiceModel<string>();
            response.Data = "Ok";
            var package = await _context.PackageModels.FirstOrDefaultAsync(c => c.PackageId == packageId);
            var status = await _context.PackageStatusModels.FirstOrDefaultAsync(c => c.Status == txtStatus.txtStatus);
            package.PackageStatus = status;
            package.StatusId = status.StatusId;
            await _context.SaveChangesAsync();
            return response;

        }
    }
}