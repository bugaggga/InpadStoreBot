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

    public interface ITIsNaturalSate : IState;
    /// <summary>
    /// Обработчик который спрашивает ФИО пользователя
    /// </summary>
    internal class LTSendFIOState : ILicenseState
    {
        public string Message { get; } = "testPlugin";
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

            context.SetState(new LTNaturalOrJuridicalPersonState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите, пожалуйста, информацию о себе, чтобы мы могли связаться с Вами. \n Укажите ваше ФИО.",
                request.QueryId
            );
        }
    }

    /// <summary>
    /// Обработчик, который узнает, Физическое или Юридическое лицо пользователь
    /// </summary>
    internal class LTNaturalOrJuridicalPersonState : IState
    {
        public string Message { get; } = "NaturalOrJuridicalPerson";
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

            var pairs = new[] {
            ("Физическое лицо", "natural"),
            ("Юридическое лицо", "juridical")
            };
            var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

            context.SetState(new DistributorState<ITIsNaturalSate>(
                context.ServiceProvider.GetServices<ITIsNaturalSate>()));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Вы Юридическое лицо или Физическое лицо?",
                request.QueryId,
                inlineKeyboardMarkup
            );
        }
    }

    internal class LTNaturalPersonState : ITIsNaturalSate
    {
        public string Message { get; } = "natural";
        private readonly ITelegramBotClient _botClient;
        public LTNaturalPersonState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LTCityState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите город, в котором вы находитесь",
                request.QueryId
            );
        }
    }



    /// <summary>
    /// Обработчик, который спрашивает название компании
    /// </summary>
    internal class LTJurdicalPersonState : ITIsNaturalSate
    {
        public string Message { get; } = "juridical";
        private readonly ITelegramBotClient _botClient;
        public LTJurdicalPersonState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LTCompanyNameState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите название вашей компании.",
                request.QueryId
            );
        }
    }

    internal class LTCompanyNameState : IState
    {
        public string Message { get; } = "companyName";
        private readonly ITelegramBotClient _botClient;
        public LTCompanyNameState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LTCityState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите город, в котором вы находитесь",
                request.QueryId
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает город пользователя
    /// </summary>
    //internal class LBCityState : IState
    //{
    //    public string Message { get; } = "city";
    //    private readonly ITelegramBotClient _botClient;
    //    public LBCityState(ITelegramBotClient client)
    //    {
    //        _botClient = client;
    //    }

    //    public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    //    {
    //        //if (request.Update.CallbackQuery is not { } query) return;
    //        //if (query.Message is not { } message) return;
    //        Console.WriteLine("Start Execute command");

    //        context.SetState(new LBUserEmailState(_botClient));

    //        return await _botClient.SendMessageWithSaveBotMessageId(
    //            context,
    //            text: "Укажите вашу почту.",
    //            request.QueryId
    //        );
    //    }
    //}

    /// <summary>
    /// Обработчик, который спрашивает почту пользователя
    /// </summary>
    internal class LTCityState : IState
    {
        public string Message { get; } = "city";
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

            context.SetState(new LTEmailState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите вашу почту.",
                request.QueryId
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает номер телефона пользовтеля
    /// </summary>
    internal class LTEmailState : IState
    {
        public string Message { get; } = "email";
        private readonly ITelegramBotClient _botClient;
        public LTEmailState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LTNumberPhoneState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите ваш номер телефона.",
                request.QueryId
            );
        }
    }

    /// <summary>
    /// Обработчик, который cпрашивает, какой плагин пользователь хочет приобрести.
    /// </summary>
    internal class LTNumberPhoneState : IState
    {
        public string Message { get; } = "numberPhone";
        private readonly ITelegramBotClient _botClient;
        public LTNumberPhoneState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LTPluginState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Напишите плагины, которые вы хотите протестировать",
                request.QueryId
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает сколько лицензий хочет купить пользователь
    /// </summary>
    internal class LTPluginState : ILicenseState
    {
        public string Message { get; } = "needDemo";
        private readonly ITelegramBotClient _botClient;
        public LTPluginState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LTFinalState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите количество лиценнзий, которые вы хотите получить.",
                request.QueryId
            );
        }
    }

    internal class LTFinalState : IState
    {
        public string Message { get; } = "countLicense";
        private readonly ITelegramBotClient _botClient;
        public LTFinalState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new DistributorState<IReplyMarkupState>(
                context.ServiceProvider.GetServices<IReplyMarkupState>()));

            await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Ваша информация была передана менеджеру.",
                request.QueryId
            );

            return 0;
        }
    }
}