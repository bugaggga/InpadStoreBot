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
using static System.Net.Mime.MediaTypeNames;

namespace InpadBotService.GigachatMethods
{

    internal class Gigachat
    {
        private static readonly string gigachatApiOauthUrl = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth"; // Замените на реальный URL
        private static readonly string gigachatApiReqestUrl = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions"; // Замените на реальный URL
        private static readonly string gigachatApiKey = "ZGZhNDk2NmYtZjJhOS00ZDM2LWIxMmQtZDRjN2I4ZjVjYjYxOmE2YTI4NTY0LTNiYTUtNDczZi1hM2MwLTA2YTBhZmVhNDQ5YQ=="; // Ваш API-ключ Gigachat
        //private static readonly HttpClient client = new HttpClient();

        private static async Task<AccessTokenResponse?> GetOauthToken()
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
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
                    request.Headers.Add("RqUID", guid);
                    request.Headers.Add("Authorization", $"Basic {gigachatApiKey}");
                    
                    var data = new List<KeyValuePair<string, string>>() { new("scope", "GIGACHAT_API_PERS") };
                    var content = new FormUrlEncodedContent(data);
                    request.Content = content;

                    // Отправка POST запроса
                    var response = await client.SendAsync(request);
                    Console.WriteLine(response.StatusCode.ToString());
                    Console.WriteLine();

                    response.EnsureSuccessStatusCode(); // выбросит исключение, если код ответа не успешный
                    var result = JsonSerializer.Deserialize<AccessTokenResponse>(await response.Content.ReadAsStreamAsync());
                    return result;
                }

                //string responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine("POST Response:");
                //Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка при отправке POST запроса: {e.Message}");
                return null;
            }
        }

        public static async Task<string> GetGigachatResponse(string userMessage)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                var token = await GetOauthToken();

                using (var client = new HttpClient(handler))
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, gigachatApiReqestUrl);

                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("Authorization", $"Bearer {token.access_token}");

                    var systemMessage = "Ты сотрудник компании Inpad.store. Ответь на следующий вопрос";

                    var body = @"{" + "\n" +
                        @"  ""model"": ""GigaChat""," + "\n" +
                        @"  ""messages"": [" + "\n" +
                        @"    {" + "\n" +
                        @"      ""role"": ""system""," + "\n" +
                        $@"      ""content"": ""{systemMessage}""" + "\n" +
                        @"    }," + "\n" +
                        @"    {" + "\n" +
                        @"      ""role"": ""user""," + "\n" +
                        $@"      ""content"": ""{userMessage}""" + "\n" +
                        @"    }" + "\n" +
                        @"  ]," + "\n" +
                        @"  ""stream"": false," + "\n" +
                        @"  ""update_interval"": 0" + "\n" +
                        @"}";

                    var content = new StringContent(body,
                        null,
                        "application/json");
                    request.Content = content;

                    HttpResponseMessage response = await client.SendAsync(request);
                    Console.WriteLine(response.StatusCode.ToString());
                    response.EnsureSuccessStatusCode();
                    var result = JsonSerializer.Deserialize<MessageResponse>(await response.Content.ReadAsStreamAsync());
                    //Console.WriteLine(await response.Content.ReadAsStringAsync());
                    return result.choices[result.choices.Count() - 1].message.content;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка при отправке POST запроса: {e.Message}");
                return null;
            }
        }
    }

    //public class AccessTokenResponse
    //{
    //    public string access_token{ get; }
    //    public long expires_at { get; }
        
    //    public AccessTokenResponse(string access_token, long expires_at)
    //    {
    //        this.access_token = access_token;
    //        this.expires_at = expires_at;
    //    }

    //    public override string ToString(){
    //        return $"token: {access_token}; expires_at: {expires_at}";
    //    }
    //}

    //public class MessageResponse
    //{
    //    public List<Choice> choices { get; } //&&&
    //    public int created { get; }
    //    public string model { get; }
    //    public Usage usage { get; }        
    //    public string @object { get; }
    //    public MessageResponse(List<Choice> choices, int created, string model, Usage usage, string @object)
    //    {
    //        this.choices = choices;
    //        this.created = created;
    //        this.model = model;
    //        this.usage = usage;
    //        this.@object = @object;
    //    }
    //}

    //public class Choice
    //{
    //    public Message message { get; }
    //    public int index { get; }
    //    public string finish_reason { get; }
    //    public Choice(Message message, int index, string finish_reason)
    //    {
    //        this.message = message;
    //        this.index = index;
    //        this.finish_reason = finish_reason;
    //    }
    //}

    //public class Message
    //{
    //    public string role { get; }
    //    public string content { get; }
    //    public long created { get; }
    //    public string name { get; }
    //    public string functions_state_id { get; }
    //    public FunctionCall function_call { get; }
    //    public object[] data_for_context { get; }
    //    public Message(string role, string content, long created, string name, string functions_state_id, FunctionCall function_call, object[] data_for_context)
    //    {
    //        this.role = role;
    //        this.content = content;
    //        this.created = created;
    //        this.name = name;
    //        this.functions_state_id = functions_state_id;
    //        this.function_call = function_call;
    //        this.data_for_context = data_for_context;
    //    }
    //}

    //public class FunctionCall
    //{
    //    public string name { get; }
    //    public object arguments { get; }
    //    public FunctionCall(string name, object arguments)
    //    {
    //        this.name = name;
    //        this.arguments = arguments;
    //    }
    //}

    //public class Usage
    //{
    //    public int prompt_tokens { get; }
    //    public int completion_tokens { get; }
    //    public int total_tokens { get; }
    //    public Usage(int prompt_tokens, int completion_tokens, int total_tokens)
    //    {
    //        this.prompt_tokens = prompt_tokens;
    //        this.completion_tokens = completion_tokens;
    //        this.total_tokens = total_tokens;
    //    }
    //}
}