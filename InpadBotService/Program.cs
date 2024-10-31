using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace InpadBotService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = Host.CreateApplicationBuilder(args);
			builder.Services.AddHostedService<TgBotBackgroundService>();
			builder.Services.Configure<BotOptions>(builder.Configuration.GetSection("BotOptions"));
			builder.Services.AddSingleton<UserContextManager>();
			//builder.Services.AddSingleton<HandlerDistributor>();
			//builder.Services.AddSingleton<UserContext>(serviceProvider =>
			//{
			//	var manager = serviceProvider.GetRequiredService<UserContextManager>();
				
			//});
			builder.Services.AddTransient<ITelegramBotClient, TelegramBotClient>(serviceProvider =>
			{
				var token = serviceProvider.GetRequiredService<IOptions<BotOptions>>().Value.Token;
				return new TelegramBotClient(token);
			});
			builder.Services.AddTransient<IReplyMarkupHandler, HelpMessageHandler>();
			builder.Services.AddTransient<IReplyMarkupHandler, SupportMessageHandler>();
			builder.Services.AddTransient<IReplyMarkupHandler, QuestionMessageHandler>();
			builder.Services.AddTransient<IReplyMarkupHandler, StartMessageHandler>();

			var host = builder.Build();
			host.Run();
		}
	}
}