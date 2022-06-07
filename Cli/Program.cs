﻿using System.Reflection;
using Cli.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Cli {
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.Load("Cli"))
                .Build();

            IServiceCollection services = new ServiceCollection();
            var client = await Supabase.Client.InitializeAsync(config["DB:url"], config["DB:key"]);
            services.AddSingleton(client);
            services.AddScoped<GetNotebooks>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var app = new App(serviceProvider);
            await app.Run();
        }
    }
}
