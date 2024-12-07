using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using InpadBotService.DatasFuncs;
using Telegram.Bot.Types.Enums;

namespace InpadBotService.HelpButton;

public interface IHelpTypeState : IState;

// Этап 1 Пункт 1
internal class QuestionAboutPluginState : IHelpTypeState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "askAboutPlugin";

    public QuestionAboutPluginState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.CallbackQuery is not { } query) return;
        //if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

		var pairs = new[] {
            ("Renga", "renga"),
            ("Конструктив", "construct"),
            ("Архитектура", "architecture"),
            ("Концепция", "concept"),
            ("ОВ и ВК", "ovAndVk"),
            ("Общие", "general"),
            ("Боксы и отверстия", "boxesAndPoints")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new DistributorState<IHelpQuestionCategoryPlugin>(
			context.ServiceProvider.GetServices<IHelpQuestionCategoryPlugin>()));

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите\r\nиз какой категории плагин, с которым вам нужна помощь",
            request.QueryId,
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 1 Пункт 2
internal class ReportErrorState : IHelpTypeState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "reportError";

    public ReportErrorState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.CallbackQuery is not { } query) return;
        //if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

        var pairs = new[] {
            ("Renga", "renga"),
            ("Конструктив", "construct"),
            ("Архитектура", "architecture"),
            ("Концепция", "concept"),
            ("ОВ и ВК", "ovAndVk"),
            ("Общие", "general"),
            ("Боксы и отверстия", "boxesAndPoints")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new DistributorState<IHelpReportCategoryPlugin>(
			context.ServiceProvider.GetServices<IHelpReportCategoryPlugin>()));

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите\r\nиз какой категории плагин, с которым вам нужна помощь",
            request.QueryId,
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 1 Пункт 3
internal class HelpInstallationState : IHelpTypeState
{
    public string Message { get; } = "helpInstall";
    private readonly ITelegramBotClient _botClient;
    public HelpInstallationState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

		var pairs = new[] {
			("Ошибка при установке сборки", "Error installing the assembly"),
			("Не получается зарегистрироваться", "Can't register"),
			("Не получается ввести ключ продукта", "Can't enter the product key")
			};
		var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new HelpInstallationCategoryState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите категорию по которой вам нужна поморщь.",
            request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

