using System.Threading.Tasks;
using Abp.Application.Services;
using LY.PF.Sessions.Dto;

namespace LY.PF.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
