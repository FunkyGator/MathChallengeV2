using MathChallengeV2.Models;
using Microsoft.Graphics.Canvas.Effects;

namespace MathChallengeV2;

public partial class MainPage : ContentPage
{
	public GameDifficulty difficulty = GameDifficulty.Easy;

	public MainPage()
	{
		InitializeComponent();
		Difficulty.Text = $"Difficulty : {Convert.ToString(difficulty)}";
		BindingContext = this;
	}

	private void OnGameChosen(object sender, EventArgs e)
	{
		Button button = (Button)sender;

		Navigation.PushAsync(new GamePage(button.Text,difficulty));
	}

	private void OnViewPreviousGamesChosen(object sender, EventArgs e)
	{
		Navigation.PushAsync(new PreviousGames());
	}

	private void ToggleDifficulty(object sender, EventArgs e)
	{
		if (difficulty == GameDifficulty.Easy) difficulty = GameDifficulty.Challenging;
		else if (difficulty == GameDifficulty.Challenging) difficulty = GameDifficulty.Hard;
        else difficulty = GameDifficulty.Easy;

        Difficulty.Text = $"Difficulty : {Convert.ToString(difficulty)}";
    }
}

