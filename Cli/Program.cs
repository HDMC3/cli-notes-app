using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cli {
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.Load("Cli"))
                .Build();

            IServiceCollection services = new ServiceCollection();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
        }
    }
}
