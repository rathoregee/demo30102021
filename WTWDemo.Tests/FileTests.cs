using Demo.Bll.Classes;
using Demo.Tests.Mock;
using Xunit;

namespace Demo.Tests
{
    public class FileTests
    {
        private readonly MoqFileWrapper _moq = new();

        [Fact]
        public async void ShouldReturnErrorOnReadFileNotExists()
        {
            ///Arrange
            _moq.SetUpReadException();
            var sut = new FileManager(_moq.File.Object, _moq.JsonDirectory.Object, _moq.FileManagerLogger.Object);
            //Act
            var result = await sut.ReadFileAsync();
            //Assert
            Assert.True(result.Status == ServiceResultStatus.ServiceUnavailable);
            Assert.True(result.Errors[0] == "file read exception");
        }

        [Fact]
        public async void ShouldReturnErrorOnWriteFile()
        {
            //Arrange
            _moq.SetUpWriteException();
            var sut = new FileManager(_moq.File.Object, _moq.JsonDirectory.Object, _moq.FileManagerLogger.Object);

            //Act
            var result = await sut.WriteFileAsync("0,1,2,3,4,5");

            //Assert
            Assert.True(result.Status == ServiceResultStatus.ServiceUnavailable);
            Assert.True(result.Errors[0] == "error on write");
        }

        [Fact]
        public async void ShouldReturnData_Ok()
        {
            //Arrange
            _moq.SetUpReadResponseValid();
            var sut = new FileManager(_moq.File.Object, _moq.JsonDirectory.Object, _moq.FileManagerLogger.Object);

            //Act
            var result = await sut.ReadFileAsync();
            _moq.VerifyRead();

            //Assert
            Assert.True(result.Status == ServiceResultStatus.Success);
            Assert.True(result.Payload.Length > 0);
            Assert.True(result.Errors == null);
        }
    }
}
