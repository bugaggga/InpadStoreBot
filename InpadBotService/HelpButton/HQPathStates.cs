using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InpadBotService.HelpButton;

internal class HQPluginState : IState
{
    public string Message { get; } = "helpByDownload";
    private readonly ITelegramBotClient _botClient;
    public HQPluginState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        // Сохранение названия плагинов в Data
        var pairs = new[] {
            ("Revit 2019", "Revit2019"),
            ("Revit 2020", "Revit2020"),
            ("Revit 2021", "Revit2021"),
            ("Revit 2022", "Revit2022"),
            ("Revit 2023", "Revit2023"),
            ("Revit 2024", "Revit2024"),
            ("Revit 2025", "Revit2025")
            };
        var builder = new InlineKeyboardBuilder(3, 2, pairs); 
        var inlineKeyboardMarkup = builder.Build();

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите версию Revit, в котором запускали плагин.",
            replyMarkup: inlineKeyboardMarkup
        );

        context.SetState(new HQVersionRevitState(_botClient));
    }
}

internal class HQVersionRevitState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQVersionRevitState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        // Сохранение данных в Data
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Введите лицензионный ключ, который у вас есть."
        );

        context.SetState(new HQLicenseState(_botClient));
    }
}

internal class HQLicenseState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQLicenseState (ITelegramBotClient client)
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

        context.SetState(new HQNumberBuildState(_botClient));
    }
}

internal class HQNumberBuildState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQNumberBuildState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");
        // Сохранение номера сборки в Data

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Опишите ваш вопрос."
        );

        context.SetState(new HQGetQuestionState(_botClient));
    }
}

internal class HQGetQuestionState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQGetQuestionState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");
        // Сохранение номера сборки в Data

		Console.WriteLine("Start Execute command");
        // Сохранение вопроса в Data
        var pairs = new[] {
            ("Отправить файл", "Send"),
            ("Не отправлять файл", "Dont send")
            };
        var builder = new InlineKeyboardBuilder(2, 1, pairs);
        var inlineKeyboardMarkup = builder.Build();

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Прикрепите файл сюда."
        );
    }
}

internal class HQFinalState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public HQFinalState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");
        // Нужно сохранить файл(если есть) в Data и отправить Data в техподдержку

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Данный вопрос был передан отделу разработок, в ближайшее время с вами свяжется специалист."
		);
    }
}