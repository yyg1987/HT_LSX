using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using LY.PF.Authorization;


namespace LY.PF.SaleOrders.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="SaleOrderAppPermissions"/> for all permission names.
    /// </summary>
    public class SaleOrderAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //在这里配置了SaleOrder 的权限。

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var entityNameModel = pages.Children.FirstOrDefault(p => p.Name == AppPermissions.Pages_Administration)
              ?? pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));





            var saleOrder = entityNameModel.CreateChildPermission(SaleOrderAppPermissions.SaleOrder, L("SaleOrder"));
            saleOrder.CreateChildPermission(SaleOrderAppPermissions.SaleOrder_CreateSaleOrder, L("CreateSaleOrder"));
            saleOrder.CreateChildPermission(SaleOrderAppPermissions.SaleOrder_EditSaleOrder, L("EditSaleOrder"));
            saleOrder.CreateChildPermission(SaleOrderAppPermissions.SaleOrder_DeleteSaleOrder, L("DeleteSaleOrder"));







        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PFConsts.LocalizationSourceName);
        }
    }




}