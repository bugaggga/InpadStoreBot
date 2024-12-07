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
    public interface IIsNaturalState : IState;

    /// <summary>
    /// Обработчик который спрашивает ФИО пользователя
    /// </summary>
    internal class LBSendFIOState : ILicenseState
    {
        public string Message { get; } = "buyPlugin";
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
            context.SetState(new LBNaturalOrJuridicalPersonState(_botClient));

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
    internal class LBNaturalOrJuridicalPersonState : IState
    {
        public string Message { get; } = "NaturalOrJuridicalPerson";
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

            var pairs = new[] {
            ("Физическое лицо", "natural"),
            ("Юридическое лицо", "juridical")
            };
            var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

            context.SetState(new DistributorState<IIsNaturalState>(
                context.ServiceProvider.GetServices<IIsNaturalState>()));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Вы Юридическое лицо или Физическое лицо?",
                request.QueryId,
                inlineKeyboardMarkup
            );
        }
    }

    internal class LBNaturalPersonState : IIsNaturalState
    {
        public string Message { get; } = "natural";
        private readonly ITelegramBotClient _botClient;
        public LBNaturalPersonState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LBCityState(_botClient));

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
    internal class LBJurdicalPersonState : IIsNaturalState
    {
        public string Message { get; } = "juridical";
        private readonly ITelegramBotClient _botClient;
        public LBJurdicalPersonState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LBCompanyNameState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите название вашей компании.",
                request.QueryId
            );
        }
    }

    internal class LBCompanyNameState : IState
    {
        public string Message { get; } = "companyName";
        private readonly ITelegramBotClient _botClient;
        public LBCompanyNameState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LBCityState(_botClient));

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
    internal class LBCityState : IState
    {
        public string Message { get; } = "city";
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

            context.SetState(new LBEmailState(_botClient));

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
    internal class LBEmailState : IState
    {
        public string Message { get; } = "email";
        private readonly ITelegramBotClient _botClient;
        public LBEmailState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LBNumberPhoneState(_botClient));

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
    internal class LBNumberPhoneState : IState
    {
        public string Message { get; } = "numberPhone";
        private readonly ITelegramBotClient _botClient;
        public LBNumberPhoneState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            context.SetState(new LBPluginState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Напишите плагины, которые вы хотите приобрести",
                request.QueryId
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает, нужна ли демонстрация плагина
    /// </summary>
    internal class LBPluginState : IState
    {
        public string Message { get; } = "plugin";
        private readonly ITelegramBotClient _botClient;
        public LBPluginState(ITelegramBotClient client)
        {
            _botClient = client;
        }

        public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
        {
            //if (request.Update.CallbackQuery is not { } query) return;
            //if (query.Message is not { } message) return;
            Console.WriteLine("Start Execute command");

            var pairs = new[] {
            ("Да", "Yes"),
            ("Нет", "No")
            };
            var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

            context.SetState(new LBDemoState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Нужна ли вам демонстрация плагина?",
                request.QueryId,
                replyMarkup: inlineKeyboardMarkup
            );
        }
    }

    /// <summary>
    /// Обработчик, который спрашивает сколько лицензий хочет купить пользователь
    /// </summary>
    internal class LBDemoState : ILicenseState
    {
        public string Message { get; } = "needDemo";
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

            context.SetState(new LBFinalState(_botClient));

            return await _botClient.SendMessageWithSaveBotMessageId(
                context,
                text: "Укажите количество лиценнзий, которые вы хотите приобрести.",
                request.QueryId
            );
        }
    }

    internal class LBFinalState :IState
    {
        public string Message { get; } = "countLicense";
        private readonly ITelegramBotClient _botClient;
        public LBFinalState(ITelegramBotClient client)
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
