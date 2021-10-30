using Microsoft.Extensions.Logging;
using Moq;
using System;
using Demo.Bll.Classes;
using Demo.Bll.Interfaces;

namespace Demo.Tests.Mock
{
    public class MoqFileWrapper
    {
        public readonly Mock<IFileWrapper> File = new();
        public readonly Mock<ILogger<FileManager>> FileManagerLogger = new();
        public readonly Mock<IJsonDirectory> JsonDirectory = new();
        public readonly Mock<IDataMigration> Migration = new();
        public MoqFileWrapper()
        {
            JsonDirectory.Setup(x => x.BasePath).Returns(string.Empty);
        }

        public void SetUpReadException()
        {
            File.Setup(x => x.ReadAllLinesAsync(It.IsAny<string>()))
                .Throws(new Exception("file read exception"));
        }

        public void SetUpWriteException()
        {
            File.Setup(x => x.WriteAllTextAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception("error on write"));
        }

        public void SetUpReadEmptyFile()
        {
            File.Setup(x => x.ReadAllLinesAsync(It.IsAny<string>()))
                .ReturnsAsync(string.Empty);
        }

        public void SetUpReadResponseValid()
        {
            var data = @"[{'name': 'Amerah Lang', 'address': '5037 Providence Bouled', 'zip': '44109', 'id': '8d322'}]";

            File.Setup(x => x.ReadAllLinesAsync(It.IsAny<string>()))
                .ReturnsAsync(data);
        }

        public void SetUpReadResponseDuplicate()
        {
            var data = @"[{'name': 'Amerah Lang', 'address': '5037 Providence Bouled', 'zip': '44109', 'id': '8d322'},{'name': 'Kamran Khan', 'address': '5037 Providence Bouled', 'zip': '44109', 'id': '8d300'},{'name': 'Amerah Lang', 'address': '5037 Providence Bouled', 'zip': '44109', 'id': '8d319'}]";

            File.Setup(x => x.ReadAllLinesAsync(It.IsAny<string>()))
                .ReturnsAsync(data);
        }

        public void SetUpReadResponseInvalidZipData()
        {
            var data = @"[{'name': 'Amerah Lang', 'address': '5037 Providence Bouled', 'zip': '4', 'id': '8d322'}]";
            File.Setup(x => x.ReadAllLinesAsync(It.IsAny<string>()))
               .ReturnsAsync(data);
        }

        public void SetUpReadResponseMissingJsonRow()
        {
            var data = @"[{'name': 'Amerah Lang', 'address': '5037 Providence Bouled', 'zip': '44109', 'id': '8d322'},{},{'name': 'Amerah Lang1', 'address': '5038 Providence Bouled', 'zip': '44109', 'id': '8d321'}]";
            
            File.Setup(x => x.ReadAllLinesAsync(It.IsAny<string>()))
            .ReturnsAsync(data);
        }

        public void VerifyRead()
        {
            File.Verify(x => x.ReadAllLinesAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
