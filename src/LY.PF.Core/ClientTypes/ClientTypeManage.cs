using Abp.Domain.Repositories;
using Abp.Domain.Services;
namespace LY.PF.ClientTypes
{
    /// <summary>
    /// 销售漏斗业务管理
    /// </summary>
    public class ClientTypeManage : IDomainService
    {
        private readonly IRepository<ClientType, int> _clientTypeRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ClientTypeManage(IRepository<ClientType, int> clientTypeRepository)
        {
            _clientTypeRepository = clientTypeRepository;
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
