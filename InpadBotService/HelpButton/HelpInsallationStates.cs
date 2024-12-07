using InpadBotService.DatasFuncs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace InpadBotService.HelpButton;

internal class HelpInstallationCategoryState : IState
{
    public string Message { get; } = "installationCategory";
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

        context.UpdateData(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);

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

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите версию Revit, в котором запускали плагин.",
			request.QueryId,
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

		context.UpdateData(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);

		context.SetState(new HelpInstallationLicenseKeyState(_botClient));

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Введите, пожалуйста, ваш лицензионный ключ, который вы использовали.",
            request.QueryId
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

		context.UpdateData(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);

		context.SetState(new HelpInstallationBuildNumberState(_botClient));

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Напишите, пожалуйста, номер сборки, которую вы установили.",
            request.QueryId
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

		context.UpdateData(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);   // Сохранение вопроса в Data

		//var pairs = new[] {
		//    ("Отправить файл", "Send")
		//    };
		//var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new HelpInstallationSendFileState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Отправьте, пожалуйста, скриншот с проблемой.",
            request.QueryId
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

		context.UpdateData(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);

		context.SetState(new HelpInstallationFinaleState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Опишите вашу проблему.",
            request.QueryId
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

		context.UpdateData(Message, context.CurrentMessage);
		//DataBuilder.UpdateData(context, Message);

		context.SetState(context.SetState(new DistributorState<IReplyMarkupState>(
			context.ServiceProvider.GetServices<IReplyMarkupState>())));

		await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Данная ошибка была передана отделу разработок, в ближайшее время с вами свяжется специалист.",
            request.QueryId
        );

        return 0;
    }
}