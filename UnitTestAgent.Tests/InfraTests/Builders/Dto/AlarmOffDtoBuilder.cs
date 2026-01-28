using MqttManager.Dto;

namespace UnitTestAgend.Tests.InfraTests.Builders.Dto;

public class AlarmOffDtoBuilder : BaseBuilder<AlarmOffDto>
{
    public AlarmOffDtoBuilder WithPrintJobGuid(string printJobGuid)
    {
        base._class.PrintJobGuid = printJobGuid;
        return this;
    }
}
