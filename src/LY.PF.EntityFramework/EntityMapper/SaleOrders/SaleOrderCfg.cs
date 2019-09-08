using System.Data.Entity.ModelConfiguration;
namespace LY.PF.SaleOrders.EntityMapper.SaleOrders
{

	/// <summary>
    /// 订单的数据配置文件
    /// </summary>
    public class SaleOrderCfg : EntityTypeConfiguration<SaleOrder>
    {
		/// <summary>
        ///  构造方法[默认链接字符串< see cref = "AbpZeroTemplateDbContext" /> ]
        /// </summary>
		public SaleOrderCfg ()
		{
		            ToTable("SaleOrder");
 

		    // ProductName
			Property(a => a.ProductName).HasMaxLength(64);
            // ProductModel
            Property(a => a.ProductModel).HasMaxLength(64);
            // ProductBrand
            Property(a => a.ProductBrand).HasMaxLength(64);
            // ImageUrl
            Property(a => a.ImageUrl).HasMaxLength(512);
            // Price
            Property(a => a.Price).HasPrecision(18, 2);
			// TotalPrice
			Property(a => a.TotalPrice).HasPrecision(18, 2);
		    // 备注
			Property(a => a.Remark).HasMaxLength(512);
		    // CreateBy
			Property(a => a.CreateBy).HasMaxLength(32);
		    // UpdateBy
			Property(a => a.UpdateBy).HasMaxLength(32);
		}
    }
}