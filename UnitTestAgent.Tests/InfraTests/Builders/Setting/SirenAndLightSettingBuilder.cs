using MqttManager.Setting;

namespace UnitTestAgend.Tests.InfraTests.Builders.Setting;

public class SirenAndLightSettingBuilder : BaseBuilder<SirenAndLightSetting>
{

    public SirenAndLightSettingBuilder WithSirenId(string sirenId)
    {
        _class.SirenId = sirenId;
        return this;
    }

    public SirenAndLightSettingBuilder WithIpAddress(string ipAddress)
    {
        _class.IpAddress = ipAddress;
        return this;
    }
}
