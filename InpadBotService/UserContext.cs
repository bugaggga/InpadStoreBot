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
	public string CurrentState { get; set; }
	public string CurrentMessage { get; set; }

	private IState currentState {  get; set; }
	public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();

	public UserContext(long userId, ITelegramBotClient botClient)
	{
		UserId = userId;
		CurrentState = "Start"; // start state
		CurrentMessage = string.Empty;
		currentState = new StartMessageHandler(botClient);
	}

	public void SetState(IState newState)
	{
		currentState = newState;
	}

	public async Task HandleMessageAsync(Update update, CancellationToken cancellationToken)
	{
		await currentState.HandleAsync(new TelegramRequest(update), cancellationToken, this);
	}
}
