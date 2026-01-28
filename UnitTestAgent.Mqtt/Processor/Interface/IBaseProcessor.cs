using MqttManager.Constants;

namespace MqttManager.Processor.Interface
{
    public interface IBaseProcessor
    {
        MessageTypeEnum MessageType { get; }
        ProcessorTypeEnum ProcessorType { get; }
        Task ProcessMessage(string message);
        void InitializeProcessor(Setting.Processor processorSetting);
    }
}
