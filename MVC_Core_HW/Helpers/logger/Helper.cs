using MVC_Core_HW.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MVC_Core_HW.Helpers
{
    public class Helper
    {
        private string _uri = @".\DataHolder\Logger.json";

        public Helper()
        {
        }

        public void Log(SearchTypeLog log)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            List<SearchTypeLog> stl = this.Deserialize();
            
            if(stl == null || stl.Count == 0)
            {
                stl = new List<SearchTypeLog>();
            }
            stl.Add(log);

            using (StreamWriter sw = new StreamWriter(this._uri))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, stl);
                }
            }
        }

        public List<SearchTypeLog> Deserialize()
        {
            List<SearchTypeLog> stl = new List<SearchTypeLog>();
            using(StreamReader sr = new StreamReader(this._uri))
            {
                var json = sr.ReadToEnd();
                stl = JsonConvert.DeserializeObject<List<SearchTypeLog>>(json);
            }
            return stl;
        }
    }
}
