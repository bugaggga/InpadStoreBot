using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService;

internal static class InlineKeyboardBuilder
{
	public static InlineKeyboardMarkup Build((string, string)[] pairs)
	{
		(var height, var width, var additionalCount) = CalculateAllParams(pairs);



        var indexer = 0;
		var buttons = new InlineKeyboardButton[height + additionalCount][];
		for (var i = 0; i < height; i++)
		{
            buttons[i] = new InlineKeyboardButton[width];
			for (var j = 0; j < width; j++)
			{
				buttons[i][j] = InlineKeyboardButton.WithCallbackData(pairs[indexer].Item1, pairs[indexer].Item2);
				indexer++;
			}
		}
		for (var i = height; i < height + additionalCount; i++)
		{
			buttons[i] = new[] {
			InlineKeyboardButton.WithCallbackData(pairs[indexer].Item1, pairs[indexer].Item2) 
			};
			indexer++;
		}

		return new InlineKeyboardMarkup(buttons);
	}

	private static (int, int, int) CalculateAllParams((string, string)[] pairs)
	{

		var height = pairs.Length >= 4 ? pairs.Length / 2 : pairs.Length;
		var width = pairs.Length >= 4 ? 2 : 1;
		var additionalCount = pairs.Length % width;

		return (height, width, additionalCount);
	}
}