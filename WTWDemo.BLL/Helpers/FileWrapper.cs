using Demo.Bll.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Demo.BLL.Helpers
{
    public class FileWrapper : IFileWrapper
    {
        public Task<string> ReadAllLinesAsync(string path)
        {
            return File.ReadAllTextAsync(path);
        }

        public Task WriteAllTextAsync(string path, string contents)
        {
            return File.WriteAllTextAsync(path, contents);
        }
    }
}
