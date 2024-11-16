using InpadBotService.HelpButton;
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
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService;

public interface IState
{
	public string Message { get; }
	public Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context);
}

public interface IReplyMarkupHandler : IState;

public record TelegramRequest(Update Update);

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

		await _botClient.SendMessageWithSaveBotMessageId(
            context,
			text: "Нажмите на кнопку, которая Вам требуется.",
			replyMarkup: replyKeyboard
		);

		context.SetState(new DistributorState<IReplyMarkupHandler>(
			context.ServiceProvider.GetServices<IReplyMarkupHandler>()));
	}
}

// Этап 1
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
		
		var pairs = new[] {
			("Хочу\r\nзадать вопрос касаемо работы плагина", "askAboutPlugin"),
			("Хочу\r\nсообщить об ошибке", "reportError"),
			("Нужна\r\nпомощь при установке/активации", "helpInstall")
			};
		var builder = new InlineKeyboardBuilder(3, 1, pairs);
		var inlineKeyboardMarkup = builder.Build();

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите\r\nпункт, по которому вам нужна помощь:",
			replyMarkup: inlineKeyboardMarkup);

		context.SetState(new DistributorState<IHelpTypeState>(
			context.ServiceProvider.GetServices<IHelpTypeState>()));
	}
}

// Этап 2
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
		if (request.Update.Message is null) return;
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
		var builder = new InlineKeyboardBuilder(3, 2, pairs); //????????????
		var inlineKeyboardMarkup = builder.Build();

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите категорию, в котором находится плагин.",
			replyMarkup: inlineKeyboardMarkup);

		context.SetState(new DistributorState<ISupportCategoryPluginState>(
			context.ServiceProvider.GetServices<ISupportCategoryPluginState>()));

	}
}

// Этап 3
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

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите услугу"
		);

		context.SetState(new DistributorState<IReplyMarkupHandler>(
			context.ServiceProvider.GetServices<IReplyMarkupHandler>()));
	}
}
