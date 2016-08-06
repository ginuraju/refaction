using System.Data.Entity;
using Data.Migrations;

namespace Data
{
    public static class DatabaseInitializer
    {
        public static void SetDefault()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>("DefaultConnection"));
        }
    }
}
