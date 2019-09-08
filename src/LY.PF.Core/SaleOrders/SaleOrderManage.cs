using Abp.Domain.Repositories;
using Abp.Domain.Services;
using LY.PF.SaleOrders;
using System;
namespace LY.PF.SaleOrders
{
    /// <summary>
    /// 订单业务管理
    /// </summary>
    public class SaleOrderManage : IDomainService
    {
        private readonly IRepository<SaleOrder, System.Guid> _saleOrderRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public SaleOrderManage(IRepository<SaleOrder, System.Guid> saleOrderRepository)
        {
            _saleOrderRepository = saleOrderRepository;
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
