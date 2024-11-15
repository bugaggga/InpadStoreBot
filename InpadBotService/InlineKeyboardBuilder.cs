using Telegram.Bot.Types.ReplyMarkups;

namespace InpadBotService;

internal class InlineKeyboardBuilder
{
	private Tuple<string, string>[] _pairs;
	private int _height;
	private int _width;
	private int _additionalCount;

	public InlineKeyboardBuilder(int height, int width, Tuple<string, string>[] pairs)
	{
		_pairs = pairs;
		_height = height;
		_width = width;
		_additionalCount = _pairs.Length - _height * _width;
	}

	public InlineKeyboardMarkup Build()
	{
		var indexer = 0;
		var buttons = new InlineKeyboardButton[_height + _additionalCount][];
		for (var i = 0; i < _height; i++)
		{
            buttons[i] = new InlineKeyboardButton[_width];
			for (var j = 0; j < _width; j++)
			{
				buttons[i][j] = InlineKeyboardButton.WithCallbackData(_pairs[indexer].Item1, _pairs[indexer].Item2);
				indexer++;
			}
		}
		for (var i = indexer; i < _height + _additionalCount; i++)
		{
			buttons[i] = new[] {
			InlineKeyboardButton.WithCallbackData(_pairs[indexer].Item1, _pairs[indexer].Item2) 
			};
			indexer++;
		}

		return new InlineKeyboardMarkup(buttons);
	}
}