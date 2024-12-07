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
    public interface ILicenseTestState : IState;

    /// <summary>
    /// Обработчик который спрашивает ФИО пользователя
    /// </summary>
    internal class LTSendFIOState : ILicenseTestState
    {
        public string Message { get; } = "LTSendFIO";
        private readonly ITelegramBotClient _botClient;
        public LTSendFIOState(ITelegramBotClient client)
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
    internal class LTNaturalOrJuridicalPersonState : ILicenseTestState
    {
        public string Message { get; } = "LTNaturalOrJuridicalPersonState";
        private readonly ITelegramBotClient _botClient;
        public LTNaturalOrJuridicalPersonState(ITelegramBotClient client)
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
    internal class LTNameCompanyState : ILicenseTestState
    {
        public string Message { get; } = "LTNameCompanyState";
        private readonly ITelegramBotClient _botClient;
        public LTNameCompanyState(ITelegramBotClient client)
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
    internal class LTCityState : ILicenseTestState
    {
        public string Message { get; } = "LTCityState";
        private readonly ITelegramBotClient _botClient;
        public LTCityState(ITelegramBotClient client)
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
    internal class LTUserEmailState : ILicenseTestState
    {
        public string Message { get; } = "LTUserEmailState";
        private readonly ITelegramBotClient _botClient;
        public LTUserEmailState(ITelegramBotClient client)
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
    internal class LTUserNumberState : ILicenseTestState
    {
        public string Message { get; } = "LTUserNumberState";
        private readonly ITelegramBotClient _botClient;
        public LTUserNumberState(ITelegramBotClient client)
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
    internal class LTPluginBuy : ILicenseTestState
    {
        public string Message { get; } = "LTPluginBuy";
        private readonly ITelegramBotClient _botClient;
        public LTPluginBuy(ITelegramBotClient client)
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
                text: "Напишите плагины, которые вы хотите протестировать."
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает сколько лицензий хочет купить пользователь
    /// </summary>
    internal class LTCountLicenseState : ILicenseTestState
    {
        public string Message { get; } = "LTCountLicenseState";
        private readonly ITelegramBotClient _botClient;
        public LTCountLicenseState(ITelegramBotClient client)
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
