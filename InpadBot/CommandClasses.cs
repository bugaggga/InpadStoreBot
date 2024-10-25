using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace InpadBot
{
	public interface ICommand
	{
		public TelegramBotClient Client { get; }

		public string Name { get; }

		public async Task Execute(Update update) { }
	}

	public class HelpCommand : ICommand 
	{
		public TelegramBotClient Client => Bot.GetTelegramBot();

		public string Name => "/help";
		public async Task Execute(Update update)
		{ 
			Console.WriteLine("Start Execute command");
			var message = update.Message;
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

			await Client.SendTextMessageAsync(
					chatId: message.Chat.Id,
			text: "Выберите кнопку:",
			replyMarkup: inlineKeyboardMarkup);
		}
	}

	public class SupportCommand : ICommand
	{
		public TelegramBotClient Client => Bot.GetTelegramBot();

		public string Name => "/support";
		public async Task Execute(Update update)
		{
			Console.WriteLine("Start Execute command");
			var message = update.Message;
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

			await Client.SendTextMessageAsync(
					chatId: message.Chat.Id,
			text: "Выберите кнопку:",
			replyMarkup: inlineKeyboardMarkup);
		}
	}


	class Bot
	{
		public static TelegramBotClient Client { get; private set; }

		//public Bot(TelegramBotClient client)
		//{
		//	Client = client;
		//}
		public static TelegramBotClient GetTelegramBot()
		{
			if (Client != null)
			{
				return Client;
			}
			Client = new TelegramBotClient("7737475347:AAE0WP2ueyWN9kHrljk_O_pEzrY3q3MTPVE");
			return Client;
		}
	}
	
}
