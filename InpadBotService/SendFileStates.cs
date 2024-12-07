using InpadBotService.DatasFuncs;
using InpadBotService.HelpButton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace InpadBotService;

public interface ISendingFileState : IState;

internal class HQSendFileState : ISendingFileState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "send";

	public HQSendFileState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;

		Console.WriteLine("Start Execute command");
        //DataBuilder.UpdateData(context, Message);

		context.SetState(new HelpFinalState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Прикрепите файл сюда.",
			request.QueryId
		);

	}
}

internal class HQDontSendFileState : ISendingFileState
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "dont send";

	public HQDontSendFileState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;

		Console.WriteLine("Start Execute command");
        //DataBuilder.UpdateData(context, Message);

		return await context.SetState(new HelpFinalState(_botClient)).HandleAsync(request, cancellationToken, context);
	}


}

internal class HelpFinalState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "questionFinal";

    public HelpFinalState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        Console.WriteLine("Start Execute command");

		context.data.AddPair(Message, context.CurrentMessage);
		//Console.WriteLine(DataBuilder.Build(context));
		// Нужно сохранить файл(если есть) в Data и отправить Data в техподдержку

		context.SetState(new DistributorState<IReplyMarkupState>(
			context.ServiceProvider.GetServices<IReplyMarkupState>()));

		await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: $"Данный запрос был передан отделу разработок, в ближайшее время с вами свяжется специалист.",
			request.QueryId
		);

		return 0;
    }
}
