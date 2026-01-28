using Newtonsoft.Json;

namespace MqttManager.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson(this object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
