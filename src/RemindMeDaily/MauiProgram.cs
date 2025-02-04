using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;
using RemindMeDaily.Core.Configurations;

namespace RemindMeDaily;

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
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddSingleton(GetSettingsConfigurations());

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		//builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}

	static ApiSettings GetSettingsConfigurations()
	{
		var assembly = Assembly.GetExecutingAssembly();
		using var stream = assembly.GetManifestResourceStream("RemindMeDaily.Core.appsettings.json");
		ApiSettings settings = null;

		if (stream != null)
		{
			using var reader = new StreamReader(stream);
			
			var json = reader.ReadToEnd();
			settings = JsonSerializer.Deserialize<ApiSettings>(json);

			reader.Close();
		}

		return settings;
	}
}
