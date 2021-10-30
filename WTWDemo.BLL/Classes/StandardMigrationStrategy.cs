using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Bll.Classes;
using Demo.Bll.Enums;
using Demo.Bll.Interfaces;
using Demo.BLL.Models;

namespace Demo.BLL.Classes
{
    public class StandardMigrationStrategy : IMigrationStrategy
    {
        private StandardReportDto _payload;
        private string _text;

        private IStandardJsonProcessor _processor { get; }
        public string Name => "Standard Data Migration";

        public int DuplicateCount => (_payload == null || _payload.Rows == null) ? 0 : _payload.Rows.Values.Where(x => x.Any(x => x.IsDuplicate)).Count();
        public int ErrorCount => (_payload == null || _payload.Rows == null) ? 0  : _payload.Rows.Where(x => x.Value.Any(x => x.Errors != null)).Count();
        public int ValidCount => (_payload == null || _payload.Rows == null) ? 0  : _payload.Rows.Where(x => x.Value.Any(x => !x.IsDuplicate && x.Errors == null)).Count();

        public StandardMigrationStrategy(IStandardJsonProcessor processor)
        {
            _processor = processor;
        }
        
        public void SetPayload(string text)
        {
            _text = text;
        }
        
        public async Task<ServiceResult<string>> MigrateAsync()
        {
            var response = await _processor.ProcessAsync(_text);

            if (response.Status != ServiceResultStatus.Success)
            {
                return new ServiceResult<string>() { Status = response.Status, Errors = response.Errors };
            }

            _payload = response.Payload;           

            return new ServiceResult<string>() { Status = ServiceResultStatus.Success };
        }

        public string GetDataLog()
        {
            StringBuilder output = new();

            /*
             Each record has an ID but that should only be used to identify a record, 
             not for validity or duplication testing (eg, two records may be identical but have different IDs).
             
             The output of the program should list the IDs of each invalid or duplicate record, 
             one per line. In the case of duplicates, mark both.
             */

            foreach (var curr in _payload.Rows.Where(x => x.Value.Any(x => x.IsDuplicate || x.Errors != null)))
            {
                if (curr.Value.Count > 0)
                {
                    output.Append($" {curr.Value[0].Id}  \n");
                }
            }
            
            Console.WriteLine(output);

            return output.ToString();
        }
    }
}
