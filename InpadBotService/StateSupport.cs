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

namespace InpadBotService;

public interface ISupportTypeAnswerHandler : IState;

public interface ICategoryPluginSupport : IState;

// Этап 2 Пункт 1
internal class SupportMessageHandler : IReplyMarkupHandler
{
    private readonly ITelegramBotClient _botClient;
    public string Message { get; } = "/support";

    public SupportMessageHandler(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");
        var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Renga", "renga"),
                    InlineKeyboardButton.WithCallbackData("Конструктив", "construct")

                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Архитектура", "architecture"),
                    InlineKeyboardButton.WithCallbackData("Концепция", "concept")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("ОВ и ВК", "ovAndVk"),
                    InlineKeyboardButton.WithCallbackData("Общие", "general"),
                    InlineKeyboardButton.WithCallbackData("Боксы и отверстия", "boxesAndPoints")
                }
                });
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessage(
                query.Message.Chat.Id,
        text: "Выберите категорию, в котором находится плагин.",
        replyMarkup: inlineKeyboardMarkup);

        context.SetState(new DistributorState<ICategoryPluginSupport>(
            context.ServiceProvider.GetServices<ICategoryPluginSupport>()));

    }
}

// Этап 2 Пункт 1.1
internal class PluginConceptSupport : ICategoryPluginSupport
{
    public string Message { get; } = "concept";
    private readonly ITelegramBotClient _botClient;
    public PluginConceptSupport(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Инсоляций", "Insolation"),
                    InlineKeyboardButton.WithCallbackData("КЕО", "Keo"),
                    InlineKeyboardButton.WithCallbackData("Генерация парков", "Generating parks")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Генерация деревьев", "Generating trees"),
                    InlineKeyboardButton.WithCallbackData("Разлиновка модели", "Model layout"),
                    InlineKeyboardButton.WithCallbackData("3D сетки", "3D grids")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("БыстроТЭПЫ", "Fasttep"),
                    InlineKeyboardButton.WithCallbackData("Подсчет площадей", "Area calculation")
                }
                });
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessage(
            chatId: message.Chat.Id,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 2 Пункт 1.2
internal class PluginArchitectureSupport : ICategoryPluginSupport
{
    public string Message { get; } = "architecture";
    private readonly ITelegramBotClient _botClient;
    public PluginArchitectureSupport(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Определить помещение", "Identify the room"),
                    InlineKeyboardButton.WithCallbackData("Расчет плинтуса", "Skirting board calculation"),
                    InlineKeyboardButton.WithCallbackData("Отделка", "Finishing")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Копировать отделку", "Copy the finish"),
                    InlineKeyboardButton.WithCallbackData("Проемы по дверям/окнам на связи", "Door/window openings are in touch"),
                    InlineKeyboardButton.WithCallbackData("Сооединение полов", "Connecting the floors")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Подсчет площадей", "Area calculation"),
                    InlineKeyboardButton.WithCallbackData("Планировка", "Layout"),
                    InlineKeyboardButton.WithCallbackData("Округление площади", "Rounding up the area"),
                    InlineKeyboardButton.WithCallbackData("Нумерация квартир", "Apartment numbering")
                }
                });
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessage(
            chatId: message.Chat.Id,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 2 Пункт 1.3
internal class PluginConstructiveSupport : ICategoryPluginSupport
{
    public string Message { get; } = "construct";
    private readonly ITelegramBotClient _botClient;
    public PluginConstructiveSupport(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Сборка арматуры", "Fitting assembly"),
                    InlineKeyboardButton.WithCallbackData("Создать разрезы и сечения", "Create sections and cross sections"),
                    InlineKeyboardButton.WithCallbackData("Создание планов", "Creating plans"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Создание контура", "Creating a contour"),
                    InlineKeyboardButton.WithCallbackData("Редактирование контура", "Editing a contour"),
                    InlineKeyboardButton.WithCallbackData("Расчет продавливания", "Calculation of the penetration")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Создание каркасов", "Creating wireframes"),
                   InlineKeyboardButton.WithCallbackData("Создание видов каркасов", "Creating types of wireframes")
                }
                });
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessage(
            chatId: message.Chat.Id,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 2 Пункт 1.4
internal class PluginOBAndBKSupport : ICategoryPluginSupport
{
    public string Message { get; } = "ovAndVk";
    private readonly ITelegramBotClient _botClient;
    public PluginOBAndBKSupport(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Муфты/гильзы", "Couplings/sleeves"),
                    InlineKeyboardButton.WithCallbackData("Аэродинамика", "Aerodynamics"),
                    InlineKeyboardButton.WithCallbackData("Создать виды систем", "Create types of systems")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Специфакция систем", "System Specification"),
                    InlineKeyboardButton.WithCallbackData("Высотные отметки", "Elevations"),
                    InlineKeyboardButton.WithCallbackData("Толщина стенки", "Wall thickness")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Диаметр изоляции", "Insulation diameter"),
                    InlineKeyboardButton.WithCallbackData("S изоляции", "S insulation")
                }
                });
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessage(
            chatId: message.Chat.Id,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 2 Пункт 1.5
internal class PluginCommonSupport : ICategoryPluginSupport
{
    public string Message { get; } = "general";
    private readonly ITelegramBotClient _botClient;
    public PluginCommonSupport(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Этажи и секции", "Floors and sections"),
                    InlineKeyboardButton.WithCallbackData("Подсчет узлов", "Counting nodes"),
                    InlineKeyboardButton.WithCallbackData("Печать листов", "Printing sheets")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Множественная печать", "Multiple printing"),
                    InlineKeyboardButton.WithCallbackData("Копировать спецификацию", "Copy the specification"),
                    InlineKeyboardButton.WithCallbackData("Копировать параметры", "Copy Parameters"),
                    InlineKeyboardButton.WithCallbackData("Параметры", "Parameters"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Параметры семейств", "Family Parameters"),
                    InlineKeyboardButton.WithCallbackData("Копировать параметры арматуры", "Copy the valve parameters"),
                    InlineKeyboardButton.WithCallbackData("Комбинирование дверей", "Door combination")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Огнекороб", "Ognekorob"),
                    InlineKeyboardButton.WithCallbackData("Просмотр пересечения", "Viewing the intersection"),
                    InlineKeyboardButton.WithCallbackData("Менеджер узлов", "Node Manager"),
                    InlineKeyboardButton.WithCallbackData("Проверка модели", "Checking the model")
                }
                });
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessage(
            chatId: message.Chat.Id,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 2 Пункт 1.6
internal class PluginBoxesAndHolesSupport : ICategoryPluginSupport
{
    public string Message { get; } = "boxesAndPoints";
    private readonly ITelegramBotClient _botClient;
    public PluginBoxesAndHolesSupport(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        if (request.Update.CallbackQuery is not { } query) return;
        if (query.Message is not { } message) return;
        Console.WriteLine("Start Execute command");

        var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
        {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Создание заданий", "Creating tasks"),
                    InlineKeyboardButton.WithCallbackData("Объединение", "Unification"),
                    InlineKeyboardButton.WithCallbackData("Смещение", "Offset")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Обрезатьм", "Crop"),
                    InlineKeyboardButton.WithCallbackData("Нумерацияи", "Numbering"),
                    InlineKeyboardButton.WithCallbackData("Отметка", "Mark")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Отвествия", "Holes"),
                    InlineKeyboardButton.WithCallbackData("Проверка пересечений", "Checking intersections"),
                    InlineKeyboardButton.WithCallbackData("Проверка пересекающихся заданий", "Checking for overlapping tasks")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Статусы заданий", "Task statuses"),
                    InlineKeyboardButton.WithCallbackData("Обозреватель статусов", "Status Browser"),
                    InlineKeyboardButton.WithCallbackData("Проверка заданий", "Checking tasks")
                }
                });
        await _botClient.AnswerCallbackQuery(
            query.Id);

        await _botClient.SendMessage(
            chatId: message.Chat.Id,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}
