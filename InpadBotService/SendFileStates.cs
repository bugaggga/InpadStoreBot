using InpadBotService.HelpButton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InpadBotService;

public interface ISendingFileState : IState;

internal class HQSendFileState : ISendingFileState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "Send";

	public HQSendFileState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.CallbackQuery is not { } query) return;
		if (query.Message is not { } message) return;

		Console.WriteLine("Start Execute command");

		await _botClient.AnswerCallbackQuery(
			query.Id);

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Прикрепите файл сюда."
		);

		context.SetState(new HelpQuestionFinalState(_botClient));
	}
}

internal class HQDontSendFileState : ISendingFileState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "Dont send";

	public HQDontSendFileState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		if (request.Update.CallbackQuery is not { } query) return;
		if (query.Message is not { } message) return;

		Console.WriteLine("Start Execute command");

		await _botClient.AnswerCallbackQuery(
			query.Id);

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Прикрепите файл сюда."
		);

		await context.SetState(new HelpQuestionFinalState(_botClient)).HandleAsync(request, cancellationToken, context);
	}
}
