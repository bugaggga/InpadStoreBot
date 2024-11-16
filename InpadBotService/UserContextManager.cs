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

	public async Task<UserContext> GetOrCreateContext(long chatId, string currentMessage)
	{
		if (currentMessage.StartsWith('/'))
		{
			if (Contexts.ContainsKey(chatId))
			{
				await botClient.DeleteBotMessageAsync(Contexts[chatId], chatId, Contexts[chatId].PreviousMessageId);
			}
			ResetUserContext(chatId, new HandlerDistributor<IReplyMarkupHandler>(serviceProvider.GetServices<IReplyMarkupHandler>()).GetHandler(currentMessage));
		}

		else if (!Contexts.ContainsKey(chatId))
		{
			ResetUserContext(chatId);
		}

		Contexts[chatId].CurrentMessage = currentMessage;
		return Contexts[chatId];
	}

	private void ResetUserContext(long chatId)
	{
		Contexts[chatId] = new UserContext(chatId, botClient, serviceProvider);
	}

	private void ResetUserContext(long chatId, IState? state)
	{
		Contexts[chatId] = new UserContext(chatId, botClient, serviceProvider, state);
	}
}