using System.Data.Entity.ModelConfiguration;
namespace LY.PF.ClientTypes.EntityMapper.ClientTypes
{

	/// <summary>
    /// 订单的数据配置文件
    /// </summary>
    public class ClientTypeCfg : EntityTypeConfiguration<ClientType>
    {
		/// <summary>
        ///  构造方法[默认链接字符串< see cref = "AbpZeroTemplateDbContext" /> ]
        /// </summary>
		public ClientTypeCfg ()
		{
		            ToTable("ClientType");
 

		    // ProductName
			Property(a => a.ClientTypeName).HasMaxLength(128);
		    // 备注
			Property(a => a.Remark).HasMaxLength(512);
		    // CreateBy
			Property(a => a.CreateBy).HasMaxLength(32);
		    // UpdateBy
			Property(a => a.UpdateBy).HasMaxLength(32);
		}
    }
}