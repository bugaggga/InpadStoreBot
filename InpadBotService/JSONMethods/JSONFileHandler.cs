using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpadBotService.JSONMethods
{
    internal class JSONFileHandler
    {
        string JSON;

        public JSONFileHandler(string fileName)
        {
            using (StreamReader r = new StreamReader("file.json"))
            {
                JSON = r.ReadToEnd();
            }
        }
    }
}
