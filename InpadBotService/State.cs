using InpadBotService.HelpButton;
using InpadBotService.QuestionButton;
using InpadBotService.SupportButton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService;

public interface IState
{
	public string Message { get; }
	public Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context);
}

public interface IReplyMarkupState : IState;

public record TelegramRequest(Update Update);

public class StartMessageState : IReplyMarkupState
{
	public string Message { get; } = "/start";
	private readonly ITelegramBotClient _botClient;
	public StartMessageState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		//if (request.Update.Message is null) return;

		var replyKeyboard = new ReplyKeyboardMarkup(new[]
		{
				new KeyboardButton[] { "/help", "/support" },
				new KeyboardButton[] { "/question" }
			})
		{
			ResizeKeyboard = true,
			OneTimeKeyboard = false, // Клавиатура не исчезает после нажатия
		};

		context.SetState(new DistributorState<IReplyMarkupState>(
			context.ServiceProvider.GetServices<IReplyMarkupState>()));

		return await _botClient.SendMessageWithSaveBotMessageId(
            context,
			text: "Нажмите на кнопку, которая Вам требуется.",
			replyMarkup: replyKeyboard
		);

		
		
	}
}

// Этап 1
internal class HelpMessageState : IReplyMarkupState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "/help";
	public HelpMessageState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		//if (request.Update.Message is null) return;
		
		var pairs = new[] {
			("Хочу\r\nзадать вопрос касаемо работы плагина", "askAboutPlugin"),
			("Хочу\r\nсообщить об ошибке", "reportError"),
			("Нужна\r\nпомощь при установке/активации", "helpInstall")
			};
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new DistributorState<IHelpTypeState>(
			context.ServiceProvider.GetServices<IHelpTypeState>()));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите\r\nпункт, по которому вам нужна помощь:",
			replyMarkup: inlineKeyboardMarkup
		);

	}
}

// Этап 2
internal class SupportMessageState : IReplyMarkupState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "/support";

	public SupportMessageState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.Message is null) return;
		Console.WriteLine("Start Execute command");

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

		context.SetState(new DistributorState<ISupportCategoryPluginState>(
			context.ServiceProvider.GetServices<ISupportCategoryPluginState>()));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите категорию, в котором находится плагин.",
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

internal class QuestionMessageState : IReplyMarkupState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "/question";

    public QuestionMessageState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.CallbackQuery is not { } query) return;
        //if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        var query = request.Update.CallbackQuery;
        // Сохранение вопроса в Data

        context.SetState(new QuestionFinalState(_botClient));

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Задайте интересующийся Вас вопрос."
        );

    }
}

