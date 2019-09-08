using System.Data.Entity.ModelConfiguration;
namespace LY.PF.Districts.EntityMapper.Districts
{

	/// <summary>
    /// 订单的数据配置文件
    /// </summary>
    public class DistrictCfg : EntityTypeConfiguration<District>
    {
		/// <summary>
        ///  构造方法[默认链接字符串< see cref = "AbpZeroTemplateDbContext" /> ]
        /// </summary>
		public DistrictCfg ()
		{
		            ToTable("District");
 

		    // ProductName
			Property(a => a.DistrictName).HasMaxLength(64);
            // ImageUrl
            Property(a => a.Address).HasMaxLength(512);
		    // 备注
			Property(a => a.Remark).HasMaxLength(512);
		    // CreateBy
			Property(a => a.CreateBy).HasMaxLength(32);
		    // UpdateBy
			Property(a => a.UpdateBy).HasMaxLength(32);
		}
    }
}