using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using LY.PF.Authorization.Roles;
using LY.PF.Authorization.Users;
using LY.PF.Chat;
using LY.PF.ClientTypes;
using LY.PF.ClientTypes.EntityMapper.ClientTypes;
using LY.PF.Districts;
using LY.PF.Districts.EntityMapper.Districts;
using LY.PF.Friendships;
using LY.PF.Indents;
using LY.PF.Indents.EntityMapper.Indents;
using LY.PF.MultiTenancy;
using LY.PF.ProductTypes;
using LY.PF.ProductTypes.EntityMapper.ProductTypes;
using LY.PF.SaleFunnels;
using LY.PF.SaleFunnels.EntityMapper.SaleFunnels;
using LY.PF.SaleOrders;
using LY.PF.SaleOrders.EntityMapper.SaleOrders;
using LY.PF.Storage;

namespace LY.PF.EntityFramework
{
    /* Constructors of this DbContext is important and each one has it's own use case.
     * - Default constructor is used by EF tooling on design time.
     * - constructor(nameOrConnectionString) is used by ABP on runtime.
     * - constructor(existingConnection) is used by unit tests.
     * - constructor(existingConnection,contextOwnsConnection) can be used by ABP if DbContextEfTransactionStrategy is used.
     * See http://www.aspnetboilerplate.com/Pages/Documents/EntityFramework-Integration for more.
     */

    public class PFDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }

        public virtual IDbSet<ChatMessage> ChatMessages { get; set; }


        public virtual IDbSet<SaleFunnel> SaleFunnels { get; set; }
        public virtual IDbSet<Indent> Indents { get; set; }

        public virtual IDbSet<ClientType> ClientTypes { get; set; }

        public virtual IDbSet<District> Districts { get; set; }
        public virtual IDbSet<ProductType> ProductTypes { get; set; }

        public virtual IDbSet<SaleOrder> SaleOrders { get; set; }
        public PFDbContext()
            : base("Default")
        {
            
        }

        public PFDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public PFDbContext(DbConnection existingConnection)
           : base(existingConnection, false)
        {

        }

        public PFDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //添加表的映射
            modelBuilder.Configurations.Add(new SaleFunnelCfg());
            modelBuilder.Configurations.Add(new IndentCfg());
            modelBuilder.Configurations.Add(new ClientTypeCfg());
            modelBuilder.Configurations.Add(new DistrictCfg());
            modelBuilder.Configurations.Add(new ProductTypeCfg());
            modelBuilder.Configurations.Add(new SaleOrderCfg());

        }

    }
}
