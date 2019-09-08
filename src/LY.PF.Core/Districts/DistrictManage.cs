using Abp.Domain.Repositories;
using Abp.Domain.Services;
namespace LY.PF.Districts
{
    /// <summary>
    /// 销售漏斗业务管理
    /// </summary>
    public class DistrictManage : IDomainService
    {
        private readonly IRepository<District, int> _districtRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public DistrictManage(IRepository<District, int> districtRepository)
        {
            _districtRepository = districtRepository;
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
