using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService;

public class TgBotBackgroundService : BackgroundService
{
	private readonly ILogger<TgBotBackgroundService> _logger;
	private readonly ITelegramBotClient _botClient;
	private readonly UserContextManager _userContextManager;
	private readonly StateDistributor _stateDistributor;

	public TgBotBackgroundService(ILogger<TgBotBackgroundService> logger,
		ITelegramBotClient client,
		UserContextManager contextManager,
		StateDistributor stateDistributor)
	{
		_logger = logger;
		_botClient = client;
		_userContextManager = contextManager;
		_stateDistributor = stateDistributor;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var receiverOptions = new ReceiverOptions
		{
			AllowedUpdates = [],// Allowed type update
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
		string? message;
		long chatId;
		switch (update.Type)
		{
			case UpdateType.Message:
				message = update.Message!.Text!;
				chatId = update.Message!.Chat.Id;
				break;
			case UpdateType.CallbackQuery:
				message = update.CallbackQuery!.Data!;
				chatId = update.CallbackQuery!.From.Id;
				break;
			default:
				message = null;
				chatId = 0;
				break;
		};
		if (message is null) return;

		Console.WriteLine("Start Handle main request");
		var context = _userContextManager.GetOrCreateContext(chatId, message);
		var handler = _stateDistributor.GetHandler(context);
		if (handler != null)
		{
			await handler.HandleAsync(new TelegramRequest(update), cancellationToken, context);
		}
		else
		{
			await _botClient.SendTextMessageAsync(chatId, "����������� ���������.");
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