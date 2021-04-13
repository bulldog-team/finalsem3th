using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Services.BranchService
{
    public class BranchService : IBranchService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public BranchService(IConfiguration config, DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        public async Task<ResponseServiceModel<ICollection<BranchModel>>> GetBranchData()
        {
            var response = new ResponseServiceModel<ICollection<BranchModel>>();
            var branchData = await _context.BranchModels.ToListAsync();
            response.Data = branchData;
            return response;
        }
    }
}