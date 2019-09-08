using System.Data.Entity.ModelConfiguration;
namespace LY.PF.ProductTypes.EntityMapper.ProductTypes
{

	/// <summary>
    /// 订单的数据配置文件
    /// </summary>
    public class ProductTypeCfg : EntityTypeConfiguration<ProductType>
    {
		/// <summary>
        ///  构造方法[默认链接字符串< see cref = "AbpZeroTemplateDbContext" /> ]
        /// </summary>
		public ProductTypeCfg ()
		{
		            ToTable("ProductType");
 

		    // ProductName
			Property(a => a.ProductTypeName).HasMaxLength(64);
		    // 备注
			Property(a => a.Remark).HasMaxLength(512);
		    // CreateBy
			Property(a => a.CreateBy).HasMaxLength(32);
		    // UpdateBy
			Property(a => a.UpdateBy).HasMaxLength(32);
		}
    }
}