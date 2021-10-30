using Demo.Bll.Classes;
using Demo.BLL.Classes;
using Demo.Tests.Mock;
using Xunit;

namespace Demo.Tests
{
    public class StandardMigrationTests
    {
        private readonly MoqFileWrapper _moq = new();

        [Theory, StandardAutoMoqData]
        public async void ShouldReturnData_ValidateEmptyFile_Failure(StandardMigrationStrategy statergey)
        {
            //Arrange
            _moq.SetUpReadEmptyFile();
            var sut = new FileManager(_moq.File.Object, _moq.JsonDirectory.Object, _moq.FileManagerLogger.Object);

            var result = await sut.ReadFileAsync();

            statergey.SetPayload(result.Payload);

            //Act
            var response = await statergey.MigrateAsync();

            //Assert
            Assert.True(response.Status == ServiceResultStatus.UnProcessed);
        }

        [Theory, StandardAutoMoqData]
        public async void ShouldValidate_InvalidZipCodeFile(StandardMigrationStrategy statergey)
        {
            //Arrange
            _moq.SetUpReadResponseInvalidZipData();

            var sut = new FileManager(_moq.File.Object, _moq.JsonDirectory.Object, _moq.FileManagerLogger.Object);

            var result = await sut.ReadFileAsync();

            statergey.SetPayload(result.Payload);

            //Act
            var response = await statergey.MigrateAsync();

            var report = statergey.GetDataLog();

            //Assert
            Assert.True(response.Status == ServiceResultStatus.Success);
            Assert.True(statergey.ValidCount == 0);
            Assert.True(statergey.DuplicateCount == 0);
            Assert.True(statergey.ErrorCount == 1);
            Assert.Contains("8d322", report);
        }

        [Theory, StandardAutoMoqData]
        public async void ShouldValidate_EmptyJsonRowInFile(StandardMigrationStrategy statergey)
        {
            //Arrange
            _moq.SetUpReadResponseMissingJsonRow();

            var sut = new FileManager(_moq.File.Object, _moq.JsonDirectory.Object, _moq.FileManagerLogger.Object);

            var result = await sut.ReadFileAsync();

            statergey.SetPayload(result.Payload);

            //Act
            var response = await statergey.MigrateAsync();

            //Assert
            Assert.True(response.Status == ServiceResultStatus.Success);
            Assert.True(statergey.ValidCount == 2);
            Assert.True(statergey.DuplicateCount == 0);
            Assert.True(statergey.ErrorCount == 1);
        }

        [Theory, StandardAutoMoqData]
        public async void ShouldIdentify_DuplicateData(StandardMigrationStrategy statergey)
        {
            //Arrange
            _moq.SetUpReadResponseDuplicate();

            var sut = new FileManager(_moq.File.Object, _moq.JsonDirectory.Object, _moq.FileManagerLogger.Object);

            var result = await sut.ReadFileAsync();

            statergey.SetPayload(result.Payload);

            //Act
            var response = await statergey.MigrateAsync();

            _moq.VerifyRead();

            //Assert
            Assert.True(response.Status == ServiceResultStatus.Success);
            Assert.True(response.Errors == null);
            Assert.True(statergey.ValidCount == 2);
            Assert.True(statergey.DuplicateCount == 1);
            Assert.True(statergey.ErrorCount == 0);
        }

        [Theory, StandardAutoMoqData]
        public async void ShouldValidate_VaildData(StandardMigrationStrategy statergey)
        {
            //Arrange
            _moq.SetUpReadResponseValid();

            var sut = new FileManager(_moq.File.Object, _moq.JsonDirectory.Object, _moq.FileManagerLogger.Object);

            var result = await sut.ReadFileAsync();

            statergey.SetPayload(result.Payload);

            //Act
            var response = await statergey.MigrateAsync();

            _moq.VerifyRead();

            //Assert
            Assert.True(response.Status == ServiceResultStatus.Success);
            Assert.True(response.Errors == null);
            Assert.True(statergey.ValidCount > 0);
            Assert.True(statergey.DuplicateCount == 0);
            Assert.True(statergey.ErrorCount == 0);
        }
    }
}
