using InpadBotService.DatasFuncs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace InpadBotService.HelpButton;

internal class HelpQuestionPluginState : IState
{
    public string Message { get; } = "plugin";
    private readonly ITelegramBotClient _botClient;
    public HelpQuestionPluginState(ITelegramBotClient client)
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

		context.SetState(new HelpQuestionVersionRevitState(_botClient));

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите версию Revit, в котором запускали плагин.",
            request.QueryId,
            replyMarkup: inlineKeyboardMarkup
		);
    }
}

internal class HelpQuestionVersionRevitState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "versionRevit";

    public HelpQuestionVersionRevitState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.CallbackQuery is not { } query) return;
        //if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        //var query = request.Update.CallbackQuery;

        context.data.AddPair(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);   // Сохранение данных в Data

		context.SetState(new HelpQuestionLicenseState(_botClient));

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Введите лицензионный ключ, который у вас есть.",
		    request.QueryId
		);
    }
}

internal class HelpQuestionLicenseState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "license";

    public HelpQuestionLicenseState (ITelegramBotClient client)
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

internal class HelpQuestionNumberBuildState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "numberBuild";

    public HelpQuestionNumberBuildState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

		context.data.AddPair(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);   // Сохранение номера сборки в Data

		context.SetState(new HelpQuestionGetQuestionState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Опишите ваш вопрос.",
            request.QueryId
		);
    }
}

internal class HelpQuestionGetQuestionState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "question";

    public HelpQuestionGetQuestionState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

		context.data.AddPair(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);   // Сохранение вопроса в Data

        var pairs = new[] {
            ("Отправить файл", "send"),
            ("Не отправлять файл", "dont send")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new DistributorState<ISendingFileState>(
			context.ServiceProvider.GetServices<ISendingFileState>()));

		return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Прикрепите файл сюда.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
    }
}