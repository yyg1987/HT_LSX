using Abp.Application.Services;
using LY.PF.Tenants.Dashboard.Dto;

namespace LY.PF.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();
    }
}
