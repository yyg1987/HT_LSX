using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using LY.PF.Authorization;


namespace LY.PF.SaleFunnels.Authorization
{
	/// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="SaleFunnelAppPermissions"/> for all permission names.
    /// </summary>
    public class SaleFunnelAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
					      //在这里配置了SaleFunnel 的权限。

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

              var entityNameModel = pages.Children.FirstOrDefault(p => p.Name == AppPermissions.Pages_Administration) 
                ?? pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));



           

            var district = entityNameModel.CreateChildPermission(SaleFunnelAppPermissions.SaleFunnel , L("SaleFunnel"));
            district.CreateChildPermission(SaleFunnelAppPermissions.SaleFunnel_CreateSaleFunnel, L("CreateSaleFunnel"));
            district.CreateChildPermission(SaleFunnelAppPermissions.SaleFunnel_EditSaleFunnel, L("EditSaleFunnel"));           
            district.CreateChildPermission(SaleFunnelAppPermissions. SaleFunnel_DeleteSaleFunnel, L("DeleteSaleFunnel"));
 

  
            


            
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PFConsts.LocalizationSourceName);
        }
    }




}