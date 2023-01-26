using MathChallengeV2.Models;
using Microsoft.Graphics.Canvas.Effects;

namespace MathChallengeV2;

public partial class MainPage : ContentPage
{
	public GameDifficulty difficulty = GameDifficulty.Easy;
	public int numberOfQuestions = 5;

	public MainPage()
	{
		InitializeComponent();
		Difficulty.Text = $"Difficulty : {Convert.ToString(difficulty)}";
		NumberOfQuestions.Text = $"Number Of Questions : {numberOfQuestions}";

		BindingContext = this;
	}

	private void OnGameChosen(object sender, EventArgs e)
	{
		Button button = (Button)sender;

		Navigation.PushAsync(new GamePage(button.Text, difficulty, numberOfQuestions));
	}

	private void OnViewPreviousGamesChosen(object sender, EventArgs e)
	{
		Navigation.PushAsync(new PreviousGames());
	}

	private void ToggleDifficulty(object sender, EventArgs e)
	{
		difficulty = difficulty == GameDifficulty.Easy ? GameDifficulty.Challenging : difficulty == GameDifficulty.Challenging ? GameDifficulty.Hard : difficulty = GameDifficulty.Easy;

        Difficulty.Text = $"Difficulty : {Convert.ToString(difficulty)}";
    }

    private void ToggleNumberOfQuestions(object sender, EventArgs e)
    {
		numberOfQuestions = numberOfQuestions == 5 ? 10 : numberOfQuestions == 10 ? 15 : numberOfQuestions == 15 ? 20 : numberOfQuestions = 5;

		NumberOfQuestions.Text = $"Number Of Questions : {numberOfQuestions}";
    }
}

