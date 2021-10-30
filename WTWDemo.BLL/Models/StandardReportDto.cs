using System.Collections.Generic;

namespace Demo.BLL.Models
{
    public class StandardReportDto
    {
        public Dictionary<object, List<StandardPersonData>> Rows { get; set; }
    }
}
