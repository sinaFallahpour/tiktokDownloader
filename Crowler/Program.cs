using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Crowler.Controllers;
using Crowler.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ManualConfig()
             .WithOptions(ConfigOptions.DisableOptimizationsValidator)
             .AddValidator(JitOptimizationsValidator.DontFailOnError)
             .AddLogger(ConsoleLogger.Default)
             .AddColumnProvider(DefaultColumnProviders.Instance);
            //var result = BenchmarkRunner.Run<PostService>(config);
            //var result = BenchmarkRunner.Run<HomeController>(config);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
