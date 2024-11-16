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
		public static async Task DeleteBotMessageAsync(this ITelegramBotClient botClient, UserContext context, long chatId, int messageId)
		{
			if (context.PreviousMessageId > 0)
			{
				await botClient.DeleteMessage(chatId, messageId);
				context.SaveBotMessageId(0);
			}
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
