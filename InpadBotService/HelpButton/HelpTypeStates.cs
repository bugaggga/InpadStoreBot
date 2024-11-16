using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InpadBotService.HelpButton;

public interface IHelpTypeState : IState;

// Этап 1 Пункт 1
internal class QuestionAboutPluginHandler : IHelpTypeState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "askAboutPlugin";

    public QuestionAboutPluginHandler(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        /*
		var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
		{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Renga", "renga"),
					InlineKeyboardButton.WithCallbackData("Конструктив", "construct")

				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Архитектура", "architecture"),
					InlineKeyboardButton.WithCallbackData("Концепция", "concept")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("ОВ и ВК", "ovAndVk"),
					InlineKeyboardButton.WithCallbackData("Общие", "general"),
					InlineKeyboardButton.WithCallbackData("Боксы и отверстия", "boxesAndPoints")
				}
				});
		*/
        var pairs = new[] {
            ("Renga", "renga"),
            ("Конструктив", "construct"),
            ("Архитектура", "architecture"),
            ("Концепция", "concept"),
            ("ОВ и ВК", "ovAndVk"),
            ("Общие", "general"),
            ("Боксы и отверстия", "boxesAndPoints")
            };
        var builder = new InlineKeyboardBuilder(3, 2, pairs); //????????????
        var inlineKeyboardMarkup = builder.Build();

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithDeletePrevBotMessage(
            context,
            query.Message.Chat.Id,
            text: "Выберите\r\nиз какой категории плагин, с которым вам нужна помощь",
            replyMarkup: inlineKeyboardMarkup);

        context.SetState(new DistributorState<IHelpQuestionCategoryPlugin>(
            context.ServiceProvider.GetServices<IHelpQuestionCategoryPlugin>()));

    }
}

// Этап 1 Пункт 2
internal class ReportErrorHandler : IHelpTypeState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "reportError";

    public ReportErrorHandler(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        /*
		var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
		{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Renga", "renga"),
					InlineKeyboardButton.WithCallbackData("Конструктив", "construct")

				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Архитектура", "architecture"),
					InlineKeyboardButton.WithCallbackData("Концепция", "concept")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("ОВ и ВК", "ovAndVk"),
					InlineKeyboardButton.WithCallbackData("Общие", "general"),
					InlineKeyboardButton.WithCallbackData("Боксы и отверстия", "boxesAndPoints")
				}
				});
		*/
        var pairs = new[] {
            ("Renga", "renga"),
            ("Конструктив", "construct"),
            ("Архитектура", "architecture"),
            ("Концепция", "concept"),
            ("ОВ и ВК", "ovAndVk"),
            ("Общие", "general"),
            ("Боксы и отверстия", "boxesAndPoints")
            };
        var builder = new InlineKeyboardBuilder(3, 2, pairs); //????????????
        var inlineKeyboardMarkup = builder.Build();

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithDeletePrevBotMessage(
            context,
            query.Message.Chat.Id,
            text: "Выберите\r\nиз какой категории плагин, с которым вам нужна помощь",
            replyMarkup: inlineKeyboardMarkup);

        context.SetState(new DistributorState<IHelpReportCategoryPlugin>(
            context.ServiceProvider.GetServices<IHelpReportCategoryPlugin>()));

    }
}

// Этап 1 Пункт 3
internal class HelpInstallationHandler : IHelpTypeState
{
    public string Message { get; } = "helpInstall";
    private readonly ITelegramBotClient _botClient;
    public HelpInstallationHandler(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        /*
		var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
		{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Ошибка при установке сборки", "Error")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Не получается зарегистрироваться", "registr")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("не получается ввести ключ продукта", "keyOfProduct")
				}
				});
        */

        var pairs = new[] {
            ("Ошибка при установке сборки", "Error"),
            ("Не получается зарегистрироваться", "registr"),
            ("не получается ввести ключ продукта", "keyOfProduct")
            };
        var builder = new InlineKeyboardBuilder(3, 1, pairs); //????????????
        var inlineKeyboardMarkup = builder.Build();

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithDeletePrevBotMessage(
            context,
            chatId: message.Chat.Id,
            text: "Выводится сообщение: \"Выберите категорию по которой вам нужна поморщь\" ",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

