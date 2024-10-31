using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace InpadBotService;

public class UserContext
{
	//public IHandler handler {  get; set; }
	public long UserId { get; }
	public string CurrentState { get; set; }
	public string CurrentMessage { get; set; }
	public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();

	public UserContext(long userId)
	{
		UserId = userId;
		CurrentState = "Start"; // Начальное состояние
		CurrentMessage = string.Empty;
		//this.handler = handler;
	}

	//public void Request()
	//{
	//	this.handler.Handle();
	//}
}

public class UserContextManager
{
	public Dictionary<long, UserContext> contexts { get; } = new();
	public UserContext GetOrCreateContext(long userId, string currentMessage)
	{
		if (!contexts.ContainsKey(userId) ||
			currentMessage == "/start" ||
			currentMessage == "/help" ||
			currentMessage == "/support" ||
			currentMessage == "/question")
		{
			ResetUserContext(userId);
		}

		contexts[userId].CurrentMessage = currentMessage;
		return contexts[userId];
	}

	private void ResetUserContext(long userId)
	{
		contexts[userId] = new UserContext(userId);
	}
}
