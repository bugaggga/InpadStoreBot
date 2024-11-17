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

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        // Сохранение вопроса в Data
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Задайте интересующийся Вас вопрос."
        );

        context.SetState(new HQLicenseState(_botClient));
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

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        // Отправка ответа, созданного нейронкой
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Ответ от нейронки"
        );

        context.SetState(new HQLicenseState(_botClient));
    }
}
