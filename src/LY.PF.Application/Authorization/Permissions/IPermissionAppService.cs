using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LY.PF.Authorization.Permissions.Dto;

namespace LY.PF.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
