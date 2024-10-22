using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("7737475347:AAE0WP2ueyWN9kHrljk_O_pEzrY3q3MTPVE");
var me = await bot.GetMeAsync();
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel();

async Task OnMessage(Message msg, UpdateType type)
{
	if (msg.Text is null) return;
	Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
	await bot.SendTextMessageAsync(msg.Chat, $"{msg.From} said: {msg.Text}");
}