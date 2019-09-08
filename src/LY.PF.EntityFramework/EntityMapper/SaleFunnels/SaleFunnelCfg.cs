using System.Data.Entity.ModelConfiguration;
namespace LY.PF.SaleFunnels.EntityMapper.SaleFunnels
{

	/// <summary>
    /// 销售漏斗的数据配置文件
    /// </summary>
    public class SaleFunnelCfg : EntityTypeConfiguration<SaleFunnel>
    {
		/// <summary>
        ///  构造方法[默认链接字符串< see cref = "AbpZeroTemplateDbContext" /> ]
        /// </summary>
		public SaleFunnelCfg ()
		{
		    ToTable("SaleFunnel");
		    // District
			//Property(a => a.District).HasMaxLength(64);
		    // Saler
			Property(a => a.Saler).HasMaxLength(64);
		    // ClientName
			Property(a => a.ClientName).HasMaxLength(64);
		    // Adress
			Property(a => a.Adress).HasMaxLength(512);
		    // ProductName
			Property(a => a.ProductName).HasMaxLength(64);
		    // ProductModel
			Property(a => a.ProductModel).HasMaxLength(64);
		    // Price
			//Property(a => a.Price).HasMaxLength(32);
		    // SumPrice
			//Property(a => a.SumPrice).HasMaxLength(32);
		    // Opportunitie
			Property(a => a.Opportunitie).HasMaxLength(64);
		    // Stage
			Property(a => a.Stage).HasMaxLength(64);
		    // ChanceSum
			Property(a => a.ChanceSum).HasMaxLength(64);
		    // NextAction
			Property(a => a.NextAction).HasMaxLength(512);
		    // RivalA
			Property(a => a.RivalA).HasMaxLength(64);
		    // ProductModelA
			Property(a => a.ProductModelA).HasMaxLength(64);
		    // RivalB
			Property(a => a.RivalB).HasMaxLength(64);
		    // ProductModelB
			Property(a => a.ProductModelB).HasMaxLength(64);
		    // Contact
			Property(a => a.Contact).HasMaxLength(64);
		    // ContactMobile
			Property(a => a.ContactMobile).HasMaxLength(64);
		    // Purchase
			Property(a => a.Purchase).HasMaxLength(64);
		    // PurchaseMobile
			Property(a => a.PurchaseMobile).HasMaxLength(64);
		    // Dean
			Property(a => a.Dean).HasMaxLength(64);
		    // DeanMobile
			Property(a => a.DeanMobile).HasMaxLength(64);
		    // LeadSource
			Property(a => a.LeadSource).HasMaxLength(64);
		    // CreateBy
			Property(a => a.CreateBy).HasMaxLength(32);
		    // UpdateBy
			Property(a => a.UpdateBy).HasMaxLength(32);
		}
    }
}