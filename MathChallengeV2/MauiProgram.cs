using MathChallengeV2.Data;

namespace MathChallengeV2;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("CaveatBrush-Regular.ttf", "CaveatBrushRegular");
				fonts.AddFont("EraserDust-p70d.ttf", "EraserDust");
				fonts.AddFont("KGChasingPavements.ttf", "ChasingPavements");
			});

		string dbPath = Path.Combine("D:\\Visual Studio Projects\\MathChallengeV2\\MathChallengeV2\\Data\\", "game.db");


		builder.Services.AddSingleton<GameRepository>(s => ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));

		return builder.Build();
	}
}
