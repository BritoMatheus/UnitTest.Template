using Moq;
using MqttManager.Dto;
using MqttManager.Extensions;
using MqttManager.Setting;
using UnitTestAgend.Tests.InfraTests.Builders.Dto;
using UnitTestAgend.Tests.InfraTests.Builders.Setting;
using UnitTestAgend.Tests.InfraTests.Fixtures.MqttManager.Processor;

namespace UnitTestAgend.Tests.MqttManager.Processor
{
    public class AlarmOffProcessorTests(AlarmOffProcessorFixture fixture) : IClassFixture<AlarmOffProcessorFixture>
    {
        [Fact]
        public async Task ProcessMessage_InvalidDtoAsync()
        {
            //arrange
            var instance = fixture.NewInstance();

            fixture.SetupDtoFactoryGetDto<AlarmOffDto>(null);

            //act
            await instance.ProcessMessage(null);

            //assert
            fixture.VerifyDtoFactoryGetDto<AlarmOffDto>(Times.Once());
            fixture.VerifySirenServiceStopSiren(Times.Never());
            fixture.VerifySirenServiceGetActiveSirens(Times.Never());
        }

        [Fact]
        public async Task ProcessMessage_NoActiveSirensAsync()
        {
            //arrange
            var instance = fixture.NewInstance();
            var dto = new AlarmOffDtoBuilder()
                .WithPrintJobGuid("guid")
                .Build();

            fixture.SetupDtoFactoryGetDto<AlarmOffDto>(dto);
            fixture.SetupSirenServiceGetActiveSirens(null);

            //act
            await instance.ProcessMessage(dto.ToJson());

            //assert
            fixture.VerifyDtoFactoryGetDto<AlarmOffDto>(Times.Once());
            fixture.VerifySirenServiceGetActiveSirens(Times.Once());
            fixture.VerifySirenServiceStopSiren(Times.Never());
        }

        [Fact]
        public async Task ProcessMessage_EmptyActiveSirensAsync()
        {
            //arrange
            var instance = fixture.NewInstance();
            var dto = new AlarmOffDtoBuilder()
                .WithPrintJobGuid("guid")
                .Build();

            fixture.SetupDtoFactoryGetDto<AlarmOffDto>(dto);
            fixture.SetupSirenServiceGetActiveSirens(new List<SirenAndLightSetting>());

            //act
            await instance.ProcessMessage(dto.ToJson());

            //assert
            fixture.VerifyDtoFactoryGetDto<AlarmOffDto>(Times.Once());
            fixture.VerifySirenServiceGetActiveSirens(Times.Once());
            fixture.VerifySirenServiceStopSiren(Times.Never());
        }

        [Fact]
        public async Task ProcessMessage_StopsAllActiveSirensAsync()
        {
            //arrange
            var instance = fixture.NewInstance();
            var dto = new AlarmOffDtoBuilder()
                .WithPrintJobGuid("guid")
                .Build();
            var sirens = new List<SirenAndLightSetting>
            {
                new SirenAndLightSettingBuilder()
                    .WithSirenId("siren1")
                    .Build(),
                new SirenAndLightSettingBuilder()
                    .WithSirenId("siren2")
                    .Build()
            };

            fixture.SetupDtoFactoryGetDto<AlarmOffDto>(dto);
            fixture.SetupSirenServiceStopSiren();
            fixture.SetupSirenServiceGetActiveSirens(sirens);

            //act
            await instance.ProcessMessage(dto.ToJson());

            //assert
            fixture.VerifyDtoFactoryGetDto<AlarmOffDto>(Times.Once());
            fixture.VerifySirenServiceGetActiveSirens(Times.Once());
            fixture.VerifySirenServiceStopSiren(Times.Exactly(2));
        }

        [Fact]
        public async Task ProcessMessage_StopSirenThrows_ContinuesAsync()
        {
            //arrange
            var instance = fixture.NewInstance();
            var dto = new AlarmOffDtoBuilder()
                .WithPrintJobGuid("guid")
                .Build();
            var sirens = new List<SirenAndLightSetting>
            {
                new SirenAndLightSettingBuilder()
                    .WithSirenId("siren1")
                    .Build(),
                new SirenAndLightSettingBuilder()
                    .WithSirenId("siren2")
                    .Build()
            };

            fixture.SetupDtoFactoryGetDto<AlarmOffDto>(dto);
            fixture.SetupSirenServiceStopSirenThrows("siren1", new Exception("stop error"));
            fixture.SetupSirenServiceStopSirenSucceeds("siren2");
            fixture.SetupSirenServiceGetActiveSirens(sirens);

            //act
            await instance.ProcessMessage(dto.ToJson());

            //assert
            fixture.VerifyDtoFactoryGetDto<AlarmOffDto>(Times.Once());
            fixture.VerifySirenServiceGetActiveSirens(Times.Once());
            fixture.VerifySirenServiceStopSiren(Times.Exactly(2));
        }

        [Fact]
        public async Task ProcessMessage_DtoFactoryThrows_HandlesExceptionAsync()
        {
            //arrange
            var instance = fixture.NewInstance();

            fixture.SetupDtoFactoryGetDtoThrows<AlarmOffDto>(new Exception("dto error"));

            //act
            await instance.ProcessMessage("message");

            //assert
            fixture.VerifyDtoFactoryGetDto<AlarmOffDto>(Times.Once());
            fixture.VerifySirenServiceGetActiveSirens(Times.Never());
            fixture.VerifySirenServiceStopSiren(Times.Never());
        }


    }
}
