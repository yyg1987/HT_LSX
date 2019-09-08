using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using LY.PF.Authorization;


namespace LY.PF.ClientTypes.Authorization
{
	/// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="ClientTypeAppPermissions"/> for all permission names.
    /// </summary>
    public class ClientTypeAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
					      //在这里配置了ClientType 的权限。

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

              var entityNameModel = pages.Children.FirstOrDefault(p => p.Name == AppPermissions.Pages_Administration) 
                ?? pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));



           

            var district = entityNameModel.CreateChildPermission(ClientTypeAppPermissions.ClientType , L("ClientType"));
            district.CreateChildPermission(ClientTypeAppPermissions.ClientType_CreateClientType, L("CreateClientType"));
            district.CreateChildPermission(ClientTypeAppPermissions.ClientType_EditClientType, L("EditClientType"));           
            district.CreateChildPermission(ClientTypeAppPermissions. ClientType_DeleteClientType, L("DeleteClientType"));
 

  
            


            
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PFConsts.LocalizationSourceName);
        }
    }




}