using MathChallengeV2.Models;
using System;

namespace MathChallengeV2;

public partial class PreviousGames : ContentPage
{
	public PreviousGames()
	{
		InitializeComponent();
		App.GameRepository.GetAllGames();

		gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}

	private void OnDelete(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;

		App.GameRepository.Delete((int)button.BindingContext);

		gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}

    private void OnBackToMenu(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void OnDetails(object sender, EventArgs e)
    {

    }
}