using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService
{
	/// <summary>
	/// Класс, который определяет медоты расширения для класса TelegrammBotClient, а именно метод удаления сообщений бота,
	/// удаление сообщений пользователя и метод отправки сообщения пользователя.
	/// </summary>
	internal static class TelegramBotClientExtensions
	{
		public static async Task DeleteBotMessageAsync(this ITelegramBotClient botClient, UserContext context, long chatId)
		{
			try
			{
				if (context.PreviousMessageId > 0)
				{
					await botClient.DeleteMessage(chatId, context.PreviousMessageId);
					context.SaveBotMessageId(0);
				}
			}
			catch
			{
				context.SaveBotMessageId(0);
				await Task.CompletedTask;
			}
		}

		public static async Task DeleteUserMessage(this ITelegramBotClient botClient, UserContext context, long chatId)
		{
			try
			{
				if (context.PreviosUserMessageId > 0)
				{
					await botClient.DeleteMessage(chatId, context.PreviosUserMessageId);
					context.SaveUserMessageId(0);
				}
			}
			catch
			{
				context.SaveUserMessageId(0);
				await Task.CompletedTask;
			}
			//context.SaveUserMessageId(newUserMessage ?? 0);
		}

		public static async Task<int> SendMessageWithSaveBotMessageId(this ITelegramBotClient botClient, UserContext context, string text, string? queryId, IReplyMarkup? replyMarkup = null)
		{
			if (queryId is not null)
			{
				await botClient.AnswerCallbackQuery(
				queryId);
			}
			//await Task.Delay(200);
			var sentMessage = await botClient.SendMessage(
				chatId: context.ChatId,
				text: text == "" ? "Не найдено" : text,
				replyMarkup: replyMarkup);
			if (replyMarkup is InlineKeyboardMarkup) context.ExpectedType = UpdateType.CallbackQuery;
			else context.ExpectedType = UpdateType.Message;
			return sentMessage.MessageId;
		}
	}
}
