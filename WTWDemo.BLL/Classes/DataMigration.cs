using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Demo.Bll.Enums;
using Demo.Bll.Interfaces;

namespace Demo.Bll.Classes
{
    public class DataMigration : IDataMigration
    {
        public string Name { get; set; }
        private IFileManager _fileManager { get; }
        private ILogger<DataMigration> _logger { get; }
        public DataMigration(ILogger<DataMigration> logger, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _logger = logger;
        }
        public async Task ProcessAsync(IMigrationStrategy statergy)
        {
            _logger.LogInformation($"Migration Strategy --> {statergy.Name}");

            try
            {
                var response = await _fileManager.ReadFileAsync();

                if (response.Status == ServiceResultStatus.Success)
                {
                    await PerformMigration(statergy, response);
                }
                else
                {
                    _logger.LogInformation(string.Format("Unable to process the file. {0}", response.Errors.ToArray()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            _logger.LogInformation("Completed!");
        }
        private async Task PerformMigration(IMigrationStrategy strategy, ServiceResult<string> response)
        {
            strategy.SetPayload(response.Payload);

            var calcResult = await strategy.MigrateAsync();

            if (calcResult.Status == ServiceResultStatus.Success)
            {

                var saveResult = await _fileManager.WriteFileAsync(strategy.GetDataLog());

                if (saveResult.Status != ServiceResultStatus.Success)
                {
                    _logger.LogInformation(string.Format("Unable to save the file. {0}", saveResult.Errors.ToArray()));
                }
            }
            else
            {
                _logger.LogInformation(string.Format("Unable to calculate. {0}", calcResult.Errors.ToArray()));
            }
        }
    }
}
