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

// Этап 3
internal class QuestionMessageHandler : IReplyMarkupHandler
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "/question";

	public QuestionMessageHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		await _botClient.SendMessage(
			chatId: request.Update.Message.Chat.Id,
			text: "Выберите услугу"
		);

		context.SetState(new DistributorState<IReplyMarkupHandler>(
			context.ServiceProvider.GetServices<IReplyMarkupHandler>()));
	}
}