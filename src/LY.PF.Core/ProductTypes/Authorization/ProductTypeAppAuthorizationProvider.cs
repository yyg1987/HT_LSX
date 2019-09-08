using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using LY.PF.Authorization;


namespace LY.PF.ProductTypes.Authorization
{
	/// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="ProductTypeAppPermissions"/> for all permission names.
    /// </summary>
    public class ProductTypeAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
					      //在这里配置了ProductType 的权限。

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

              var entityNameModel = pages.Children.FirstOrDefault(p => p.Name == AppPermissions.Pages_Administration) 
                ?? pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));



           

            var district = entityNameModel.CreateChildPermission(ProductTypeAppPermissions.ProductType , L("ProductType"));
            district.CreateChildPermission(ProductTypeAppPermissions.ProductType_CreateProductType, L("CreateProductType"));
            district.CreateChildPermission(ProductTypeAppPermissions.ProductType_EditProductType, L("EditProductType"));           
            district.CreateChildPermission(ProductTypeAppPermissions. ProductType_DeleteProductType, L("DeleteProductType"));
 

  
            


            
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PFConsts.LocalizationSourceName);
        }
    }




}