using System.Data.Entity.Migrations;

namespace Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
            //Any sees data goes here
            context.SaveChanges();
        }
    }
}

