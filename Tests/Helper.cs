using Npgsql;

namespace Tests;


public class Helper
{
    public static readonly NpgsqlDataSource DataSource;
    
    public static string ClientAppBaseUrl = "http://localhost:5000/";
    
    public static string ApiBaseUrl = "http://localhost:5000/";


    static Helper()
    {
        var envVarKeyName = "pgconn";

        var rawConnectionString = Environment.GetEnvironmentVariable(envVarKeyName);

        if (rawConnectionString == null)
        {
            throw new Exception($@"Test runner enviorment variable has not been set");
        }

        try
        {
            var uri = new Uri(rawConnectionString);
            var properlyFormattedConnectionString = string.Format(
                "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;MaxPoolSize=3;",
                uri.Host,
                uri.AbsolutePath.Trim('/'),
                uri.UserInfo.Split(':')[0],
                uri.UserInfo.Split(':')[1],
                uri.Port > 0 ? uri.Port : 5432);
            DataSource = new NpgsqlDataSourceBuilder(properlyFormattedConnectionString).Build();
            DataSource.OpenConnection().Close();
        }
        catch (Exception e)
        {
            throw new Exception($@"The connection string is found but could not be used.");
        }
        
    }

}