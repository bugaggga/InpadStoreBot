using InpadBotService.GigachatMethods;
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

internal class QuestionFinalState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "";

    public QuestionFinalState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.CallbackQuery is not { } query) return;
        //if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");


        // Отправка ответа, созданного нейронкой
        var response = await Gigachat.GetGigachatResponse(context.CurrentMessage);

        context.SetState(new DistributorState<IReplyMarkupState>(
                    context.ServiceProvider.GetServices<IReplyMarkupState>()));

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: response,
            request.QueryId
		);

        return 0;
    }
}
