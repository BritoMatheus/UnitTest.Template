using MqttManager.Extensions;
using Newtonsoft.Json;

namespace MqttManager.Dto.Base
{
    public class BaseDto : IDto
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; } = TimeExtension.EuropeLocalTime();

        [JsonProperty("messageType")]
        public string MessageType { get; set; } = default!; // MessageTypeEnum

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}