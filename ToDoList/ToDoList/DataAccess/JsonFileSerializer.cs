using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Xml;

namespace ToDoList.DataAccess
{
    public class JsonFileSerializer
    {
        public JsonFileSerializer()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
        }
        public void Serialize<T>(IEnumerable<T> obj)
        {
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString("yyyy MM dd HHmmss");
            string fileName = $"../../../Saves/TDLSave - {formattedDate}.json";

            using (FileStream stream = File.Create(fileName))
            {
                using StreamWriter streamWriter = new StreamWriter(stream);
                using (JsonTextWriter jStream = new JsonTextWriter(streamWriter)
                {

                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' ',

                })
                {
                    (new JsonSerializer()).Serialize(jStream, obj);
                }
            }
        }


        public IEnumerable<T> Deserialize<T>(string fileName)
        {
            string filename = "../../../Saves/" + fileName;
            IEnumerable<T> obj;
            using (StreamReader file = File.OpenText(filename))
            {

                JsonSerializer serializer = new JsonSerializer();
                obj = (IEnumerable<T>)serializer.Deserialize(file, typeof(IEnumerable<T>));
            }
            return obj;
        }
    }
}

