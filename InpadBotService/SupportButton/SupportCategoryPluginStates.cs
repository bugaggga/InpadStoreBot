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

namespace InpadBotService.SupportButton;

public interface ISupportCategoryPluginState : IState;


// Этап 2 Пункт 1
internal class SCategoryConceptState : ISupportCategoryPluginState
{
    public string Message { get; } = "concept";
    private readonly ITelegramBotClient _botClient;
    public SCategoryConceptState(ITelegramBotClient client)
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
            ("Инсоляций", "Insolation"),
            ("КЕО", "Keo"),
            ("Генерация парков", "Generating parks"),
            ("Генерация деревьев", "Generating trees"),
            ("Разлиновка модели", "Model layout"),
            ("3D сетки", "3D grids"),
            ("БыстроТЭПЫ", "Fasttep"),
            ("Подсчет площадей", "Area calculation")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 2 Пункт 2
internal class SCategoryArchitectureState : ISupportCategoryPluginState
{
    public string Message { get; } = "architecture";
    private readonly ITelegramBotClient _botClient;
    public SCategoryArchitectureState(ITelegramBotClient client)
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
            ("Определить помещение", "Identify the room"),
            ("Расчет плинтуса", "Skirting board calculation"),
            ("Отделка", "Finishing"),
            ("Копировать отделку", "Copy the finish"),
            ("Проемы по дверям/окнам на связи", "Door/window openings are in touch"),
            ("Сооединение полов", "Connecting the floors"),
            ("Подсчет площадей", "Area calculation"),
            ("Планировка", "Layout"),
            ("Округление площади", "Rounding up the area"),
            ("Нумерация квартир", "Apartment numbering")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 2 Пункт 3
internal class SCategoryConstructiveState : ISupportCategoryPluginState
{
    public string Message { get; } = "construct";
    private readonly ITelegramBotClient _botClient;
    public SCategoryConstructiveState(ITelegramBotClient client)
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
            ("Сборка арматуры", "Fitting assembly"),
            ("Создать разрезы и сечения", "Create sections and cross sections"),
            ("Создание планов", "Creating plans"),
            ("Создание контура", "Creating a contour"),
            ("Редактирование контура", "Editing a contour"),
            ("Расчет продавливания", "Calculation of the penetration"),
            ("Создание каркасов", "Creating wireframes"),
            ("Создание видов каркасов", "Creating types of wireframes")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 2 Пункт 4
internal class SCategoryOBAndBKState : ISupportCategoryPluginState
{
    public string Message { get; } = "ovAndVk";
    private readonly ITelegramBotClient _botClient;
    public SCategoryOBAndBKState(ITelegramBotClient client)
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
            ("Муфты/гильзы", "Couplings/sleeves"),
            ("Аэродинамика", "Aerodynamics"),
            ("Создать виды систем", "Create types of systems"),
            ("Специфакция систем", "System Specification"),
            ("Высотные отметки", "Elevations"),
            ("Толщина стенки", "Wall thickness"),
            ("Диаметр изоляции", "Insulation diameter"),
            ("S изоляции", "S insulation")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 2 Пункт 5
internal class SCategoryCommonState : ISupportCategoryPluginState
{
    public string Message { get; } = "general";
    private readonly ITelegramBotClient _botClient;
    public SCategoryCommonState(ITelegramBotClient client)
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
            ("Этажи и секции", "Floors and sections"),
            ("Подсчет узлов", "Counting nodes"),
            ("Печать листов", "Printing sheets"),
            ("Множественная печать", "Multiple printing"),
            ("Копировать спецификацию", "Copy the specification"),
            ("Копировать параметры", "Copy Parameters"),
            ("Параметры", "Parameters"),
            ("Параметры семейств", "Family Parameters"),
            ("Копировать параметры арматуры", "Copy the valve parameters"),
            ("Комбинирование дверей", "Door combination"),
            ("Огнекороб", "Ognekorob"),
            ("Просмотр пересечения", "Viewing the intersection"),
            ("Менеджер узлов", "Node Manager"),
            ("Проверка модели", "Checking the model")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 2 Пункт 6
internal class SCategoryBoxesAndHolesState : ISupportCategoryPluginState
{
    public string Message { get; } = "boxesAndPoints";
    private readonly ITelegramBotClient _botClient;
    public SCategoryBoxesAndHolesState(ITelegramBotClient client)
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
            ("Создание заданий", "Creating tasks"),
            ("Объединение", "Unification"),
            ("Смещение", "Offset"),
            ("Обрезатьм", "Crop"),
            ("Нумерацияи", "Numbering"),
            ("Отметка", "Mark"),
            ("Отвествия", "Holes"),
            ("Проверка пересечений", "Checking intersections"),
            ("Проверка пересекающихся заданий", "Checking for overlapping tasks"),
            ("Статусы заданий", "Task statuses"),
            ("Обозреватель статусов", "Status Browser"),
            ("Проверка заданий", "Checking tasks")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 1 Пункт 1.1
internal class PluginConceptReport : ISupportCategoryPluginState
{
    public string Message { get; } = "concept";
    private readonly ITelegramBotClient _botClient;
    public PluginConceptReport(ITelegramBotClient client)
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
            ("Инсоляций", "Insolation"),
            ("КЕО", "Keo"),
            ("Генерация парков", "Generating parks"),
            ("Генерация деревьев", "Generating trees"),
            ("Разлиновка модели", "Model layout"),
            ("3D сетки", "3D grids"),
            ("БыстроТЭПЫ", "Fasttep"),
            ("Подсчет площадей", "Area calculation")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 1 Пункт 1.2
internal class PluginArchitectureReport : ISupportCategoryPluginState
{
    public string Message { get; } = "architecture";
    private readonly ITelegramBotClient _botClient;
    public PluginArchitectureReport(ITelegramBotClient client)
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
            ("Определить помещение", "Identify the room"),
            ("Расчет плинтуса", "Skirting board calculation"),
            ("Отделка", "Finishing"),
            ("Копировать отделку", "Copy the finish"),
            ("Проемы по дверям/окнам на связи", "Door/window openings are in touch"),
            ("Сооединение полов", "Connecting the floors"),
            ("Подсчет площадей", "Area calculation"),
            ("Планировка", "Layout"),
            ("Округление площади", "Rounding up the area"),
            ("Нумерация квартир", "Apartment numbering")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 1 Пункт 1.3
internal class PluginConstructiveReport : ISupportCategoryPluginState
{
    public string Message { get; } = "construct";
    private readonly ITelegramBotClient _botClient;
    public PluginConstructiveReport(ITelegramBotClient client)
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
            ("Сборка арматуры", "Fitting assembly"),
            ("Создать разрезы и сечения", "Create sections and cross sections"),
            ("Создание каркасов", "Creating wireframes"),
            ("Создание планов", "Creating plans"),
            ("Создание контура", "Creating a contour"),
            ("Создание видов каркасов", "Creating types of wireframes"),
            ("Редактировать контура", "Edit the outline"),
            ("Расчет продавливания", "Calculation of the penetration")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 1 Пункт 1.4
internal class PluginOBAndBKReport : ISupportCategoryPluginState
{
    public string Message { get; } = "ovAndVk";
    private readonly ITelegramBotClient _botClient;
    public PluginOBAndBKReport(ITelegramBotClient client)
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
            ("Муфты/гильзы", "Couplings/sleeves"),
            ("Аэродинамика", "Aerodynamics"),
            ("Создать виды систем", "Create types of systems"),
            ("Специфакция систем", "System Specification"),
            ("Высотные отметки", "Elevations"),
            ("Толщина стенки", "Wall thickness"),
            ("Диаметр изоляции", "Insulation diameter"),
            ("S изоляции", "S insulation")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 1 Пункт 1.5
internal class PluginCommonReport : ISupportCategoryPluginState
{
    public string Message { get; } = "general";
    private readonly ITelegramBotClient _botClient;
    public PluginCommonReport(ITelegramBotClient client)
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
            ("Этажи и секции", "Floors and sections"),
            ("Подсчет узлов", "Counting nodes"),
            ("Печать листов", "Printing sheets"),
            ("Множественная печать", "Multiple printing"),
            ("Копировать спецификацию", "Copy the specification"),
            ("Копировать параметры", "Copy Parameters"),
            ("Параметры семейств", "Family Parameters"),
            ("Копировать параметры арматуры", "Copy the valve parameters"),
            ("Комбинирование дверей", "Door combination"),
            ("Огнекороб", "Ognekorob"),
            ("Просмотр пересечения", "Viewing the intersection"),
            ("Менеджер узлов", "Node Manager"),
            ("Проверка модели", "Checking the model")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}

// Этап 1 Пункт 1.6
internal class PluginBoxesAndHolesReport : ISupportCategoryPluginState
{
    public string Message { get; } = "boxesAndPoints";
    private readonly ITelegramBotClient _botClient;
    public PluginBoxesAndHolesReport(ITelegramBotClient client)
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
            ("Создание заданий", "Creating tasks"),
            ("Объединение", "Unification"),
            ("Смещение", "Offset"),
            ("Обрезатьм", "Crop"),
            ("Нумерацияи", "Numbering"),
            ("Отметка", "Mark"),
            ("Отвествия", "Holes"),
            ("Проверка пересечений", "Checking intersections"),
            ("Проверка пересекающихся заданий", "Checking for overlapping tasks"),
            ("Статусы заданий", "Task statuses"),
            ("Обозреватель статусов", "Status Browser"),
            ("Проверка заданий", "Checking tasks")
            };
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

        await _botClient.AnswerCallbackQuery(
            query.Id);

        return await _botClient.SendMessageWithSaveBotMessageId(
            context,
            text: "Выберите на какой плагин вам нужна информация.",
            replyMarkup: inlineKeyboardMarkup,
			newType: UpdateType.CallbackQuery
		);
    }
}
