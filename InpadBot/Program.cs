using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("7737475347:AAE0WP2ueyWN9kHrljk_O_pEzrY3q3MTPVE");
var task = bot.GetMeAsync();

bot.OnMessage += HandleUpdateAsync;

var me = await task;
Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");

Console.ReadLine();
cts.Cancel();


async Task HandleUpdateAsync(Message sender, UpdateType type)
{
	if (sender.Text == "/start")
	{
		Console.WriteLine($"Received a text message: {sender.Text}");
		// Создание Reply клавиатуры
		var replyKeyboard = new ReplyKeyboardMarkup(new[]
		{
				new KeyboardButton[] { "/help", "/support" },
				new KeyboardButton[] { "/question" }
			})
		{
			ResizeKeyboard = true // Изменяет размер клавиатуры для удобства
		};

		await bot.SendTextMessageAsync(
			chatId: sender.Chat,
			text: "Выберите услугу",
			replyMarkup: replyKeyboard
		);
	}
}