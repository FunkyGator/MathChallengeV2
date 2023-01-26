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
		if (difficulty == GameDifficulty.Easy) difficulty = GameDifficulty.Challenging;
		else if (difficulty == GameDifficulty.Challenging) difficulty = GameDifficulty.Hard;
        else difficulty = GameDifficulty.Easy;

        Difficulty.Text = $"Difficulty : {Convert.ToString(difficulty)}";
    }

    private void ToggleNumberOfQuestions(object sender, EventArgs e)
    {
		if (numberOfQuestions == 5) numberOfQuestions = 10;
		else if (numberOfQuestions == 10) numberOfQuestions = 15;
		else if (numberOfQuestions == 15) numberOfQuestions = 20;
		else numberOfQuestions= 5;

		NumberOfQuestions.Text = $"Number Of Questions : {numberOfQuestions}";
    }
}

