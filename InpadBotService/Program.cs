using InpadBotService.HelpButton;
using InpadBotService.LicenseButton;
using InpadBotService.QuestionButton;
using InpadBotService.SupportButton;
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
			builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(serviceProvider =>
			{
				var token = serviceProvider.GetRequiredService<IOptions<BotOptions>>().Value.Token;
				return new TelegramBotClient(token);
			});

			builder.Services.AddMultipleImplementations<IReplyMarkupState>(
				[typeof(HelpMessageState),
				typeof(StartMessageState),
				typeof(SupportMessageState),
				typeof (LicenseMessageState),
				typeof(QuestionMessageState)]);

			builder.Services.AddMultipleImplementations<IHelpTypeState>(
				[typeof(QuestionAboutPluginState),
				typeof(ReportErrorState),
				typeof(HelpInstallationState)]);

			builder.Services.AddMultipleImplementations<IHelpQuestionCategoryPlugin>(
				[typeof(HQCategoryConceptState),
				typeof(HQCategoryCommonState),
				typeof(HQCategoryConstructiveState),
				typeof(HQCategoryOBAndBKState),
				typeof(HQCategoryRengaState),
				typeof(HQCategoryArchitectureState),
				typeof(HQCategoryBoxesAndHolesState)]);

			builder.Services.AddMultipleImplementations<IHelpReportCategoryPlugin>(
				[typeof(HRCategoryConceptState),
				typeof(HRCategoryCommonState),
				typeof(HRCategoryConstructiveState),
				typeof(HRCategoryOBAndBKState),
				typeof(HRCategoryRengaState),
				typeof(HRCategoryArchitectureState),
				typeof(HRCategoryBoxesAndHolesState)]);

			builder.Services.AddMultipleImplementations<ISupportCategoryPluginState>(
                [typeof(SCategoryConceptState),
                typeof(SCategoryArchitectureState),
                typeof(SCategoryConstructiveState),
                typeof(SCategoryOBAndBKState),
                typeof(SCategoryCommonState),
                typeof(SCategoryBoxesAndHolesState),
			    typeof (SCategoryRengaState)]);

			builder.Services.AddMultipleImplementations<ISendingFileState>(
				[typeof(HQSendFileState),
				typeof(HQDontSendFileState)]);

			builder.Services.AddMultipleImplementations<ILicenseState>(
				[typeof(LBSendFIOState),
                typeof(LTSendFIOState)]);

			builder.Services.AddMultipleImplementations<IIsNaturalState>(
				[typeof(LBNaturalPersonState),
				typeof (LBJurdicalPersonState)]);

            builder.Services.AddMultipleImplementations<ITIsNaturalSate>(
                [typeof(LTNaturalPersonState),
                typeof (LTJurdicalPersonState)]);

            var host = builder.Build();
			host.Run();
		}
	}

	public static class ServiceCollectionExtensions
	{
		public static void AddMultipleImplementations<TInterface>(this IServiceCollection services, params Type[] implementations)
		{
			foreach (var implementation in implementations)
			{
				services.AddSingleton(typeof(TInterface), implementation);
			}
		}
	}
}