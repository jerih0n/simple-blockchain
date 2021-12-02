﻿using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.LocalConnectors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blockchain.Node.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appSettings.json", false);
                
                if (args != null) config.AddCommandLine(args);
            })
            .ConfigureServices((hostingContext, services) =>
            {
                services.AddSingleton<NodeConfiguration>();
                services.AddSingleton<BlockchainLocalDataConnector>();
                services.AddHostedService<NodeCLI>();
                
            });

            await builder.RunConsoleAsync();
        }
    }
}
