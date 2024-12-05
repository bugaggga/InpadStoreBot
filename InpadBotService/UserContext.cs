using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using InpadBotService.DatasFuncs;
using Telegram.Bot.Types.Enums;

namespace InpadBotService;

/// <summary>
/// Контексты диалога пользователя с ботом.
/// </summary>
public class UserContext
{
	public long ChatId { get; }
	public string CurrentMessage { get; set; }
	public IState CurrentState {  get; private set; }
	public int PreviousMessageId { get; private set; }
	public int PreviosUserMessageId { get; private set; }
	public IServiceProvider ServiceProvider { get; }
	public UserData data = new UserData();  //на подумать 
	public UpdateType ExpectedType { get; set; }

	public UserContext(long chatId, ITelegramBotClient botClient, IServiceProvider serviceProvider, IState? state = null)
	{
		ChatId = chatId;
		CurrentMessage = string.Empty;
		CurrentState = state ?? new StartMessageState(botClient);
		ServiceProvider = serviceProvider;
		ExpectedType = UpdateType.Message;
        data.Clear();
    }

	public IState SetState(IState newState)
	{
		CurrentState = newState;
		return CurrentState;
	}

	public async Task<int> HandleMessageAsync(Update update, CancellationToken cancellationToken)
	{
		return await CurrentState.HandleAsync(new TelegramRequest(update), cancellationToken, this);
	}

	public void SaveBotMessageId(int newMessageId)
	{
		PreviousMessageId = newMessageId;
	}

	public void SaveUserMessageId(int newUserMessageId)
	{
		PreviosUserMessageId = newUserMessageId;
	}

	public void ClearData()
	{
		data.Clear();
	}
}
