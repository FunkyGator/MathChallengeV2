using MathChallengeV2.Models;
using Microsoft.Maui.Platform;

namespace MathChallengeV2;

public partial class PreviousGameDetails : ContentPage
{
	

	public PreviousGameDetails(Game gameDets)
	{
		InitializeComponent();
		
		score.Text = gameDets.Score.ToString();
		type.Text = gameDets.Type.ToString();
		datePlayed.Text = gameDets.DatePlayed.ToString("M/d/yyyy");
		difficulty.Text = gameDets.Difficulty.ToString();
		numberOfQuestions.Text = gameDets.NumberOfQuestions.ToString();

		gameDetails.ItemsSource = gameDets.GameDetails;

    }

    private void OnBackToGamesHistoryBtnClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void OnBackToMenuBtnClicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new MainPage());
    }
}