using LY.PF.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.PF.EntityMapper.Users
{
    /// <summary>
    /// 订单的数据配置文件
    /// </summary>
    public class UserCfg : EntityTypeConfiguration<User>
    {
        /// <summary>
        ///  构造方法[默认链接字符串< see cref = "AbpZeroTemplateDbContext" /> ]
        /// </summary>
        public UserCfg()
        {
            ToTable("AbpUsers");
            //忽略
            Ignore(a => a.Name);
            Ignore(a => a.Surname);
            // 可空
            Property(a => a.EmailAddress).IsOptional();
        }
    }

}
