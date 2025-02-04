using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json;
using RemindMeDaily.Core.Services;
using RemindMeDaily.Domain.Interfaces.Services;
using RemindMeDaily.Core.Configurations;
using RemindMeDaily.ViewModels;
using RemindMeDaily.Pages;


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

		builder.Services.AddSingleton<HttpClient>();
        //builder.Services.AddHttpClient<RemindServiceCore>().SetHandlerLifetime(TimeSpan.FromMinutes(5));

		 // Adiciona o arquivo de configuração JSON
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Configura as configurações no container de DI
        var apiSettings = config.GetSection("ApiSettings").Get<ApiSettings>() ?? new ApiSettings();
        builder.Services.AddSingleton(apiSettings);

        // Adiciona serviços necessários
        builder.Services.AddSingleton<IReminderCoreService, RemindServiceCore>();

        builder.Services.AddTransient<RemindersViewModel>();

		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

        // builder.Services.AddSingleton<MainPage>();

		// builder.Services.AddSingleton<App>();

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
