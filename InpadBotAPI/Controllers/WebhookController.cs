using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotWebhook.Controllers
{
	[ApiController]
	[Route("/")]
	public class WebhookController : ControllerBase
	{
		//private readonly ITelegramBotClient _botClient;

		//public WebhookController(ITelegramBotClient botClient)
		//{
		//	_botClient = botClient;
		//}

		[Route("Start")]
		[HttpPost]
		public void Post(Update update)
		{
			Console.WriteLine(update.Message.Text);
			//if (update == null)
			//{
			//	return BadRequest();
			//}

			//if (update.Message != null)
			//{
			//	var chatId = update.Message.Chat.Id;
			//	var messageText = update.Message.Text;

			//	// Пример обработки текстового сообщения
			//	if (messageText == "/start")
			//	{
			//		await _botClient.SendTextMessageAsync(chatId, "Привет! Это бот, использующий Webhook!");
			//	}
			//	else
			//	{
			//		await _botClient.SendTextMessageAsync(chatId, $"Вы отправили: {messageText}");
			//	}
			//}

			//return Ok();
		}

		[HttpGet]
		public string Get()
		{
			//Здесь мы пишем, что будет видно если зайти на адрес,
			//указаную в ngrok и launchSettings
			return "Telegram bot was started";
		}
	}
}
