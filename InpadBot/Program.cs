using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using InpadBot;
using System.Xml.Linq;
class Program
{
	static TelegramBotClient bot = Bot.GetTelegramBot();
	public async static void Main(string[] args)
	{
		using var cts = new CancellationTokenSource();
		
		var me = await bot.GetMeAsync();
		//UpdateDistributor<CommandExecutor> updateDistributor = new UpdateDistributor<CommandExecutor>();

		var receiverOptions = new ReceiverOptions
		{
			AllowedUpdates = [],// Получать все типы обновлений
			DropPendingUpdates = true
		};

		bot.StartReceiving(
			HandleUpdateAsync,
			HandleErrorAsync,
			receiverOptions,
			cts.Token);

		while (true)
		{
			Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
			await Task.Delay(3000);
		}

		//Console.ReadLine();
		//cts.Cancel();
	}

	static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
	{
		Console.WriteLine("Start Handle main request");
		if (update.Message is not { } message)
		{  //и такое тоже бывает, делаем проверку
			Console.WriteLine("Wrong Message");
			return;
		}

		if (message.Text is not { } messageText)
		{
			return;
		}

		var chatId = message.Chat.Id;

		switch (messageText)
		{
			case "/start":
				await SendStartMessageAsync(chatId, cancellationToken);
				break;
		}

		//await updateDistributor.GetUpdate(update);
	}

	static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
	{
		Console.WriteLine($"Error: {exception.Message}");
		return Task.CompletedTask;
	}

	private static async Task SendStartMessageAsync(long chatID, CancellationToken cancellationToken)
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

		await bot.SendTextMessageAsync(
			chatID,
			text: "Выберите услугу",
			replyMarkup: replyKeyboard
		);
	}
}



