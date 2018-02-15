using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WTS.BL.Utils;

namespace WTS.BL.Common
{

    public class DictionaryArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IDictionary<int, string>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JArray.Load(reader);
            var dict = new Dictionary<int, string>();
            foreach (var prop in obj)
                dict.Add(prop ["key"].ToString().TryParse<int>(), prop ["value"].ToString());
            return dict;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dict = (IDictionary<int, string>)value;
            var obj = new JArray();
            foreach (var kvp in dict)
            {
                obj.Add(new JObject(
                    new JProperty("key", kvp.Key.ToString()),
                    new JProperty("value", kvp.Value)
                ));
            }
            obj.WriteTo(writer);
        }
    }
}
