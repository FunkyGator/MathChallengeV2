using MathChallengeV2.Models;

namespace MathChallengeV2;

// Codebehind for the MainPage.
public partial class MainPage : ContentPage
{
	public GameDifficulty difficulty = GameDifficulty.Easy;
	public int numberOfQuestions = 5;

	public MainPage()
	{
		InitializeComponent();

		Difficulty.Text = $"Difficulty : {difficulty}";
		NumberOfQuestions.Text = $"Number Of Questions : {numberOfQuestions}";

		BindingContext = this;
	}

	// When the Game Type is chosen takes the Player to the GamePage.
	private void OnGameChosen(object sender, EventArgs e)
	{
		Button button = (Button)sender;

		Navigation.PushAsync(new GamePage(button.Text, difficulty, numberOfQuestions));
	}

	// When the View Previous Games Button is clicked takes the player to the PreviousGamesPage.
	private void OnViewPreviousGamesBtnClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new PreviousGames());
	}

	// When the Change Difficulty button is clicked rotates through the difficulty options and updates the buttons text.
	private void ToggleDifficultyBtnClicked(object sender, EventArgs e)
	{
		difficulty = difficulty == GameDifficulty.Easy ? GameDifficulty.Challenging : difficulty == GameDifficulty.Challenging ? GameDifficulty.Hard : difficulty = GameDifficulty.Easy;
        Difficulty.Text = $"Difficulty : {difficulty}";
    }

	// When the Change Number of Questions button is clicked rotates through the number of questions options and updates the buttons text.
    private void ToggleNumberOfQuestionsBtnClicked(object sender, EventArgs e)
    {
		numberOfQuestions = numberOfQuestions == 5 ? 10 : numberOfQuestions == 10 ? 15 : numberOfQuestions == 15 ? 20 : numberOfQuestions = 5;
		NumberOfQuestions.Text = $"Number Of Questions : {numberOfQuestions}";
    }
}

