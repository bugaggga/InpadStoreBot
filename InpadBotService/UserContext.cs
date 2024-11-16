using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InpadBotService;

public class UserContext
{
	public long ChatId { get; }
	public string CurrentMessage { get; set; }
	public IState CurrentState {  get; private set; }
	public int PreviousMessageId { get; private set; }
	public Queue<int> UsersMessageId { get; private set; } = new Queue<int>();
	public IServiceProvider ServiceProvider { get; }
	//public StringBuilder data { get; } = new StringBuilder();  на подумать 
	//public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();

	public UserContext(long chatId, ITelegramBotClient botClient, IServiceProvider serviceProvider, IState? state = null)
	{
		ChatId = chatId;
		CurrentMessage = string.Empty;
		CurrentState = state ?? new StartMessageHandler(botClient);
		ServiceProvider = serviceProvider;
	}

	public void SetState(IState newState)
	{
		CurrentState = newState;
	}

	public async Task HandleMessageAsync(Update update, CancellationToken cancellationToken)
	{
		await CurrentState.HandleAsync(new TelegramRequest(update), cancellationToken, this);
	}

	public void SaveBotMessageId(int newMessageId)
	{
		PreviousMessageId = newMessageId;
	}
}
