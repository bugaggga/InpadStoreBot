using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InpadBotService;

public class UserContext
{
	public long UserId { get; }
	public string CurrentMessage { get; set; }
	public IState CurrentState {  get; private set; }
	public IServiceProvider ServiceProvider { get; }
	public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();

	public UserContext(long userId, ITelegramBotClient botClient, IServiceProvider serviceProvider, IState? state = null)
	{
		UserId = userId;
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
}
