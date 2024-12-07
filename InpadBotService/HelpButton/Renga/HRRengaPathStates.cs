using InpadBotService.HelpButton;
using InpadBotService;
using Telegram.Bot;
using InpadBotService.DatasFuncs;

internal class HelpReportRengaPluginState : IState
{
	public string Message { get; } = "plugin";
	private readonly ITelegramBotClient _botClient;
	public HelpReportRengaPluginState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		context.data.AddPair(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);   // Сохранение названия плагинов в Data

		context.SetState(new HelpReportRengaLicenseState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Введите лицензионный ключ, который у вас есть.",
			request.QueryId
		);
	}
}

internal class HelpReportRengaLicenseState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "license";

	public HelpReportRengaLicenseState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

		context.UpdateData(Message, context.CurrentMessage);
        //DataBuilder.UpdateData(context, Message);   // Сохранение лицензионного ключа в Data

		context.SetState(new HelpReportRengaVersionState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите версию Renga, в которой вы работаете.",
			request.QueryId
		);
	}
}

internal class HelpReportRengaVersionState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "rengaVersion";

	public HelpReportRengaVersionState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

		context.UpdateData(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);   // Сохранение лицензионного ключа в Data

		context.SetState(new HelpReportNumberBuildState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите номер сборки плагинов, которую вы использовали.",
			request.QueryId
		);
	}
}

