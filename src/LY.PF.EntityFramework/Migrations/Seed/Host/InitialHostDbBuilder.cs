using LY.PF.EntityFramework;

namespace LY.PF.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly PFDbContext _context;

        public InitialHostDbBuilder(PFDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
