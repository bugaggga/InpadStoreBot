using Telegram.Bot;

namespace InpadBotService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = Host.CreateApplicationBuilder(args);
			builder.Services.AddHostedService<TgBotBackgroundService>();
			builder.Services.AddTransient<ITelegramBotClient, TelegramBotClient>(serviceProvider =>
			{
				var token = "7737475347:AAE0WP2ueyWN9kHrljk_O_pEzrY3q3MTPVE";
				return new TelegramBotClient(token);
			});

			builder.Services.AddTransient<IHandler, WelcomeMessageHandler>();

			var host = builder.Build();
			host.Run();
		}
	}
}