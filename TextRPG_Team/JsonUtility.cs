using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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

            if (!File.Exists(filePath))
            {
                return null;
            }
            else
            {
                string json = File.ReadAllText(filePath);
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
