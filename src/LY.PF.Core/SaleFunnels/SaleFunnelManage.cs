using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
namespace LY.PF.SaleFunnels
{
    /// <summary>
    /// 销售漏斗业务管理
    /// </summary>
    public class SaleFunnelManage : IDomainService
    {
        private readonly IRepository<SaleFunnel, System.Guid> _saleFunnelRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public SaleFunnelManage(IRepository<SaleFunnel, System.Guid> saleFunnelRepository)
        {
            _saleFunnelRepository = saleFunnelRepository;
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
