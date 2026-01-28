namespace MqttManager.Extensions
{
    public class TimeExtension
    {
        public static DateTime EuropeLocalTime()
        {
            TimeZoneInfo locationTimezone;

            try
            {
                locationTimezone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                // Windows and Linux uses different names, try to find using Linux convetion
                locationTimezone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Berlin");
            }

            var timestamp = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, locationTimezone);
            return timestamp;
        }
    }
}
