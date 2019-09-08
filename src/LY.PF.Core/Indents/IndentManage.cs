using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
namespace LY.PF.Indents
{
    /// <summary>
    /// 订单业务管理
    /// </summary>
    public class IndentManage : IDomainService
    {
        private readonly IRepository<Indent, System.Guid> _indentRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        public IndentManage(IRepository<Indent, System.Guid> indentRepository)
        {
            _indentRepository = indentRepository;
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
