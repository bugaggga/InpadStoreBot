using InpadBotService.DatasFuncs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InpadBotService.HelpButton;

internal class HelpRepotPluginState : IState
{
	public string Message { get; } = "helpByDownload";
	private readonly ITelegramBotClient _botClient;
	public HelpRepotPluginState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.CallbackQuery is not { } query) return;
		if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);	// Сохранение названия плагинов в Data

        var pairs = new[] {
			("Revit 2019", "Revit2019"),
			("Revit 2020", "Revit2020"),
			("Revit 2021", "Revit2021"),
			("Revit 2022", "Revit2022"),
			("Revit 2023", "Revit2023"),
			("Revit 2024", "Revit2024"),
			("Revit 2025", "Revit2025")
			};
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
			query.Id);

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите версию Revit, в котором запускали плагин.",
			replyMarkup: inlineKeyboardMarkup
		);

		context.SetState(new HelpReportVersionRevitState(_botClient));
	}
}

internal class HelpReportVersionState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "HelpReportVersionState";

	public HelpReportVersionState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.CallbackQuery is not { } query) return;
		if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);	// Сохранение данных в Data

        await _botClient.AnswerCallbackQuery(
			query.Id);

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Введите лицензионный ключ, который у вас есть."
		);

		context.SetState(new HelpReportLicenseState(_botClient));
	}
}

internal class HelpReportLicenseState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "HelpReportLicenseState";

	public HelpReportLicenseState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);	// Сохранение лицензионного ключа в Data

        await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите номер сборки плагинов, которую вы использовали."
		);

		context.SetState(new HelpReportNumberBuildState(_botClient));
	}
}

internal class HelpReportNumberBuildState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "HelpReportNumberBuildState";

	public HelpReportNumberBuildState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // Сохранение номера сборки в Data

        await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Опишите ваш вопрос."
		);

		context.SetState(new HelpReportGetQuestionState(_botClient));
	}
}

internal class HelpReportGetQuestionState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "HelpReportGetQuestionState";

	public HelpReportGetQuestionState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // Сохранение вопроса в Data

        var pairs = new[] {
			("Отправить файл", "Send"),
			("Не отправлять файл", "Dont send")
			};
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Отправьте, пожалуйста, файл на котором у вас возник вопрос.",
			replyMarkup: inlineKeyboardMarkup
		);

		context.SetState(new DistributorState<ISendingFileState>(
			context.ServiceProvider.GetServices<ISendingFileState>()));
	}
}

internal class HelpReportFinalState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "HelpReportFinalState";

	public HelpReportFinalState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

		Console.WriteLine(DataBuilder.Build(context));// Нужно сохранить файл(если есть) в Data и отправить Data в техподдержку

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Данный вопрос был передан отделу разработок, в ближайшее время с вами свяжется специалист."
		);
	}
}


