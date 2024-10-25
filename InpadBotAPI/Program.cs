using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Регистрация клиента TelegramBotClient
//builder.Services.AddHttpClient("telegram_bot")
//	.AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
//		new TelegramBotClient("7737475347:AAE0WP2ueyWN9kHrljk_O_pEzrY3q3MTPVE", httpClient)); // Вставь сюда свой токен

builder.Services.AddControllers();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//	var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
//	var webhookUrl = "https://198.16.70.53/api/Webhook/Start";  // Укажи здесь URL
//	await botClient.SetWebhookAsync(webhookUrl);
//	Console.WriteLine($"Webhook установлен на {webhookUrl}");
//}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();