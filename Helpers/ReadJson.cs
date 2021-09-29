using System.IO;

namespace Invento.Helpers
{
    public class ReadJson
    {
        public static string LoadJson (string path)
        {
            var data = new StreamReader(path);
            return data.ReadToEnd();
        }
    }
}