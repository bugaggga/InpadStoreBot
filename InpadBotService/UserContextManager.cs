using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InpadBotService
{
	public class UserContextManager
	{
		private Dictionary<long, UserContext> Contexts { get; } = new();
		private readonly ITelegramBotClient botClient;
		private readonly IServiceProvider serviceProvider;

		public UserContextManager(ITelegramBotClient botClient, IServiceProvider serviceProvider)
		{
			this.botClient = botClient;
			this.serviceProvider = serviceProvider;
		}
		public UserContext GetOrCreateContext(long userId, string currentMessage)
		{
			if (!Contexts.ContainsKey(userId) ||
				currentMessage == "/start")
			{
				ResetUserContext(userId);
			}

			Contexts[userId].CurrentMessage = currentMessage;
			return Contexts[userId];
		}

		private void ResetUserContext(long userId)
		{
			Contexts[userId] = new UserContext(userId, botClient, serviceProvider);
		}
	}
}
