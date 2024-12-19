using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpadBotService.GigachatMethods
{
    public class AccessTokenResponse
    {
        public string access_token { get; }
        public long expires_at { get; }

        public AccessTokenResponse(string access_token, long expires_at)
        {
            this.access_token = access_token;
            this.expires_at = expires_at;
        }

        public override string ToString()
        {
            return $"token: {access_token}; expires_at: {expires_at}";
        }
    }

    public class MessageResponse
    {
        public List<Choice> choices { get; } 
        public int created { get; }
        public string model { get; }
        public Usage usage { get; }
        public string @object { get; }
        public MessageResponse(List<Choice> choices, int created, string model, Usage usage, string @object)
        {
            this.choices = choices;
            this.created = created;
            this.model = model;
            this.usage = usage;
            this.@object = @object;
        }
    }

    public class Choice
    {
        public Message message { get; }
        public int index { get; }
        public string finish_reason { get; }
        public Choice(Message message, int index, string finish_reason)
        {
            this.message = message;
            this.index = index;
            this.finish_reason = finish_reason;
        }
    }

    public class Message
    {
        public string role { get; }
        public string content { get; }
        public long created { get; }
        public string name { get; }
        public string functions_state_id { get; }
        public FunctionCall function_call { get; }
        public object[] data_for_context { get; }
        public Message(string role, string content, long created, string name, string functions_state_id, FunctionCall function_call, object[] data_for_context)
        {
            this.role = role;
            this.content = content;
            this.created = created;
            this.name = name;
            this.functions_state_id = functions_state_id;
            this.function_call = function_call;
            this.data_for_context = data_for_context;
        }
    }

    public class FunctionCall
    {
        public string name { get; }
        public object arguments { get; }
        public FunctionCall(string name, object arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }
    }

    public class Usage
    {
        public int prompt_tokens { get; }
        public int completion_tokens { get; }
        public int total_tokens { get; }
        public Usage(int prompt_tokens, int completion_tokens, int total_tokens)
        {
            this.prompt_tokens = prompt_tokens;
            this.completion_tokens = completion_tokens;
            this.total_tokens = total_tokens;
        }
    }
}
