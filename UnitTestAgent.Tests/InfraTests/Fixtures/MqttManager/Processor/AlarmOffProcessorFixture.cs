using Microsoft.Extensions.Logging;
using Moq;
using MqttManager.Dto.Base;
using MqttManager.DtoFactory;
using MqttManager.Processor;
using MqttManager.Setting;
using MqttManager.Siren;

namespace UnitTestAgend.Tests.InfraTests.Fixtures.MqttManager.Processor
{
    public class AlarmOffProcessorFixture
    {
        public Mock<ILogger<AlarmOffProcessor>> LoggerMock { get; set; }
        public Mock<IDtoFactory> DtoFactoryMock { get; set; }
        public Mock<ISirenService> SirenServiceMock { get; set; }

        public void ConfigureMocks()
        {
            LoggerMock = new();
            DtoFactoryMock = new();
            SirenServiceMock = new();
        }

        public AlarmOffProcessor NewInstance()
        {
            ConfigureMocks();
            return new AlarmOffProcessor(
                LoggerMock.Object,
                DtoFactoryMock.Object,
                SirenServiceMock.Object
                );
        }

        public void SetupDtoFactoryGetDto<T>(T expectedResult) where T : class, IDto
        {
            DtoFactoryMock
                .Setup(s => s.GetDto<T>(It.IsAny<string>()))
                .Returns(expectedResult);
        }

        public void SetupDtoFactoryGetDtoThrows<T>(Exception exception) where T : class, IDto
        {
            DtoFactoryMock
                .Setup(s => s.GetDto<T>(It.IsAny<string>()))
                .Throws(exception);
        }

        public void SetupSirenServiceGetActiveSirens(List<SirenAndLightSetting>? expectedResult)
        {
            SirenServiceMock
                .Setup(s => s.GetActiveSirens())
                .Returns(expectedResult);
        }

        public void SetupSirenServiceStopSiren()
        {
            SirenServiceMock
                .Setup(s => s.StopSiren(It.IsAny<string>()))
                .Returns(Task.CompletedTask);
        }

        public void SetupSirenServiceStopSirenThrows(string sirenId, Exception exception)
        {
            SirenServiceMock
                .Setup(s => s.StopSiren(sirenId))
                .ThrowsAsync(exception);
        }

        public void SetupSirenServiceStopSirenThrows(Exception exception)
        {
            SirenServiceMock
                .Setup(s => s.StopSiren(It.IsAny<string>()))
                .ThrowsAsync(exception);
        }

        public void SetupSirenServiceStopSirenSucceeds(string sirenId)
        {
            SirenServiceMock
                .Setup(s => s.StopSiren(sirenId))
                .Returns(Task.CompletedTask);
        }

        public void VerifyDtoFactoryGetDto<T>(Times times) where T : class, IDto
        {
            DtoFactoryMock
                .Verify(s => s.GetDto<T>(It.IsAny<string>()), times);
        }

        public void VerifySirenServiceGetActiveSirens(Times times)
        {
            SirenServiceMock
                .Verify(s => s.GetActiveSirens(), times);
        }

        public void VerifySirenServiceStopSiren(Times times)
        {
            SirenServiceMock
                .Verify(s => s.StopSiren(It.IsAny<string>()), times);
        }


    }
}
