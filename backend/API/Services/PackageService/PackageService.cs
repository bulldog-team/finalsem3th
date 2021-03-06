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

        // Get UserId in JWT Token
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        // Get distance from Google Maps Matrix API
        private int GetDistance(string original, string target)
        {
            try
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
            catch
            {
                return -1;
            }
        }

        // Create a new package
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
                if (distance == -1) throw new ArgumentException("Error");
                newPackage.Distance = distance;
                newPackage.TotalPrice = Convert.ToInt32((distance * deliveryType.UnitPrice + newPackage.Weight * deliveryType.UnitPrice * 2
                ) / 1000);

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

        // Get all pakages in list
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
                TotalPrice = c.TotalPrice,
                Type = c.DeliveryType.TypeName,
                PackageStatus = c.PackageStatus.Status
            }).ToListAsync();
            response.Data = packageList;
            return response;
        }

        // Get package information in details
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
                Weight = c.Weight,

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

        // Get all delivery type
        public async Task<ResponseServiceModel<List<DeliveryTypeModel>>> GetDeliveryType()
        {
            var response = new ResponseServiceModel<List<DeliveryTypeModel>>();

            response.Data = await _context.DeliveryTypeModels.ToListAsync();
            return response;
        }

        // User update package in details
        public async Task<ResponseServiceModel<string>> UserUpdatePackageStatus(int packageId, UserUpdatePackageStatus request)
        {
            var response = new ResponseServiceModel<string>();
            response.Data = "Ok";
            var package = await _context.PackageModels.FirstOrDefaultAsync(c => c.PackageId == packageId);
            var status = await _context.PackageStatusModels.FirstOrDefaultAsync(c => c.Status == request.txtStatus);

            if (package.IsPaid == false && package.DeliveryType.TypeName != "VPP")
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            if (status.Status == "Received" && package.IsPaid == false)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            package.PackageStatus = status;
            package.StatusId = status.StatusId;

            if (status.Status == "Received")
            {
                package.DateReceived = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return response;
        }

        // Update payment status
        public async Task<ResponseServiceModel<string>> UserUpdatePaymentPackage(int packageId)
        {
            var response = new ResponseServiceModel<string>();
            var package = await _context.PackageModels.FirstOrDefaultAsync(c => c.PackageId == packageId);
            package.IsPaid = true;
            await _context.SaveChangesAsync();
            return response;

        }

        // Search package
        public ResponseServiceModel<ICollection<SearchingPackageResponseDTO>> SearchPackage(PackageResource request)
        {
            var response = new ResponseServiceModel<ICollection<SearchingPackageResponseDTO>>();
            if (request == null)
            {
                response.Success = false;
                response.Message = " Something wrongs!";
                return response;
            }

            if (string.IsNullOrEmpty(request.PackageId) && (string.IsNullOrEmpty(request.Pincode)))
            {
                response.Success = false;
                response.Message = " Something wrongs!";
                return response;
            }

            var collection = _context.PackageModels as IQueryable<PackageModel>;

            if (!string.IsNullOrWhiteSpace(request.PackageId))
            {
                var text = request.PackageId.Trim();
                collection = collection.Where(c => c.PackageId.ToString().Contains(text));
            }
            if (!string.IsNullOrWhiteSpace(request.Pincode))
            {
                var text = request.Pincode.Trim();
                collection = collection.Where(c => c.Pincode.ToString().Contains(text));
            }

            response.Data = collection.Include(c => c.PackageStatus).Include(c => c.DeliveryType).Select(c => new SearchingPackageResponseDTO
            {
                DateSent = c.DateSent,
                Distance = c.Distance,
                DateReceived = c.DateReceived,
                IsPaid = c.IsPaid,
                PackageId = c.PackageId,
                PackageStatus = c.PackageStatus.Status,
                PackageType = c.DeliveryType.TypeName,
                Pincode = c.Pincode,
                ReceiveAddress = c.ReceiveAddress,
                SenderAddress = c.SenderAddress,
                TotalPrice = c.TotalPrice,
                UserId = c.UserId
            }).ToList();
            return response;
        }

        public async Task<ResponseServiceModel<string>> AdminDeletePackage(int packageId)
        {
            var response = new ResponseServiceModel<string>();
            var package = await _context.PackageModels.FirstOrDefaultAsync(c => c.PackageId == packageId);
            if (package != null)
            {
                _context.PackageModels.Remove(package);
                response.Data = "Ok";
                await _context.SaveChangesAsync();
                return response;
            }
            response.Success = false;
            response.Message = "Something wrongs!";
            return response;

        }

        public async Task<ResponseServiceModel<string>> UserUpdatePackageInfo(int packageId, InitPackageDTO request)
        {
            var response = new ResponseServiceModel<string>();
            var package = await _context.PackageModels.Include(c => c.PackageStatus).FirstOrDefaultAsync(c => c.PackageId == packageId);
            var userId = GetUserId();
            var deliveryType = await _context.DeliveryTypeModels.FirstOrDefaultAsync(c => c.TypeName == request.DeliveryType);

            var user = await _context.UserModels.FirstOrDefaultAsync(c => c.UserId == userId);

            if (package.PackageStatus.Status == "Sending" || package.PackageStatus.Status == "Received")
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            if (package != null && user != null && deliveryType != null)
            {
                package.DateSent = request.DateSent;
                package.SenderName = request.SenderName;
                package.SenderAddress = request.SenderAddress;
                package.ReceiveAddress = request.ReceiveAddress;
                package.ReceiveName = request.ReceiveName;
                package.DeliveryType = deliveryType;
                package.Pincode = request.Pincode;
                package.Weight = request.Weight;
                try
                {
                    var distance = GetDistance(request.SenderAddress, request.ReceiveAddress);
                    if (distance == -1) throw new ArgumentException("Error");
                    package.Distance = distance;
                    package.TotalPrice = Convert.ToInt32((distance * deliveryType.UnitPrice + package.Weight * deliveryType.UnitPrice * 2
                    ) / 1000);
                }
                catch (Exception ex)
                {
                    response.Message = "Someting wrongs!" + ex.Message;
                    response.Success = false;
                    return response;
                }

                await _context.SaveChangesAsync();
                response.Data = "Ok";
                return (response);
            }
            response.Success = false;
            response.Message = "Something wrongs!";
            return (response);
        }

        public async Task<ResponseServiceModel<string>> UserUpdateCashPayment(int packageId)
        {
            var response = new ResponseServiceModel<string>();
            var package = await _context.PackageModels.FirstOrDefaultAsync(c => c.PackageId == packageId);
            package.IsPaid = true;

            if (package != null)
            {
                var userId = GetUserId();
                var user = await _context.UserModels.FirstOrDefaultAsync(c => c.UserId == userId);
                package.User = user;
                await _context.SaveChangesAsync();
                response.Data = "Ok";
                return response;
            }
            response.Success = false;
            response.Message = "Something wrongs!";
            return response;

        }
    }
}