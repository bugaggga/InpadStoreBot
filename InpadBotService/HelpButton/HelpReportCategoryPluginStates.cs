using InpadBotService.DatasFuncs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace InpadBotService.HelpButton;

public interface IHelpReportCategoryPlugin : IState;

// Этап 1 Пункт 2.1
internal class HRCategoryConceptState : IHelpReportCategoryPlugin
{
	public string Message { get; } = "concept";
	private readonly ITelegramBotClient _botClient;
	public HRCategoryConceptState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

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
		
		this.SetState(context, _botClient);

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите каким плагином вы воспользовались.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

// Этап 1 Пункт 2.2
internal class HRCategoryArchitectureState : IHelpReportCategoryPlugin
{
	public string Message { get; } = "architecture";
	private readonly ITelegramBotClient _botClient;
	public HRCategoryArchitectureState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

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

		this.SetState(context, _botClient);

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите каким плагином вы воспользовались.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

// Этап 1 Пункт 2.3
internal class HRCategoryConstructiveState : IHelpReportCategoryPlugin
{
	public string Message { get; } = "construct";
	private readonly ITelegramBotClient _botClient;
	public HRCategoryConstructiveState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

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

		this.SetState(context, _botClient);

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите каким плагином вы воспользовались.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

// Этап 1 Пункт 2.4
internal class HRCategoryOBAndBKState : IHelpReportCategoryPlugin
{
	public string Message { get; } = "ovAndVk";
	private readonly ITelegramBotClient _botClient;
	public HRCategoryOBAndBKState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

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
		
		this.SetState(context, _botClient);

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите каким плагином вы воспользовались.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

// Этап 1 Пункт 2.5
internal class HRCategoryCommonState : IHelpReportCategoryPlugin
{
	public string Message { get; } = "general";
	private readonly ITelegramBotClient _botClient;
	public HRCategoryCommonState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

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

		this.SetState(context, _botClient);

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите каким плагином вы воспользовались.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

// Этап 1 Пункт 2.6
internal class HRCategoryBoxesAndHolesState : IHelpReportCategoryPlugin
{
	public string Message { get; } = "boxesAndPoints";
	private readonly ITelegramBotClient _botClient;
	public HRCategoryBoxesAndHolesState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

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

		this.SetState(context, _botClient);

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите каким плагином вы воспользовались.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

// Этап 1 Пункт 2.7
internal class HRCategoryRengaState : IHelpReportCategoryPlugin
{
	public string Message { get; } = "renga";
	private readonly ITelegramBotClient _botClient;
	public HRCategoryRengaState(ITelegramBotClient client)
	{
		_botClient = client;
	}

	public async Task<int> HandleAsync(TelegramRequest request, CancellationToken cancellationToken, UserContext context)
	{
		//if (request.Update.CallbackQuery is not { } query) return;
		//if (query.Message is not { } message) return;
		Console.WriteLine("Start Execute command");

		//DataBuilder.UpdateData(context, Message);

        var pairs = new[] {
			("Подсчет площадей", "Area calculation"),
			("Активация", "Activation")
			};
        var inlineKeyboardMarkup = InlineKeyboardBuilder.Build(pairs);

		context.SetState(new HelpReportRengaPluginState(_botClient));

		return await _botClient.SendMessageWithSaveBotMessageId(
			context,
			text: "Выберите каким плагином вы воспользовались.",
			request.QueryId,
			replyMarkup: inlineKeyboardMarkup
		);
	}
}

public static class HRCategoryPluginExtensions
{
	public static void SetState(this IHelpReportCategoryPlugin hqCategoryPluginState, UserContext context, ITelegramBotClient client)
	{
		context.SetState(new HelpRepotPluginState(client));
	}
}
