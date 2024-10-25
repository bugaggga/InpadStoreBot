using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService;

public class TgBotBackgroundService : BackgroundService
{
	private readonly ILogger<TgBotBackgroundService> _logger;
	private readonly ITelegramBotClient _client;

	public TgBotBackgroundService(ILogger<TgBotBackgroundService> logger, ITelegramBotClient client)
	{
		_logger = logger;
		_client = client;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var receiverOptions = new ReceiverOptions
		{
			AllowedUpdates = [],// Получать все типы обновлений
			DropPendingUpdates = true
		};

		while (!stoppingToken.IsCancellationRequested)
		{
			await _client.ReceiveAsync(HandleUpdateAsync,
				HandleErrorAsync,
				receiverOptions: receiverOptions,
				cancellationToken: stoppingToken);

			if (_logger.IsEnabled(LogLevel.Information))
			{
				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			}
			await Task.Delay(1000, stoppingToken);
		}
	}

	async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
	{
		Console.WriteLine("Start Handle main request");

		var handler = update switch
		{
			{ Message: { } message } => MessageTextHandler(message, cancellationToken),
			{ CallbackQuery: { } callbackQuery } => CallbackQueryHandler(callbackQuery, cancellationToken),
			_ => UnknownUpdateHandlerAsync(update, cancellationToken)
		};
		await handler;
	}

	private async Task MessageTextHandler(Message message, CancellationToken cancellationToken)
	{
		var chatId = message.Chat.Id;
		switch (message.Text)
		{
			case "/start":
				await SendStartMessageAsync(chatId, cancellationToken);
				break;
			case "/help":
				await SendInlineMarkupAsync(chatId, cancellationToken);
				break;
			case "/support":
				await SendInlineMarkupAsync(chatId, cancellationToken);
				break;
			case "/question":
				await SendInlineMarkupAsync(chatId, cancellationToken);
				break;
		}
	}

	private async Task CallbackQueryHandler(CallbackQuery query, CancellationToken cancellationToken)
	{
		if (query.Message is not { } message) return;
		switch (query.Data)
		{
			case "btn1":
				await _client.SendTextMessageAsync(message.Chat.Id, $"You've pushed a {query.Data}"); break;
			case "btn2":
				await _client.SendTextMessageAsync(message.Chat.Id, $"You've pushed a {query.Data}"); break;
			case "btn3":
				await _client.SendTextMessageAsync(message.Chat.Id, $"You've pushed a {query.Data}"); break;
			case "btn4":
				await _client.SendTextMessageAsync(message.Chat.Id, $"You've pushed a {query.Data}"); break;
		}
	}

	private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Unknown type message");
		return Task.CompletedTask;
	}

	Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
	{
		Console.WriteLine($"Error: {exception.Message}");
		return Task.CompletedTask;
	}

	async Task SendStartMessageAsync(long chatID, CancellationToken cancellationToken)
	{
		Console.WriteLine("Start Execute command");
		var replyKeyboard = new ReplyKeyboardMarkup(new[]
		{
				new KeyboardButton[] { "/help", "/support" },
				new KeyboardButton[] { "/question" }
			})
		{
			ResizeKeyboard = true
		};

		await _client.SendTextMessageAsync(
			chatID,
			text: "Выберите услугу",
			replyMarkup: replyKeyboard
		);
	}

	async Task SendInlineMarkupAsync(long chatId, CancellationToken cancellationToken)
	{
		var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
		{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Button 1", "btn1"),
					InlineKeyboardButton.WithCallbackData("Button 2", "btn2")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Button 3", "btn3"),
					InlineKeyboardButton.WithCallbackData("Button 4", "btn4")
				}
				});

		await _client.SendTextMessageAsync(
				chatId,
		text: "Выберите кнопку:",
		replyMarkup: inlineKeyboardMarkup);
	}

}





