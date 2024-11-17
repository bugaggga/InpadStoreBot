using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using InpadBotService.DataBuilder;

namespace InpadBotService;

public class UserContext
{
	public long ChatId { get; }
	public string CurrentMessage { get; set; }
	public IState CurrentState {  get; private set; }
	public int PreviousMessageId { get; private set; }
	public Queue<int> UsersMessageId { get; private set; } = new Queue<int>();
	public IServiceProvider ServiceProvider { get; }
	public UserData data = new UserData();  //на подумать 

	public UserContext(long chatId, ITelegramBotClient botClient, IServiceProvider serviceProvider, IState? state = null)
	{
		ChatId = chatId;
		CurrentMessage = string.Empty;
		CurrentState = state ?? new StartMessageHandler(botClient);
		ServiceProvider = serviceProvider;
	}

	public IState SetState(IState newState)
	{
		CurrentState = newState;
		return CurrentState;
	}

	public async Task HandleMessageAsync(Update update, CancellationToken cancellationToken)
	{
		await CurrentState.HandleAsync(new TelegramRequest(update), cancellationToken, this);
	}

	public void SaveBotMessageId(int newMessageId)
	{
		PreviousMessageId = newMessageId;
	}

	public void ClearData()
	{
		data.Clear();
	}
}
