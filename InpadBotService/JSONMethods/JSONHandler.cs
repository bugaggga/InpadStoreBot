using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace InpadBotService.JSONMethods
{
    public static class JSONHandler
    {

        public static string? FindValueByKey(string? key, string? valueKey)
        {
			var r = new StreamReader("JSONMethods\\pluginLinks.json");
			var json = r.ReadToEnd();
			var obj = JObject.Parse(json);
            var value = obj[key][valueKey].Value<string[]>();
            //var valueKey = value[valueKey];

            return null;
        }
    }
}
