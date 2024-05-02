using Microsoft.Extensions.Configuration;

namespace FPH.Common.Options;

public class ConnectionStringOptions : AppOptions
{
    private const string SectionName = "ConnectionStrings";

    public ConnectionStringOptions(IConfiguration configuration) : base(configuration, SectionName)
    {
    }

    public string DefaultConnection => _configuration.GetSection("DefaultConnections").Value;
}
