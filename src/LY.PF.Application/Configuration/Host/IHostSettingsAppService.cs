using System.Threading.Tasks;
using Abp.Application.Services;
using LY.PF.Configuration.Host.Dto;

namespace LY.PF.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
