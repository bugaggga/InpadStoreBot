using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InpadBotService;
/// <summary>
/// Класс, который хранит контексты общения пользователя с ботом. Определяет метод создания и получения пользователя.
/// </summary>
public class UserContextManager
{
	public Dictionary<long, UserContext> Contexts { get; } = new();
	private readonly ITelegramBotClient botClient;
	private readonly IServiceProvider serviceProvider;

	public UserContextManager(ITelegramBotClient botClient, IServiceProvider serviceProvider)
	{
		this.botClient = botClient;
		this.serviceProvider = serviceProvider;
	}

	public async Task<UserContext> GetOrCreateContext(long chatId, string currentMessage, int? currentMessageId)
	{
		if (currentMessage.StartsWith('/'))
		{
			//int tempPrevUserMessageId = 0;
			//int tempPrevBotMessageId = 0;
			if (Contexts.ContainsKey(chatId))
			{
				//tempPrevBotMessageId = Contexts[chatId].PreviousMessageId;
				//tempPrevUserMessageId = Contexts[chatId].PreviosUserMessageId;
				await botClient.DeleteUserMessage(Contexts[chatId], chatId);
				await botClient.DeleteBotMessageAsync(Contexts[chatId], chatId);
			}
			ResetUserContext(chatId, new HandlerDistributor<IReplyMarkupHandler>(serviceProvider.GetServices<IReplyMarkupHandler>()).GetHandler(currentMessage));
			//Contexts[chatId].SaveUserMessageId(tempPrevUserMessageId);
			//Contexts[chatId].SaveBotMessageId(tempPrevBotMessageId);
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