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

/// <summary>
/// Класс сервиса, который опрашивает телеграмм бот на получеие апдейтов.
/// </summary>

public class TgBotBackgroundService : BackgroundService
{
	private readonly ILogger<TgBotBackgroundService> _logger;
	private readonly ITelegramBotClient _botClient;
	private readonly UserContextManager _userContextManager;
	private readonly IServiceProvider serviceProvider;

	public TgBotBackgroundService(ILogger<TgBotBackgroundService> logger,
		ITelegramBotClient client,
		UserContextManager contextManager,
		IServiceProvider serviceProvider)
	{
		_logger = logger;
		_botClient = client;
		_userContextManager = contextManager;
		this.serviceProvider = serviceProvider;
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

		var context = await _userContextManager.GetOrCreateContext(chatId, message, update.Message?.MessageId);
		if (context.ExpectedType == update.Type)
		{
			var sendMessageId = await context.HandleMessageAsync(update, cancellationToken);
			await Task.Delay(100);
			await _botClient.DeleteUserMessage(context, chatId);
			await _botClient.DeleteBotMessageAsync(context, chatId);
			context.SaveUserMessageId(update.Message?.MessageId ?? 0);
			context.SaveBotMessageId(sendMessageId);
		}

		else await UnknownUpdateHandlerAsync(update, cancellationToken, chatId);
	}

	private async Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken, long chatId)
	{
		//_userContextManager.Contexts[chatId].SaveBotMessageId(0);
		_logger.LogInformation("Unknown type message");
		await _botClient.SendMessage(chatId,
			text: "Нераспознанное сообщение"
		);
		//return Task.CompletedTask;
	}

	//private async Task<int> IncorrectMessageHandlerAsync(UserContext context, Update update, CancellationToken cancellationToken, long chatId)
	//{
	//	_logger.LogInformation("Incorrect message");
	//	return await context.CurrentState.HandleAsync(new TelegramRequest(update), cancellationToken, context);
	//}

	Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
	{
		_logger.LogInformation($"Error: {exception.Message}");
		//await _botClient.SendMessage(chatId,
		//	text: "Некорректное сообщение"
		//);
		return Task.CompletedTask;
	}

	private void InitialUpdateComponents(UpdateType type, Update update)
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
	}
}