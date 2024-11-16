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

namespace InpadBotService.HelpButton;

public interface IRevit : IState;

public interface ISendFile : IState;


// Этап 1 Пункт 2.3
internal class FileSendAndErrorDescribe
{

}

// Этап 1 Пункт 3
internal class HelpRevitVersion : IRevit
{
    public string Message { get; } = "helpByDownload";
    private readonly ITelegramBotClient _botClient;
    public HelpRevitVersion(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        /*
        var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
               
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Revit 2019", "Revit2019"),
                    InlineKeyboardButton.WithCallbackData("Revit 2020", "Revit2020"),
                    InlineKeyboardButton.WithCallbackData("Revit 2021", "Revit2021")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Revit 2022", "Revit2022"),
                    InlineKeyboardButton.WithCallbackData("Revit 2023", "Revit2023"),
                    InlineKeyboardButton.WithCallbackData("Revit 2024", "Revit2024")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Revit 2025", "Revit2025")
                }
                });
        */

        var pairs = new[] {
            ("Revit 2019", "Revit2019"),
            ("Revit 2020", "Revit2020"),
            ("Revit 2021", "Revit2021"),
            ("Revit 2022", "Revit2022"),
            ("Revit 2023", "Revit2023"),
            ("Revit 2024", "Revit2024"),
            ("Revit 2025", "Revit2025")
            };
        var builder = new InlineKeyboardBuilder(2, 3, pairs); //????????????
        var inlineKeyboardMarkup = builder.Build();

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithDeletePrevBotMessage(
            context,
            text: "Выберите версию Revit, в котором запускали плагин.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 1 Пункт 3.1
internal class LicenseKey
{

}

// Этап 1 Пункт 3.2
internal class BuildNumber
{

}

// Этап 1 Пункт 3.3
internal class ScrinshotOfError
{

}

// Этап 1 Пункт 3.4
internal class ErrorDescribe
{

}

// Этап 1 Пункт 3.5.1 и 3.5.2
internal class FileSend
{
    public string Message { get; } = "helpByDownload";
    private readonly ITelegramBotClient _botClient;
    public FileSend(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        /*
        var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Не отправлять файл", "dont send file")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Отправить файл", "send file")
                }
                });
        */

        var pairs = new[] {
            ("Не отправлять файл", "dont send file"),
            ("Отправить файл", "send file")
            };
        var builder = new InlineKeyboardBuilder(2, 1, pairs); //????????????
        var inlineKeyboardMarkup = builder.Build();

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessage(
            chatId: message.Chat.Id,
            text: "Отправьте, пожалуйста, файл на котором у вас возник вопрос.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

/*
Если нажата кнопка "Не отправлять файл" выводится сообщение: Данный вопрос был передан отделу разработок, в ближайшее время с вами свяжется специалист
При нажатии кнопки "Отправить файл" выводится сообщение: Прикрепите файл сюда. Потом выводится это: Данный вопрос был передан отделу разработок,
в ближайшее время с вами свяжется специалист
 */


// Вводится ключ

// Пользователь вводит номер версии Renga
internal class RengaVersion
{

}

// Вводится номер сборки плагинов

internal class BuildNumberOfPlugin
{

}

// Дальше смотри в схему