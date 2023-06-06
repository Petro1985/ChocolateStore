using System.Data.Common;
using Npgsql;

namespace ChocolateBackEnd.Options;

public static class ConfigurationManagerExt
{
    public static string? GetConnectionString(this ConfigurationManager conf)
    {
        var confSection = conf["DbConnectionString"];
        
        return confSection;
    }
}