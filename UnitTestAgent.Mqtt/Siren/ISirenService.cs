using MqttManager.Setting;

namespace MqttManager.Siren
{
    public interface ISirenService
    {
        void StartSirenService(List<SirenAndLightSetting>? sirenAndLightSettings);

        Task RunSiren(string sirenId);

        Task StopSiren(string sirenId);

        List<SirenAndLightSetting>? GetActiveSirens();
    }
}
