using System.Configuration;

namespace refactor_me.Infrastructure.Configuration
{
    public static class StaticConfiguration
    {
        public static string DatabaseConnectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}
