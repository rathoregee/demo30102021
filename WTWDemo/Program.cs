using System;
using Microsoft.Extensions.DependencyInjection;
namespace Demo
{
    class Program
    {
        public static void Main(String[] args)
        {
            var services = Startup.ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<EntryPoint>().Run(args);
        }
    }
}
