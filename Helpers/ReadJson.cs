using System.IO;

namespace Invento.Helpers
{
    public class ReadJson
    {
//        public List<Campus> LoadJson()
//        {
//            using (var r = new StreamReader("./data/json/campus.json"))
//            {
//                var json = r.ReadToEnd();
//                
//                //Get JsonArray With Object
//                var items = JsonConvert.DeserializeObject<List<Campus>>(json);
//                foreach (var item in items)
//                {
//                    Console.WriteLine("{0} {1}", item.Name, item.Country);
//                }
//
//                return items;
//                
//                //Get JsonArray Without Object
//                dynamic array = JsonConvert.DeserializeObject(json);
//                Console.WriteLine("{0} {1}", array[0].Name, array[1].Name);
//            }
//        }

        public static string LoadJson (string path)
        {
            var data = new StreamReader(path);
            return data.ReadToEnd();
        }
    }
}