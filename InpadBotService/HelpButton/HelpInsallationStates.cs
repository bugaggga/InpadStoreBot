using InpadBotService.DatasFuncs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace InpadBotService.HelpButton;

internal class MainHelpInstallationState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "helpInstall";

    public MainHelpInstallationState(ITelegramBotClient client)
    {
		_botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.CallbackQuery is not { } query) return;
        //if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        var query = request.Update.CallbackQuery;


		DataBuilder.UpdateData(context, Message);

        var pairs = new[] {
            ("Ошибка при установке сборки", "Error installing the assembly"),
            ("Не получается зарегистрироваться", "Can't register"),
            ("Не получается ввести ключ продукта", "Can't enter the product key")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new HelpInstallationCategoryState(_botClient));

		await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите категорию по которой вам нужна поморщь.",
            replyMarkup: inlineKeyboardMarkup);
    }
}

internal class HelpInstallationCategoryState : IState
{
    public string Message { get; } = "InstallationCategoryState";
    private readonly ITelegramBotClient _botClient;
    public HelpInstallationCategoryState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.CallbackQuery is not { } query) return;
        //if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
		var query = request.Update.CallbackQuery;

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

		context.SetState(new HelpInstallationVersionRevitState(_botClient));

		await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите версию Revit, в котором запускали плагин.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

internal class HelpInstallationVersionRevitState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationVersionRevitState";

    public HelpInstallationVersionRevitState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

		DataBuilder.UpdateData(context, Message);

		context.SetState(new HelpInstallationLicenseKeyState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Введите, пожалуйста, ваш лицензионный ключ, который вы использовали."
        );
    }
}

internal class HelpInstallationLicenseKeyState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationLicenseKeyState";

    public HelpInstallationLicenseKeyState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

		DataBuilder.UpdateData(context, Message);

		context.SetState(new HelpInstallationBuildNumberState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Напишите, пожалуйста, номер сборки, которую вы установили."
        );
    }
}

internal class HelpInstallationBuildNumberState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationBuildNumberState";

    public HelpInstallationBuildNumberState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);   // Сохранение вопроса в Data

        var pairs = new[] {
            ("Отправить файл", "Send")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new HelpInstallationSendFileState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Отправьте, пожалуйста, скриншот с проблемой.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

internal class HelpInstallationSendFileState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationSendFileState";

    public HelpInstallationSendFileState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);

		context.SetState(new HelpInstallationFinaleState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Опишите вашу проблему."
        );
    }
}

internal class HelpInstallationFinaleState : IState
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "InstallationFinaleState";

    public HelpInstallationFinaleState(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        //if (request.Update.Message is null) return;
        Console.WriteLine("Start Execute command");

        DataBuilder.UpdateData(context, Message);

		context.SetState(context.SetState(new DistributorState<IReplyMarkupHandler>(
			context.ServiceProvider.GetServices<IReplyMarkupHandler>())));

		await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Данная ошибка была передана отделу разработок, в ближайшее время с вами свяжется специалист.",
            newType: UpdateType.Message
        );

        return 0;
    }
}