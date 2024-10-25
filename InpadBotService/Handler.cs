using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService
{
	internal class WelcomeMessageHandler : IHandler
	{
		private readonly ITelegramBotClient _botClient;	
		public WelcomeMessageHandler(ITelegramBotClient client) 
		{
			_botClient = client;
		}

		public async Task Handle(TelegramRequest request, CancellationToken cancellationToken) 
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

			await _botClient.SendTextMessageAsync(
				chatId: request.Update.Message.Chat.Id,
				text: "Выберите услугу",
				replyMarkup: replyKeyboard
			);
		}
	}

	interface IHandler
	{
		public Task Handle(TelegramRequest request, CancellationToken cancellationToken);
	}

	public record TelegramRequest(Update Update);
}


