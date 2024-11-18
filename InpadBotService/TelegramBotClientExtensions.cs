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

		public static async Task<int> SendMessageWithSaveBotMessageId(this ITelegramBotClient botClient, UserContext context, string text, IReplyMarkup? replyMarkup = null, UpdateType? newType = null)
		{
			await Task.Delay(500);
			var sentMessage = await botClient.SendMessage(
				chatId: context.ChatId,
				text: text,
				replyMarkup: replyMarkup);
			context.ExpectedType = newType ?? context.ExpectedType;
			return sentMessage.MessageId;
		}
	}
}
