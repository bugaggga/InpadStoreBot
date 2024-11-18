using InpadBotService.DatasFuncs;
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
        DataBuilder.UpdateData(context, Message);

        await _botClient.AnswerCallbackQuery(
			query.Id);

		await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Прикрепите файл сюда."
		);

		context.SetState(new HelpFinalState(_botClient));
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
        DataBuilder.UpdateData(context, Message);

        await _botClient.AnswerCallbackQuery(
			query.Id);

		await context.SetState(new HelpFinalState(_botClient)).HandleAsync(request, cancellationToken, context);
	}


}

internal class HelpFinalState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "HelpQuestionFinalState";

    public HelpFinalState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        Console.WriteLine("Start Execute command");

        //Console.WriteLine(DataBuilder.Build(context));
        // Нужно сохранить файл(если есть) в Data и отправить Data в техподдержку

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: $"Данный запрос был передан отделу разработок, в ближайшее время с вами свяжется специалист./n{DataBuilder.Build(context)}"
        );

        context.SetState(new DistributorState<IReplyMarkupHandler>(
            context.ServiceProvider.GetServices<IReplyMarkupHandler>()));
    }
}
