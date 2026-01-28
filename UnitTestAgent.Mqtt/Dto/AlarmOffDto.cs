using MqttManager.Dto.Base;
using Newtonsoft.Json;

namespace MqttManager.Dto
{
    public class AlarmOffDto : BaseDto
    {
        [JsonProperty("printJobGuid")]
        public string PrintJobGuid { get; set; } = default!;
    }
}
