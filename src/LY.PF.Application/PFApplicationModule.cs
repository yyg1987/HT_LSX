using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using LY.PF.Authorization;

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
