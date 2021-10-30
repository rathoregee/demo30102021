using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Demo.Bll.Interfaces;

namespace Demo
{
    public class EntryPoint
    {
        private IDataMigration _migration { get; }
        private IMigrationStrategy _stratgey { get; }
        private ILogger<EntryPoint> _logger { get; }

        public EntryPoint(IDataMigration migration, IMigrationStrategy stratgey, ILogger<EntryPoint> logger)
        {
            _migration = migration;
            _stratgey = stratgey;
            _logger = logger;
        }
        public void Run(string[] args)
        {
            _logger.LogInformation("Main thread ....");

            _migration.Name = "Standard migration";

            Task.Factory.StartNew(async () => {
                await _migration.ProcessAsync(_stratgey);
            });

            Console.ReadKey();
        }
    }
}
