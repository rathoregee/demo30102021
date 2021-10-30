using System;
using Demo.Bll.Enums;
using Demo.Bll.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Demo.Bll.Classes
{
    public class FileManager : IFileManager
    {
   
        private IFileWrapper _file { get; }
        private IJsonDirectory _dir { get; }
        private ILogger<FileManager> _logger { get; }
        public FileManager(IFileWrapper file, IJsonDirectory dir, ILogger<FileManager> logger)
        {
            _file = file;
            _dir = dir;
            _logger = logger;
        }

        public async Task<ServiceResult<string>> ReadFileAsync()
        {
            var response = new ServiceResult<string>()
            {
                Status = ServiceResultStatus.UnProcessed
            };

            try
            {
                var rows = await _file.ReadAllLinesAsync(string.Format("{0}\\{1}", _dir.BasePath, _dir.InputFileName));
                response.Payload = rows;
                response.Status = ServiceResultStatus.Success;
                return response;
            }
            catch (Exception e)
            {
                response.Errors = new string[] { e.Message };
                response.Status = ServiceResultStatus.ServiceUnavailable;
                _logger.LogError(e.Message);
            }

            return response;
        }

        public async Task<ServiceResult<bool>> WriteFileAsync(string data)
        {
            var response = new ServiceResult<bool>() { Status = ServiceResultStatus.UnProcessed };

            try
            {
                await _file.WriteAllTextAsync(string.Format("{0}\\output.txt", _dir.BasePath), data);

                response.Status = ServiceResultStatus.Success;

            }
            catch (Exception e)
            {
                response.Errors = new string[] { e.Message };

                response.Status = ServiceResultStatus.ServiceUnavailable;

                _logger.LogError(e.Message);
            }

            return response;
        }
    }
}
