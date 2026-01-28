using MqttManager.Constants;
using MqttManager.Dto;
using MqttManager.Dto.Base;
using Newtonsoft.Json;

namespace MqttManager.DtoFactory
{
    public class DtoFactory : IDtoFactory
    {
        private static readonly Dictionary<MessageTypeEnum, Type> _messageTypes = new()
        {
            { MessageTypeEnum.AlarmOff , typeof (AlarmOffDto) }
        };

        public IDto? CreateDto(MessageTypeEnum messageTypeEnum, string jsonPayload)
        {
            if (!_messageTypes.TryGetValue(messageTypeEnum, out var targetType))
                return null;

            var idto = (IDto?)JsonConvert.DeserializeObject(jsonPayload, targetType);
            if (idto == null) return null;

            idto.MessageType = messageTypeEnum.ToString();
            return idto;
        }

        public T? GetDto<T>(string jsonPayload) where T : class, IDto
        {
            return JsonConvert.DeserializeObject<T>(jsonPayload);
        }
    }
}
