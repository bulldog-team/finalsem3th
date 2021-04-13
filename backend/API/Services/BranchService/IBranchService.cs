using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Services.BranchService
{
    public interface IBranchService
    {
        Task<ResponseServiceModel<ICollection<BranchModel>>> GetBranchData();
    }
}