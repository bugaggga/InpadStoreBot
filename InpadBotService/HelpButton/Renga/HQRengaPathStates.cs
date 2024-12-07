using InpadBotService.HelpButton;
using InpadBotService;
using Telegram.Bot;
using InpadBotService.DatasFuncs;

internal class HelpQuestionRengaPluginState : IState
{
	public string Message { get; } = "plugin";
	private readonly ITelegramBotClient _botClient;
	public HelpQuestionRengaPluginState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		context.data.AddPair(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

		context.SetState(new HelpQuestionRengaLicenseState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Введите лицензионный ключ, который у вас есть.",
			request.QueryId
		);
	}
}

internal class HelpQuestionRengaLicenseState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "license";

	public HelpQuestionRengaLicenseState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

		context.data.AddPair(Message, context.CurrentMessage);
        //DataBuilder.UpdateData(context, Message);   // Сохранение лицензионного ключа в Data

		context.SetState(new HelpQuestionRengaVersionState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите версию Renga, в которой вы работаете.",
			request.QueryId
		);
	}
}

internal class HelpQuestionRengaVersionState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "rengaVersion";

	public HelpQuestionRengaVersionState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

		context.data.AddPair(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);   // Сохранение лицензионного ключа в Data

		context.SetState(new HelpQuestionNumberBuildState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите номер сборки плагинов, которую вы использовали.",
			request.QueryId
		);

	}
}

