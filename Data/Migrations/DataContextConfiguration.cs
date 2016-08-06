using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Data.Migrations
{
    internal sealed class DataContextConfiguration : DbConfiguration
    {
        public DataContextConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}