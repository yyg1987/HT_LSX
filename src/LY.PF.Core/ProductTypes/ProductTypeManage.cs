using Abp.Domain.Repositories;
using Abp.Domain.Services;
namespace LY.PF.ProductTypes
{
    /// <summary>
    /// 销售漏斗业务管理
    /// </summary>
    public class ProductTypeManage : IDomainService
    {
        private readonly IRepository<ProductType, int> _productTypeRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ProductTypeManage(IRepository<ProductType, int> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        //TODO:编写领域业务代码


        /// <summary>
        ///     初始化
        /// </summary>
        private void Init()
        {

        }
    }
}
