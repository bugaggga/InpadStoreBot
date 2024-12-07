using InpadBotService.DatasFuncs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace InpadBotService.HelpButton;

internal class HelpRepotPluginState : IState
{
	public string Message { get; } = "plugin";
	private readonly ITelegramBotClient _botClient;
	public HelpRepotPluginState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");
		var query = request.Update.CallbackQuery;

		//DataBuilder.UpdateData(context, Message);	// Сохранение названия плагинов в Data

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

		context.SetState(new HelpReportVersionRevitState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите версию Revit, в котором запускали плагин.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

internal class HelpReportVersionRevitState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "versionRevit";

	public HelpReportVersionRevitState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);   // Сохранение данных в Data

		context.SetState(new HelpReportLicenseState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Введите лицензионный ключ, который у вас есть.",
			request.QueryId
		);

	}
}

internal class HelpReportLicenseState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "license";

	public HelpReportLicenseState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        //DataBuilder.UpdateData(context, Message);   // Сохранение лицензионного ключа в Data

		context.SetState(new HelpReportNumberBuildState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Напишите номер сборки плагинов, которую вы использовали.",
			request.QueryId
		);
	}
}

internal class HelpReportNumberBuildState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "numberBuild";

	public HelpReportNumberBuildState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        //DataBuilder.UpdateData(context, Message);   // Сохранение номера сборки в Data

		context.SetState(new HelpReportGetQuestionState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Опишите ваш вопрос.",
			request.QueryId
		);
	}
}

internal class HelpReportGetQuestionState : IState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "question";

	public HelpReportGetQuestionState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

        //DataBuilder.UpdateData(context, Message);   // Сохранение вопроса в Data

        var pairs = new[] {
			("Отправить файл", "Send"),
			("Не отправлять файл", "Dont send")
			};
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new DistributorState<ISendingFileState>(
			context.ServiceProvider.GetServices<ISendingFileState>()));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Отправьте, пожалуйста, файл на котором у вас возник вопрос.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}


