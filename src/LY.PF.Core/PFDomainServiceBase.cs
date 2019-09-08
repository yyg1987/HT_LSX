using Abp.Domain.Services;

namespace LY.PF
{
    public abstract class PFDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected PFDomainServiceBase()
        {
            LocalizationSourceName = PFConsts.LocalizationSourceName;
        }
    }
}
