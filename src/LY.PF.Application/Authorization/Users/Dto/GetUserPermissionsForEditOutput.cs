using System.Collections.Generic;
using Abp.Application.Services.Dto;
using LY.PF.Authorization.Permissions.Dto;

namespace LY.PF.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}