using System.Data.SqlClient;
using Shouldly;
using Xunit;

namespace LY.PF.Tests.General
{
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=localhost; Database=PF; Trusted_Connection=True;");
            csb["Database"].ShouldBe("PF");
        }
    }
}
