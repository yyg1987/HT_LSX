using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using LY.PF.Authorization;


namespace LY.PF.Districts.Authorization
{
	/// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="DistrictAppPermissions"/> for all permission names.
    /// </summary>
    public class DistrictAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
					      //在这里配置了District 的权限。

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

              var entityNameModel = pages.Children.FirstOrDefault(p => p.Name == AppPermissions.Pages_Administration) 
                ?? pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));



           

            var district = entityNameModel.CreateChildPermission(DistrictAppPermissions.District , L("District"));
            district.CreateChildPermission(DistrictAppPermissions.District_CreateDistrict, L("CreateDistrict"));
            district.CreateChildPermission(DistrictAppPermissions.District_EditDistrict, L("EditDistrict"));           
            district.CreateChildPermission(DistrictAppPermissions. District_DeleteDistrict, L("DeleteDistrict"));
 

  
            


            
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PFConsts.LocalizationSourceName);
        }
    }




}