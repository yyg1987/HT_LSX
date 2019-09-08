using System.Threading.Tasks;
using Abp.Application.Services;
using LY.PF.Configuration.Tenants.Dto;

namespace LY.PF.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
