using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Bll.Interfaces
{
    public interface IFileWrapper
    {
        Task<string> ReadAllLinesAsync(string path);
        Task WriteAllTextAsync(string path, string contents);
    }
}
