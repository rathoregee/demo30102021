using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using Demo.Bll.Classes;
using Demo.Bll.Enums;
using Demo.Bll.Interfaces;
using Demo.BLL.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Demo.BLL.Classes
{
    public class StandardJsonProcessor : IStandardJsonProcessor
    {
        private IValidator<StandardPersonData> Validator { get; }

        private IEnumerable<StandardPersonData> _jsonRows;

        private readonly Dictionary<object, List<StandardPersonData>> _dictionary = new();
        public StandardJsonProcessor(IValidator<StandardPersonData> validator)
        {
            Validator = validator;
        }
        public async Task<ServiceResult<StandardReportDto>> ProcessAsync(string text)
        {
            var response = new ServiceResult<StandardReportDto>()
            {
                Status = ServiceResultStatus.UnProcessed,
                Payload = new StandardReportDto()
            };

            if (string.IsNullOrEmpty(text))
                return response;

            ConvertJsonToObjects(text);

            response.Status = ServiceResultStatus.Success;

            await ProcessJsonData();

            response.Payload.Rows = _dictionary;

            return response;
        }

        private void ConvertJsonToObjects(string text)
        {
            _jsonRows = JsonConvert.DeserializeObject<IEnumerable<StandardPersonData>>(text);
        }

        private async Task ProcessJsonData()
        {
            foreach (var curr in _jsonRows)
            {
                var dicKey = new { curr.Name, curr.Address, curr.Zip };

                if (!_dictionary.Keys.Contains(dicKey))
                {
                    _dictionary[dicKey] = new List<StandardPersonData>();
                }

                var person = new StandardPersonData()
                {
                    Id = curr.Id,
                    Name = curr.Name,
                    Address = curr.Address,
                    Zip = curr.Zip
                };

                await VaildateZipCode(person);

                _dictionary[dicKey].Add(person);

                if (_dictionary[dicKey].Count > 1)
                {
                    _dictionary[dicKey].Last().IsDuplicate = true;
                }
            }
        }

        private async Task VaildateZipCode(StandardPersonData standard)
        {
            var validationResult = await Validator.ValidateAsync(standard);

            if (!validationResult.IsValid)
            {
                standard.Errors = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .ToArray();
            }
        }
    }
}
