using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using LY.PF.Authorization;


namespace LY.PF.Indents.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="IndentAppPermissions"/> for all permission names.
    /// </summary>
    public class IndentAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //在这里配置了Indent 的权限。

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var entityNameModel = pages.Children.FirstOrDefault(p => p.Name == AppPermissions.Pages_Administration)
              ?? pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));





            var indent = entityNameModel.CreateChildPermission(IndentAppPermissions.Indent, L("Indent"));
            indent.CreateChildPermission(IndentAppPermissions.Indent_CreateIndent, L("CreateIndent"));
            indent.CreateChildPermission(IndentAppPermissions.Indent_EditIndent, L("EditIndent"));
            indent.CreateChildPermission(IndentAppPermissions.Indent_DeleteIndent, L("DeleteIndent"));







        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PFConsts.LocalizationSourceName);
        }
    }




}