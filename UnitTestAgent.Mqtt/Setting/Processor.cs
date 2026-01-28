namespace MqttManager.Setting
{
    public class Processor
    {
        public string ProcessorType { get; set; } = default!;
        public List<string> PublisherList { get; set; } = new List<string>(); // Publisher client names
        public List<string> SnoozePublisherList { get; set; } = new List<string>(); // Snoozed alarm Publisher client names
    }
}
