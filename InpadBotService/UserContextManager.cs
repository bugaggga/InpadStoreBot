using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InpadBotService;

public class UserContextManager
{
	private Dictionary<long, UserContext> Contexts { get; } = new();
	private readonly ITelegramBotClient botClient;

	public UserContextManager(ITelegramBotClient botClient)
	{
		this.botClient = botClient;
	}
	public UserContext GetOrCreateContext(long userId, string currentMessage)
	{
		if (!Contexts.ContainsKey(userId) ||
			currentMessage == "/start" ||
			currentMessage == "/help" ||
			currentMessage == "/support" ||
			currentMessage == "/question")
		{
			ResetUserContext(userId);
		}

		Contexts[userId].CurrentMessage = currentMessage;
		return Contexts[userId];
	}

	private void ResetUserContext(long userId)
	{
		Contexts[userId] = new UserContext(userId, botClient);
	}
}