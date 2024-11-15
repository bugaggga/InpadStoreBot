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

public interface IState
{
	public string Message { get; }
	public Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context);
}

//public interface IHelpState: IState;

//public interface ISupportState: IState;

//public interface IQuestionState : IState;

public interface IReplyMarkupHandler : IState;

//public interface IPlugin : IState;

public record TelegramRequest(Update Update);

//public interface IHelpTypeAnswerHandler : IState;

public class StartMessageHandler : IState
{
	public string Message { get; } = "/start";
	private readonly ITelegramBotClient _botClient;
	public StartMessageHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		var replyKeyboard = new ReplyKeyboardMarkup(new[]
		{
				new KeyboardButton[] { "/help", "/support" },
				new KeyboardButton[] { "/question" }
			})
		{
			ResizeKeyboard = true
		};

		await _botClient.SendMessage(
			chatId: request.Update.Message.Chat.Id,
			text: "Нажмите на кнопку, которая Вам требуется.",
			replyMarkup: replyKeyboard
		);

		context.SetState(new DistributorState<IReplyMarkupHandler>(
			context.ServiceProvider.GetServices<IReplyMarkupHandler>()));
	}
}

// Этап 1
/*
internal class HelpMessageHandler : IReplyMarkupHandler
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "/help";
	public HelpMessageHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
		{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Хочу\r\nзадать вопрос касаемо работы плагина", "helpByWorkOrError"),
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Хочу\r\nсообщить об ошибке", "helpByWorkOrError")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Нужна\r\nпомощь при установке/активации", "helpByDownload")
				}
				});

		await _botClient.SendTextMessageAsync(
				request.Update.Message.Chat.Id,
		text: "Выберите\r\nпункт, по которому вам нужна помощь:",
		replyMarkup: inlineKeyboardMarkup);

		context.SetState(new DistributorState<IHelpTypeAnswerHandler>(
			context.ServiceProvider.GetServices<IHelpTypeAnswerHandler>()));
	}
}
*/

/*
// Этап 2
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
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
		{
				new[]
				{
					InlineKeyboardButton.WithCallbackData("supportButton 1", "btn1"),
					InlineKeyboardButton.WithCallbackData("supportButton 2", "btn2")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("SupportButton 3", "btn3"),
					InlineKeyboardButton.WithCallbackData("SupportButton 4", "btn4")
				}
				});

		await _botClient.SendTextMessageAsync(
				request.Update.Message.Chat.Id,
		text: "Выберите кнопку:",
		replyMarkup: inlineKeyboardMarkup);

		context.SetState(new DistributorState<IHelpTypeAnswerHandler>(
			context.ServiceProvider.GetServices<IHelpTypeAnswerHandler>()));
	}
}
*/
/*
// Этап 3
internal class QuestionMessageHandler : IReplyMarkupHandler
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "/question";

	public QuestionMessageHandler(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		Console.WriteLine("Start Execute command");
		if (request.Update.Message is null) return;

		var replyKeyboard = new ReplyKeyboardMarkup(new[]
		{
				new KeyboardButton[] { "/help", "/support" },
				new KeyboardButton[] { "/question" }
			})
		{
			ResizeKeyboard = true
		};

		await _botClient.SendTextMessageAsync(
			chatId: request.Update.Message.Chat.Id,
			text: "Выберите услугу",
			replyMarkup: replyKeyboard
		);

		context.SetState(new DistributorState<IReplyMarkupHandler>(
			context.ServiceProvider.GetServices<IReplyMarkupHandler>()));
	}
}
*/

