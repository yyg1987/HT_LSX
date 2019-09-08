using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using LY.PF.Authorization.Roles;
using LY.PF.Authorization.Users;
using LY.PF.Chat;
using LY.PF.Friendships;
using LY.PF.MultiTenancy;
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
    }
}
