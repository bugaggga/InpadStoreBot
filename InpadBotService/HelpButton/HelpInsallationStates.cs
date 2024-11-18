using InpadBotService.DatasFuncs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InpadBotService.HelpButton;

internal class InstallationCategoryState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "helpInstall";

    public InstallationCategoryState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);

        var pairs = new[] {
            ("Ошибка при установке сборки", "Error installing the assembly"),
            ("Не получается зарегистрироваться", "Can't register"),
            ("Не получается ввести ключ продукта", "Can't enter the product key")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите категорию по которой вам нужна поморщь.",
            replyMarkup: inlineKeyboardMarkup);

        context.SetState(new DistributorState<IHelpReportCategoryPlugin>(
            context.ServiceProvider.GetServices<IHelpReportCategoryPlugin>()));

    }
}

internal class InstallationRevitVersionState : IState
{
    public string Message { get; } = "InstallationCategoryState";
    private readonly ITelegramBotClient _botClient;
    public InstallationRevitVersionState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);

        var pairs = new[] {
            ("Revit 2019", "Revit2019"),
            ("Revit 2020", "Revit2020"),
            ("Revit 2021", "Revit2021"),
            ("Revit 2022", "Revit2022"),
            ("Revit 2023", "Revit2023"),
            ("Revit 2024", "Revit2024"),
            ("Revit 2025", "Revit2025")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите версию Revit, в котором запускали плагин.",
            replyMarkup: inlineKeyboardMarkup
        );

        context.SetState(new InstallationLicenseKeyState(_botClient));
    }
}

internal class InstallationLicenseKeyState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationLicenseKeyState";

    public InstallationLicenseKeyState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Введите, пожалуйста, ваш лицензионный ключ, который вы использовали."
        );

        context.SetState(new InstallationRevitVersionState(_botClient));
    }
}

internal class InstallationBuildNumberState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationBuildNumberState";

    public InstallationBuildNumberState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Напишите, пожалуйста, номер сборки, которую вы установили."
        );

        context.SetState(new InstallationSendFileState(_botClient));
    }
}

internal class InstallationSendFileState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationSendFileAndDiscribeProblem";

    public InstallationSendFileState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // Сохранение вопроса в Data

        var pairs = new[] {
            ("Отправить файл", "Send")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Отправьте, пожалуйста, скриншот с проблемой.",
            replyMarkup: inlineKeyboardMarkup
        );

        context.SetState(new DistributorState<ISendingFileState>(
            context.ServiceProvider.GetServices<ISendingFileState>()));
    }
}

internal class InstallationDescripeProblemState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationSendFileAndDiscribeProblem";

    public InstallationDescripeProblemState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Опишите вашу проблему."
        );

        context.SetState(new InstallationSendFileState(_botClient));
    }
}

internal class InstallationFinaleState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationSendFileAndDiscribeProblem";

    public InstallationFinaleState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);

        await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Данная ошибка была передана отделу разработок, в ближайшее время с вами свяжется специалист."
        );

        context.SetState(new InstallationSendFileState(_botClient));
    }
}