/*
// Этап 1 Пункт 1
internal class HelpTypeHandler : IHelpTypeAnswerHandler
{
	private readonly ITelegramBotClient _botClient;
	public string Message { get; } = "helpByWorkOrError";

	public HelpTypeHandler(ITelegramBotClient client)
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
		await _botClient.AnswerCallbackQueryAsync(
			query.Id);

		await _botClient.SendTextMessageAsync(
				query.Message.Chat.Id,
		text: "Выберите\r\nиз какой категории плагин, с которым вам нужна помощь",
		replyMarkup: inlineKeyboardMarkup);

	}
}

// Этап 1 Пункт 1.1
internal class PluginConcept : IPlugin
{
    public string Message { get; } = "concept";
    private readonly ITelegramBotClient _botClient;
    public PluginConcept(ITelegramBotClient client)
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
        await _botClient.AnswerCallbackQueryAsync(
            query.Id);

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 1 Пункт 1.2
internal class PluginArchitecture : IPlugin
{
    public string Message { get; } = "architecture";
    private readonly ITelegramBotClient _botClient;
    public PluginArchitecture(ITelegramBotClient client)
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
        await _botClient.AnswerCallbackQueryAsync(
            query.Id);

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 1 Пункт 1.3
internal class PluginConstructive : IPlugin
{
    public string Message { get; } = "construct";
    private readonly ITelegramBotClient _botClient;
    public PluginConstructive(ITelegramBotClient client)
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
                    InlineKeyboardButton.WithCallbackData("Создание каркасов", "Creating wireframes")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Создание планов", "Creating plans"),
                    InlineKeyboardButton.WithCallbackData("Создание контура", "Creating a contour"),
                    InlineKeyboardButton.WithCallbackData("Создание видов каркасов", "Creating types of wireframes")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Редактировать контура", "Edit the outline"),
                    InlineKeyboardButton.WithCallbackData("Расчет продавливания", "Calculation of the penetration")
                }
                });
        await _botClient.AnswerCallbackQueryAsync(
            query.Id);

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 1 Пункт 1.4
internal class PluginOBAndBK : IPlugin
{
    public string Message { get; } = "ovAndVk";
    private readonly ITelegramBotClient _botClient;
    public PluginOBAndBK(ITelegramBotClient client)
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
        await _botClient.AnswerCallbackQueryAsync(
            query.Id);

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 1 Пункт 1.5
internal class PluginCommon : IPlugin
{
    public string Message { get; } = "general";
    private readonly ITelegramBotClient _botClient;
    public PluginCommon(ITelegramBotClient client)
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
                    InlineKeyboardButton.WithCallbackData("Копировать параметры", "Copy Parameters")
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
        await _botClient.AnswerCallbackQueryAsync(
            query.Id);

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 1 Пункт 1.6
internal class PluginBoxesAndHoles : IPlugin
{
    public string Message { get; } = "boxesAndPoints";
    private readonly ITelegramBotClient _botClient;
    public PluginBoxesAndHoles(ITelegramBotClient client)
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
        await _botClient.AnswerCallbackQueryAsync(
            query.Id);

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Выберите каким плагином вы воспользовались.",
            replyMarkup: inlineKeyboardMarkup
        );
    }
}

// Этап 1 Пункт 2
internal class HelpDownloadHandler : IHelpTypeAnswerHandler
{
	public string Message { get; } = "helpByDownload";
	private readonly ITelegramBotClient _botClient;
	public HelpDownloadHandler(ITelegramBotClient client)
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
					InlineKeyboardButton.WithCallbackData("Ошибка при установке сборки", "Error")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("Не получается зарегистрироваться", "registr")
				},
				new[]
				{
					InlineKeyboardButton.WithCallbackData("не получается ввести ключ продукта", "keyOfProduct")
				}
				});
		await _botClient.AnswerCallbackQueryAsync(
			query.Id);

		await _botClient.SendTextMessageAsync(
			chatId: message.Chat.Id,
			text: "Выводится сообщение: \"Выберите категорию по которой вам нужна поморщь\" ",
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

// Этап 1 Пункт 4
internal class PluginRenga : IPlugin
{
    public string Message { get; } = "renga";
    private readonly ITelegramBotClient _botClient;
    public PluginRenga(ITelegramBotClient client)
    {
        _botClient = client;
    }

    public async Task HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
    {
        await Task.CompletedTask;
    }
}
*/
