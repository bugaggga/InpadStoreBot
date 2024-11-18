using InpadBotService.HelpButton;
using InpadBotService;
using Telegram.Bot;
using InpadBotService.DatasFuncs;

internal class HelpQuestionRengaPluginState : IState
{
	public string Message { get; } = "helpByDownload";
	private readonly ITelegramBotClient _botClient;
	public HelpQuestionRengaPluginState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.CallbackQuery is not { } query) return;
		if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

        await _botClient.AnswerCallbackQuery(
			query.Id);

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Введите лицензионный ключ, который у вас есть."
		);

		context.SetState(new HelpQuestionRengaLicenseState(_botClient));
	}
}

internal class HelpQuestionRengaLicenseState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "HelpQuestionRengaLicenseState";

	public HelpQuestionRengaLicenseState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // Сохранение лицензионного ключа в Data

        await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите версию Renga, в которой вы работаете."
		);

		context.SetState(new HelpQuestionRengaVersionState(_botClient));
	}
}

internal class HelpQuestionRengaVersionState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "HelpQuestionRengaVersionState";

	public HelpQuestionRengaVersionState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // Сохранение лицензионного ключа в Data

        await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите номер сборки плагинов, которую вы использовали."
		);

		context.SetState(new HelpQuestionNumberBuildState(_botClient));
	}
}

