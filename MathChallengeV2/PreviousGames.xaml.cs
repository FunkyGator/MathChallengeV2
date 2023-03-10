using MathChallengeV2.Models;

namespace MathChallengeV2;

// Codebehind for the PreviousGames page.
public partial class PreviousGames : ContentPage
{
	List<Game> games = App.GameRepository.GetAllGames();

	public PreviousGames()
	{
		InitializeComponent();

		gamesList.ItemsSource = games;
	}

	// Sends the Id of the database record to the GameRepository delete method then updates list.
	private void OnDeleteBtnClicked(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;

		App.GameRepository.Delete((int)button.BindingContext);

		gamesList.ItemsSource = App.GameRepository.GetAllGames();

		
	}

	// Toggles whether the player can see the GameDetails under the game or not.
	private void OnDetailsBtnClicked(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;

		for (int i = 0; i < games.Count; i++)
		{ 
			if (games[i].GameId == (int)button.BindingContext) Navigation.PushAsync(new PreviousGameDetails(games[i]));
		}
    }

	// Returns the player to the MainPage.
    private void OnBackToMenuBtnClicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}