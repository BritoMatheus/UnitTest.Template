using MqttManager.Constants;
using MqttManager.Dto.Base;

namespace MqttManager.DtoFactory
{
    public interface IDtoFactory
    {
        IDto? CreateDto(MessageTypeEnum messageTypeEnum, string jsonPayload);
        T? GetDto<T>(string jsonPayload) where T : class, IDto;
    }
}
