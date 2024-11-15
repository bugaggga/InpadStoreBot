using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InpadBotService
{
	internal static class TelegramBotClientExtensions
	{
		public static async Task DeleteBotMessageAsync(this ITelegramBotClient botClient, UserContext context, long chatId, int messageId)
		{
			if (context.PreviousMessageId > 0) await botClient.DeleteMessage(chatId, messageId);
		}

		//public static async SendMessageWithDeletePrevBotMessage(this ITelegramBotClient botClient, UserContext context, long chatId, int messageId)
	}
}
