using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using LY.PF.Authorization;
using LY.PF.ClientTypes.Authorization;
using LY.PF.Districts.Authorization;
using LY.PF.Indents.Authorization;
using LY.PF.ProductTypes.Authorization;
using LY.PF.SaleFunnels.Authorization;
using LY.PF.SaleOrders.Authorization;

namespace LY.PF
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(typeof(PFCoreModule))]
    public class PFApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            Configuration.Authorization.Providers.Add<SaleFunnelAppAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<IndentAppAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<ClientTypeAppAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<DistrictAppAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<ProductTypeAppAuthorizationProvider>();
            Configuration.Authorization.Providers.Add<SaleOrderAppAuthorizationProvider>();


            //Adding custom AutoMapper mappings
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                CustomDtoMapper.CreateMappings(mapper);
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
