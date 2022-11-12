using Api.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var configuration = BuildConfiguration();

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services => services.InitializeServices(configuration))
    .Build();

await host.RunAsync();

IConfiguration BuildConfiguration()
{
    var location = Assembly.GetExecutingAssembly().Location;
    var directory = Path.GetDirectoryName(location);

    return new ConfigurationBuilder()
        .SetBasePath(directory)
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
        .AddEnvironmentVariables()
        .Build();
}