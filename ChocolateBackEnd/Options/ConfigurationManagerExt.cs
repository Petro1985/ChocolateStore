namespace ChocolateBackEnd.Options;

public static class ConfigurationManagerExt
{
    public static string GetConnectionString(this ConfigurationManager conf)
    {
        var exc = new Exception("connection string exception");

        var confSection = conf.GetSection("DbConnectionString");
        var server = confSection["Server"] ?? throw exc;
        var port = confSection["Port"] ?? throw exc;
        var dataBase = confSection["Database"] ?? throw exc;
        var userId = confSection["User_Id"] ?? throw exc;
        var password = conf["DBPassword"] ?? throw exc;
        
        return $"Server={server};Port={port};DataBase={dataBase};User Id={userId};Password={password}";
    }
}