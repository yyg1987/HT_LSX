using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LY.PF.Authorization.Users.Dto;

namespace LY.PF.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
