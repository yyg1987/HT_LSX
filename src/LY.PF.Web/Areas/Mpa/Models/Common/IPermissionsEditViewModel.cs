using System.Collections.Generic;
using LY.PF.Authorization.Permissions.Dto;

namespace LY.PF.Web.Areas.Mpa.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}