using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService;

public class TgBotBackgroundService : BackgroundService
{
	private readonly ILogger<TgBotBackgroundService> _logger;
	private readonly ITelegramBotClient _botClient;
	private readonly UserContextManager _userContextManager;
	private readonly IEnumerable<IReplyMarkupHandler> _replyMarkupHandlers;

	public TgBotBackgroundService(ILogger<TgBotBackgroundService> logger,
		ITelegramBotClient client,
		UserContextManager contextManager,
		IEnumerable<IReplyMarkupHandler> replyMarkupHandlers)
	{
		_logger = logger;
		_botClient = client;
		_userContextManager = contextManager;
		_replyMarkupHandlers = replyMarkupHandlers;
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
			await _botClient.ReceiveAsync(HandleUpdateAsync,
				HandleErrorAsync,
				receiverOptions: receiverOptions,
				cancellationToken: stoppingToken);
			Console.WriteLine("Worker running at: {time}", DateTimeOffset.Now);
			if (_logger.IsEnabled(LogLevel.Information))
			{
				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			}
			await Task.Delay(1000, stoppingToken);
		}
	}

	async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
	{
		if (update.Message is null) return;

		Console.WriteLine("Start Handle main request");
		var context = _userContextManager.GetOrCreateContext(update.Message.Chat.Id, update.Message.Text!);
		var handlerDistibutor = new HandlerDistributor(_botClient, context, _replyMarkupHandlers);
		var handler = handlerDistibutor.GetHandler(context.CurrentState);
		if (handler != null)
		{
			await handler.Handle(new TelegramRequest(update), cancellationToken, context);
		}
		else
		{
			await _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Неизвестное состояние.");
		}
	}

	private async Task CallbackQueryHandler(CallbackQuery query, CancellationToken cancellationToken)
	{
		if (query.Message is not { } message) return;
		switch (query.Data)
		{
			case "btn1":
				await _botClient.SendTextMessageAsync(message.Chat.Id, $"You've pushed a {query.Data}"); break;
			case "btn2":
				await _botClient.SendTextMessageAsync(message.Chat.Id, $"You've pushed a {query.Data}"); break;
			case "btn3":
				await _botClient.SendTextMessageAsync(message.Chat.Id, $"You've pushed a {query.Data}"); break;
			case "btn4":
				await _botClient.SendTextMessageAsync(message.Chat.Id, $"You've pushed a {query.Data}"); break;
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
}





