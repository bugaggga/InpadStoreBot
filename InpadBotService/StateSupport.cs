using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService;


// Этап 2
internal class SupportMessageHandler : IReplyMarkupHandler 
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "/support";

	public SupportMessageHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
		{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("supportButton 1", "btn1"),
					InlineKeyboardButton.WithCallbackData("supportButton 2", "btn2")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("SupportButton 3", "btn3"),
					InlineKeyboardButton.WithCallbackData("SupportButton 4", "btn4")
				}
				});

		await _botClient.SendTextMessageAsync(
				request.Update.Message.Chat.Id,
		text: "Выберите кнопку:",
		replyMarkup: inlineKeyboardMarkup);

		context.SetState(new DistributorState<IHelpTypeAnswerHandler>(
			context.ServiceProvider.GetServices<IHelpTypeAnswerHandler>()));
	}
}