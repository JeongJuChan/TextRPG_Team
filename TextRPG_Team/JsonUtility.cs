using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TextRPG_Team
{

    public class JsonUtility
    {
        static readonly string basePath = Directory.GetParent(Environment.CurrentDirectory).FullName;

        public static void Save<T>(T t, string name) where T : class
        {
            string filePath = @$"{basePath}\{name}.json";
            
            string json = JsonSerializer.Serialize(t);
            File.WriteAllText(filePath, json);
        }
        
        public static T? Load<T>(string name) where T : class
        {
            string filePath = $@"{basePath}\{name}.json";

            string json = File.ReadAllText(filePath);

            if (!File.Exists(filePath))
            {
                return null;
            }
            else
            {
                T t = JsonSerializer.Deserialize<T>(json);
                if (t == null)
                {
                    return null;
                }
                else
                {
                    return t;
                }
            }
        }
    }
}
