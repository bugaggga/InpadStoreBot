using InpadBotService.DatasFuncs;
using InpadBotService.HelpButton;
using InpadBotService.SupportButton;
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

namespace InpadBotService.LicenseButton
{
    public interface ILicenseState : IState;

    /// <summary>
    /// Обработчик который спрашивает ФИО пользователя
    /// </summary>
    internal class LBSendFIOState : ILicenseState
    {
        public string Message { get; } = "LBSendFIO";
        private readonly ITelegramBotClient _botClient;
        public LBSendFIOState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");
            var query = request.Update.CallbackQuery;

            DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

            context.SetState(new HelpQuestionRengaLicenseState(_botClient));

            await _botClient.AnswerCallbackQuery(
                query.Id);

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите, пожалуйста, информацию о себе, чтобы мы могли связаться с Вами. \n Укажите ваше ФИО."
            );
        }
    }

    /// <summary>
    /// Обработчик, который узнает, Физическое или Юридическое лицо пользователь
    /// </summary>
    internal class LBNaturalOrJuridicalPersonState : ILicenseState
    {
        public string Message { get; } = "LBNaturalOrJuridicalPersonState";
        private readonly ITelegramBotClient _botClient;
        public LBNaturalOrJuridicalPersonState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");
            var query = request.Update.CallbackQuery;

            DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

            context.SetState(new HelpQuestionRengaLicenseState(_botClient));

            await _botClient.AnswerCallbackQuery(
                query.Id);

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Вы Юридическое лицо или Физическое лицо?"
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает название компании
    /// </summary>
    internal class LBNameCompanyState : ILicenseState
    {
        public string Message { get; } = "LBNameCompanyState";
        private readonly ITelegramBotClient _botClient;
        public LBNameCompanyState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");
            var query = request.Update.CallbackQuery;

            DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

            context.SetState(new HelpQuestionRengaLicenseState(_botClient));

            await _botClient.AnswerCallbackQuery(
                query.Id);

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите название вашей компании."
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает город пользователя
    /// </summary>
    internal class LBCityState : ILicenseState
    {
        public string Message { get; } = "LBCityState";
        private readonly ITelegramBotClient _botClient;
        public LBCityState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");
            var query = request.Update.CallbackQuery;

            DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

            context.SetState(new HelpQuestionRengaLicenseState(_botClient));

            await _botClient.AnswerCallbackQuery(
                query.Id);

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите город, в котором вы находитесь."
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает почту пользователя
    /// </summary>
    internal class LBUserEmailState : ILicenseState
    {
        public string Message { get; } = "LBUserEmailState";
        private readonly ITelegramBotClient _botClient;
        public LBUserEmailState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");
            var query = request.Update.CallbackQuery;

            DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

            context.SetState(new HelpQuestionRengaLicenseState(_botClient));

            await _botClient.AnswerCallbackQuery(
                query.Id);

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите вашу почту."
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает номер телефона пользовтеля
    /// </summary>
    internal class LBUserNumberState : ILicenseState
    {
        public string Message { get; } = "LBUserNumberState";
        private readonly ITelegramBotClient _botClient;
        public LBUserNumberState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");
            var query = request.Update.CallbackQuery;

            DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

            context.SetState(new HelpQuestionRengaLicenseState(_botClient));

            await _botClient.AnswerCallbackQuery(
                query.Id);

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите ваш номер телефона."
            );
        }
    }
    
    /// <summary>
    /// Обработчик, который мпрашивает, какой плагин пользователь хочет приобрести.
    /// </summary>
    internal class LBPluginBuy : ILicenseState
    {
        public string Message { get; } = "LBPluginBuy";
        private readonly ITelegramBotClient _botClient;
        public LBPluginBuy(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");
            var query = request.Update.CallbackQuery;

            DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

            context.SetState(new HelpQuestionRengaLicenseState(_botClient));

            await _botClient.AnswerCallbackQuery(
                query.Id);

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Напишите плагины, которые вы хотите приобрести."
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает, нужна ли демонстрация плагина
    /// </summary>
    internal class LBDemoState : ILicenseState
    {
        public string Message { get; } = "LBDemoState";
        private readonly ITelegramBotClient _botClient;
        public LBDemoState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");
            var query = request.Update.CallbackQuery;

            var pairs = new[] {
            ("Да", "Yes"),
            ("Нет", "No")
            };
            var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

            context.SetState(new SupportFinalState(_botClient));

            await _botClient.AnswerCallbackQuery(
                query.Id);

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Нужна ли вам демонстрация плагина?",
                replyMarkup: inlineKeyboardMarkup
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает сколько лицензий хочет купить пользователь
    /// </summary>
    internal class LBCountLicenseState : ILicenseState
    {
        public string Message { get; } = "LBCountLicenseState";
        private readonly ITelegramBotClient _botClient;
        public LBCountLicenseState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");
            var query = request.Update.CallbackQuery;

            DataBuilder.UpdateData(context, Message);  // Сохранение названия плагинов в Data

            context.SetState(new HelpQuestionRengaLicenseState(_botClient));

            await _botClient.AnswerCallbackQuery(
                query.Id);

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите количество лиценнзий, которые вы ходите приобрести."
            );
        }
    }

    //Обработчик, который пишет: "Ваша информация передана менеджеру."
}
