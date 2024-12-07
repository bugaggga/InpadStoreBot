using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace InpadBotService.GigachatMethods
{

    internal class Gigachat
    {
        private static readonly string gigachatApiOauthUrl = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth"; // Замените на реальный URL
        private static readonly string gigachatApiReqestUrl = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions"; // Замените на реальный URL
        private static readonly string gigachatApiKey = "ZGZhNDk2NmYtZjJhOS00ZDM2LWIxMmQtZDRjN2I4ZjVjYjYxOmE2YTI4NTY0LTNiYTUtNDczZi1hM2MwLTA2YTBhZmVhNDQ5YQ=="; // Ваш API-ключ Gigachat
        private static readonly HttpClient client = new HttpClient();

        public static async Task GetOauthToken()
        {
            try
            {
                // Подготовка данных для отправки
                var jsonData = "{ scope: GIGACHAT_API_PERS }"; // Замените на ваши данные
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Отправка POST запроса
                HttpResponseMessage response = await client.PostAsync(gigachatApiOauthUrl, content);
                response.EnsureSuccessStatusCode(); // выбросит исключение, если код ответа не успешный

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("POST Response:");
                Console.WriteLine(responseBody);
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
