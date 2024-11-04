using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService;

public interface IState
{
	public string Message { get; }
	public Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context);
}

//interface IMessageHandler : IState
//{
//	//public Task Handle(TelegramRequest request, CancellationToken cancellationToken);
//}

public interface IReplyMarkupHandler : IState;

public interface IHelpTypeAnswerHandler : IState;


public class StartMessageHandler : IReplyMarkupHandler
{
	public string Message { get; } = "/start";
	private readonly ITelegramBotClient _botClient;
	public StartMessageHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		var replyKeyboard = new ReplyKeyboardMarkup(new[]
		{
				new KeyboardButton[] { "/help", "/support" },
				new KeyboardButton[] { "/question" }
			})
		{
			ResizeKeyboard = true
		};

		await _botClient.SendTextMessageAsync(
			chatId: request.Update.Message.Chat.Id,
			text: "Выберите услугу",
			replyMarkup: replyKeyboard
		);
	}
}

internal class HelpMessageHandler : IReplyMarkupHandler
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "/help";
	public HelpMessageHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
		{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Хочу\r\nзадать вопрос касаемо работы плагина", "helpByWorkOrError"),
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Хочу\r\nсообщить об ошибке", "helpByWorkOrError")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Нужна\r\nпомощь при установке/активации", "helpByDownload")
				}
				});

		await _botClient.SendTextMessageAsync(
				request.Update.Message.Chat.Id,
		text: "Выберите\r\nпункт, по которому вам нужна помощь:",
		replyMarkup: inlineKeyboardMarkup);

		context.CurrentState = "WaitingHelpTypeCallback";
	}
}

internal class SupportMessageHandler : IReplyMarkupHandler 
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "/support";

	public SupportMessageHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
		{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("supportButton 1", "btn1"),
					InlineKeyboardButton.WithCallbackData("supportButton 2", "btn2")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("SupportButton 3", "btn3"),
					InlineKeyboardButton.WithCallbackData("SupportButton 4", "btn4")
				}
				});

		await _botClient.SendTextMessageAsync(
				request.Update.Message.Chat.Id,
		text: "Выберите кнопку:",
		replyMarkup: inlineKeyboardMarkup);
		context.CurrentState = "WaitingCallback";
	}
}

internal class QuestionMessageHandler : IReplyMarkupHandler
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "/question";

	public QuestionMessageHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		var replyKeyboard = new ReplyKeyboardMarkup(new[]
		{
				new KeyboardButton[] { "/help", "/support" },
				new KeyboardButton[] { "/question" }
			})
		{
			ResizeKeyboard = true
		};

		await _botClient.SendTextMessageAsync(
			chatId: request.Update.Message.Chat.Id,
			text: "Выберите услугу",
			replyMarkup: replyKeyboard
		);
		context.CurrentMessage = "WaitingForInput";
	}
}

internal class HelpTypeHandler : IHelpTypeAnswerHandler
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "helpByWorkOrError";

	public HelpTypeHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.CallbackQuery is not { } query) return;
		if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");
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
		await _botClient.AnswerCallbackQueryAsync(
			query.Id);

		await _botClient.SendTextMessageAsync(
				query.Message.Chat.Id,
		text: "Выберите\r\nиз какой категории плагин, с которым вам нужна помощь",
		replyMarkup: inlineKeyboardMarkup);

		context.CurrentState = "WaitingCategoryCallback";
	}
}

internal class HelpDownloadHandler : IHelpTypeAnswerHandler
{
	public string Message { get; } = "helpByDownload";
	private readonly ITelegramBotClient _botClient;
	public HelpDownloadHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.CallbackQuery is not { } query) return;
		if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

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
		await _botClient.AnswerCallbackQueryAsync(
			query.Id);

		await _botClient.SendTextMessageAsync(
			chatId: message.Chat.Id,
			text: "Выводится сообщение: \"Выберите категорию по которой вам нужна поморщь\" ",
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

public record TelegramRequest(Update Update);