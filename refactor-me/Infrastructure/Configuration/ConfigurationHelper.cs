using System;
using System.Configuration;

namespace refactor_me.Infrastructure.Configuration
{
    public static class ConfigurationHelper
    {
        public static string GetConnectionString(string name)
        {
            var value = ConfigurationManager.ConnectionStrings[name];
            if (string.IsNullOrWhiteSpace(value?.ConnectionString))
            {
                throw new InvalidOperationException($"Connection string missing: {name}");
            }
            return value.ConnectionString;
        }
    }
}
