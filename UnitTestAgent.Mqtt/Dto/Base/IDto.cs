namespace MqttManager.Dto.Base
{
    public interface IDto
    {
        DateTime Timestamp { get; set; }
        string MessageType { get; set; }
    }
}
