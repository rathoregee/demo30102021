
using Demo.Bll.Interfaces;

namespace Demo.BLL.Helpers
{
    public class JsonDirectory : IJsonDirectory
    {
        public JsonDirectory(string filePath, string fileName)
        {
            _filePath = filePath;
            _fileName = fileName;
        }
        public string BasePath { get => _filePath; }
        public string InputFileName { get => _fileName; }
        private string _filePath { get; }
        private string _fileName { get; }
    }
}
