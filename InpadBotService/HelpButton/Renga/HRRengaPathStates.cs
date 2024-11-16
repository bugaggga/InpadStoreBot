using InpadBotService.HelpButton;
using InpadBotService;
using Telegram.Bot;

internal class HRRengaPluginState : IState
{
	public string Message { get; } = "helpByDownload";
	private readonly ITelegramBotClient _botClient;
	public HRRengaPluginState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.CallbackQuery is not { } query) return;
		if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		// Сохранение названия плагинов в Data
		await _botClient.AnswerCallbackQuery(
			query.Id);

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Введите лицензионный ключ, который у вас есть."
		);

		context.SetState(new HRRengaLicenseState(_botClient));
	}
}

internal class HRRengaLicenseState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "";

	public HRRengaLicenseState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");
		// Сохранение лицензионного ключа в Data

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите версию Renga, в которой вы работаете."
		);

		context.SetState(new HRRengaVersionState(_botClient));
	}
}

internal class HRRengaVersionState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "";

	public HRRengaVersionState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");
		// Сохранение лицензионного ключа в Data

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите номер сборки плагинов, которую вы использовали."
		);

		context.SetState(new HRNumberBuildState(_botClient));
	}
}

