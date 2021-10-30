using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Models
{
    public class StandardCalculationResult
    {
        public int MinYear { get; set; }
        public int DiffYear { get; set; }
        public Dictionary<string, string> ResultRows { get; set; }

    }
}