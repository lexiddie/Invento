using System;

namespace Invento.Helpers
{
    public class ReadDateTime
    {
        public static string ReadDate(String dateTime = "11/23/2019 20:06:36")
        {
            var arr = dateTime.Split(' ')[0];
            return arr;
        }
        
        public static string ReadTime(String dateTime = "11/23/2019 20:06:36")
        {
            var arr = dateTime.Split(' ')[1];
            return arr;
        }
        
        public static string ReadRealTime(String time = "12:00:00")
        {
            var hour = time.Split(":")[0];
            var minute = time.Split(":")[1];
            return $"{hour}:{minute}";
        }

        public static string ReadTimeSpan(TimeSpan timeSpan)
        {
            var hour = timeSpan.ToString().Split(":")[0];
            var minute = timeSpan.ToString().Split(":")[1];
            return $"{hour}:{minute}";
        }
    }
}