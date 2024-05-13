using Microsoft.Extensions.Logging;

namespace YouTuber
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif
			// TODO add IOption patterns for each module, see 
			/*

			services.AddOptions<DownloaderAppSettings>()
				.BindConfiguration(DownloaderAppSettings.DownloaderAppSettingsSection)
				.ValidateFluentValidation()
				.ValidateOnStart();
			*/

			return builder.Build();
		}
	}
}
