using InpadBotService.HelpButton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService.QuestionButton;

internal class SendQuestionState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public SendQuestionState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.CallbackQuery is not { } query) return;
        //if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        var query = request.Update.CallbackQuery;
		// Сохранение вопроса в Data

		context.SetState(new HelpQuestionLicenseState(_botClient));

		await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Задайте интересующийся Вас вопрос."
        );

    }
}

internal class ReplyQuestionState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public ReplyQuestionState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.CallbackQuery is not { } query) return;
        //if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        var query = request.Update.CallbackQuery;
		// Отправка ответа, созданного нейронкой
		context.SetState(new HelpQuestionLicenseState(_botClient));

		await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Ответ от нейронки",
			newType: UpdateType.Message
		);

    }
}
