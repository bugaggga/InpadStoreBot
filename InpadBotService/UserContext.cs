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
