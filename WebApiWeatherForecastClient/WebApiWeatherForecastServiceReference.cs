using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiWeatherForecastClient.WebApiWeatherForecastServiceReference
{
    public static class DateOnlyExtensions
    {
        public static DateOnly ParseExact(string value, string dateFormat, CultureInfo invariantCulture)
        {
            var dateTime = DateTime.ParseExact(value, dateFormat, invariantCulture);

            return new DateOnly
            {
                Day = dateTime.Day,
                Month = dateTime.Month,
                Year = dateTime.Year,
            };
        }

        public static string ToString(this DateOnly dateOnly, string dateFormat, CultureInfo invariantCulture)
        {
            DateTime toString = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);

            return toString.ToString(dateFormat, invariantCulture);
        }
    }

    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return DateOnlyExtensions.ParseExact((string)reader.Value, DateFormat, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }

    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private const string TimeFormat = "HH:mm:ss.FFFFFFF";

        public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return TimeOnly.ParseExact((string)reader.Value, TimeFormat, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(TimeFormat, CultureInfo.InvariantCulture));
        }
    }

    public partial class WebApiWeatherForecastServiceReference
    {
        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)

        {
            settings.Converters.Add(new DateOnlyJsonConverter());
            settings.Converters.Add(new TimeOnlyJsonConverter());
        }
    }
}