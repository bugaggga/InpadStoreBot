using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService
{
	internal static class TelegramBotClientExtensions
	{
		public static async Task DeleteBotMessageAsync(this ITelegramBotClient botClient, UserContext context, long chatId)
		{
			if (context.PreviousMessageId > 0)
			{
				await botClient.DeleteMessage(chatId, context.PreviousMessageId);
				context.SaveBotMessageId(0);
			}
		}

		public static async Task DeleteUserMessageAndSaveNew(this ITelegramBotClient botClient, UserContext context, long chatId, int? newUserMessage)
		{
			if (context.PreviosUserMessageId > 0)
			{
				await botClient.DeleteMessage(chatId, context.PreviosUserMessageId);
			}
			context.SaveUserMessageId(newUserMessage ?? 0);
		}

		public static async Task SendMessageWithSaveBotMessageId(this ITelegramBotClient botClient, UserContext context, string text, IReplyMarkup? replyMarkup = null)
		{
			var sentMessage = await botClient.SendMessage(
				chatId: context.ChatId,
				text: text,
				replyMarkup: replyMarkup);
			context.SaveBotMessageId(sentMessage.MessageId);
		}
	}
}
