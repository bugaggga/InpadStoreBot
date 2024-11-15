using Microsoft.Extensions.DependencyInjection;
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
	private readonly IServiceProvider serviceProvider;

	public UserContextManager(ITelegramBotClient botClient, IServiceProvider serviceProvider)
	{
		this.botClient = botClient;
		this.serviceProvider = serviceProvider;
	}

	public async Task<UserContext> GetOrCreateContext(long userId, string currentMessage)
	{
		if (currentMessage.StartsWith('/'))
		{
			if (Contexts.ContainsKey(userId))
			{
				await botClient.DeleteBotMessageAsync(Contexts[userId], userId, Contexts[userId].PreviousMessageId);
			}
			ResetUserContext(userId, new HandlerDistributor<IReplyMarkupHandler>(serviceProvider.GetServices<IReplyMarkupHandler>()).GetHandler(currentMessage));
		}

		else if (!Contexts.ContainsKey(userId))
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

	private void ResetUserContext(long userId, IState? state)
	{
		Contexts[userId] = new UserContext(userId, botClient, serviceProvider, state);
	}
}