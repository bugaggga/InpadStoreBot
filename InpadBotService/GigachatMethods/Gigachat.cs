using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using static System.Formats.Asn1.AsnWriter;

namespace InpadBotService.GigachatMethods
{

    internal class Gigachat
    {
        private static readonly string gigachatApiOauthUrl = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth"; // Замените на реальный URL
        private static readonly string gigachatApiReqestUrl = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions"; // Замените на реальный URL
        private static readonly string gigachatApiKey = "ZGZhNDk2NmYtZjJhOS00ZDM2LWIxMmQtZDRjN2I4ZjVjYjYxOmE2YTI4NTY0LTNiYTUtNDczZi1hM2MwLTA2YTBhZmVhNDQ5YQ=="; // Ваш API-ключ Gigachat
        //private static readonly HttpClient client = new HttpClient();

        public static async Task GetOauthToken()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var client = new HttpClient(handler))
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, gigachatApiOauthUrl);
                    //request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    request.Headers.Add("Accept", "application/json");
                    var guid = Guid.NewGuid().ToString();
                    Console.WriteLine(guid);
                    request.Headers.Add("RqUID", guid);
                    request.Headers.Add("Authorization", $"Basic {gigachatApiKey}");
                    
                    var data = new List<KeyValuePair<string, string>>() { new("scope", "GIGACHAT_API_PERS") };
                    var content = new FormUrlEncodedContent(data);
                    request.Content = content;

                    // Отправка POST запроса
                    HttpResponseMessage response = await client.SendAsync(request);
                    Console.WriteLine(response.StatusCode.ToString());
                    Console.WriteLine();
                    response.EnsureSuccessStatusCode(); // выбросит исключение, если код ответа не успешный
                }


                //string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine("POST Response:");
                //Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка при отправке POST запроса: {e.Message}");
            }
        }

        public static async Task<string> GetGigachatResponse(string userMessage)
        {
            return "";
        }
    }

    
}
