using System.Security.Claims;
using System.Threading.Tasks;
using Шабашка.DAL;
using Шабашка.Domain.Response;
using Шабашка.рф.Models;

namespace Шабашка.Service
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
    }
}
