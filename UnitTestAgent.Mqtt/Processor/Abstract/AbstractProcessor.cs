using Microsoft.Extensions.Logging;
using MqttManager.Constants;
using MqttManager.Processor.Interface;

namespace MqttManager.Processor.Abstract
{
    public abstract class AbstractProcessor : IBaseProcessor
    {
        protected readonly ILogger<AbstractProcessor> _logger;
        protected Setting.Processor? _processorSetting;

        protected AbstractProcessor(ILogger<AbstractProcessor> logger)
        {
            _logger = logger;
        }

        public abstract ProcessorTypeEnum ProcessorType { get; }
        public abstract MessageTypeEnum MessageType { get; }
        public abstract void InitializeProcessor(Setting.Processor processorSetting);
        public abstract Task ProcessMessage(string message);
    }
}
