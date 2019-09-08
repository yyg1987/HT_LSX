using Abp.Dependency;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Views;

namespace LY.PF.Web.Views
{
    public abstract class PFWebViewPageBase : PFWebViewPageBase<dynamic>
    {
       
    }

    public abstract class PFWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        public IAbpSession AbpSession { get; private set; }
        
        protected PFWebViewPageBase()
        {
            AbpSession = IocManager.Instance.Resolve<IAbpSession>();
            LocalizationSourceName = PFConsts.LocalizationSourceName;
        }
    }
}