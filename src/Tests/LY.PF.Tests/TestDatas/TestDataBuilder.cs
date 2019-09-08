using EntityFramework.DynamicFilters;
using LY.PF.EntityFramework;

namespace LY.PF.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly PFDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(PFDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
