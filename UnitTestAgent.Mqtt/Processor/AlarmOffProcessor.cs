using Microsoft.Extensions.Logging;
using MqttManager.Constants;
using MqttManager.Dto;
using MqttManager.DtoFactory;
using MqttManager.Siren;

namespace MqttManager.Processor
{
    public class AlarmOffProcessor : Abstract.AbstractProcessor
    {
        private readonly IDtoFactory _dtoFactory;
        private readonly ISirenService _sirenService;

        public AlarmOffProcessor(ILogger<AlarmOffProcessor> logger, IDtoFactory dtoFactory, ISirenService sirenService) : base(logger)
        {
            _dtoFactory = dtoFactory;
            _sirenService = sirenService;
        }

        public override ProcessorTypeEnum ProcessorType => ProcessorTypeEnum.AlarmOff;

        public override MessageTypeEnum MessageType => MessageTypeEnum.AlarmOff;

        public override void InitializeProcessor(Setting.Processor processorSetting)
        {
            _processorSetting = processorSetting;
        }

        public async override Task ProcessMessage(string message)
        {
            try
            {
                _logger.LogInformation($"AlarmOffProcessor: Received message : {message}.");

                var alarmOffDto = _dtoFactory.GetDto<AlarmOffDto>(message);
                if (alarmOffDto == null)
                {
                    _logger.LogInformation($"AlarmOffProcessor: DTO creation is returning null. Message : {message}");
                    return;
                }
                var activeSirens = _sirenService.GetActiveSirens();
                if (activeSirens == null)
                {
                    _logger.LogInformation($"AlarmOffProcessor: There is no active siren(s) defined.");
                    return;
                }

                var tasks = activeSirens.Select(async item =>
                {
                    try
                    {
                        await _sirenService.StopSiren(item.SirenId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"AlarmOffProcessor : ProcessMessage failed for message: {message} - SirenId: {item} - Message: {ex.Message}");
                    }
                });

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AlarmOffProcessor: Error processing message: {ex.Message}");
            }
        }
    }
}
