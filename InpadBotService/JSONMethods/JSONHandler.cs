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

        public static IEnumerable<string?> FindValueByKey(string? key, string? valueKey)
        {
			var r = new StreamReader("JSONMethods\\pluginLinks.json");
			var json = r.ReadToEnd();
			var obj = JObject.Parse(json);
            Console.WriteLine(obj[key][valueKey].Type);
			Console.WriteLine(obj[key].Type);
			var value = obj[key][valueKey].ToArray().Select(x => x.Value<string>());
            //var valueKey = value[valueKey];
            //Console.WriteLine(value.First());
            return value;
        }
    }
}
