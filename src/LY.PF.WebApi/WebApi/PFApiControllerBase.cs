using Abp.WebApi.Controllers;

namespace LY.PF.WebApi
{
    public abstract class PFApiControllerBase : AbpApiController
    {
        protected PFApiControllerBase()
        {
            LocalizationSourceName = PFConsts.LocalizationSourceName;
        }
    }
}