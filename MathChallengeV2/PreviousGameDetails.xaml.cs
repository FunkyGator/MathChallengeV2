using MathChallengeV2.Models;

namespace MathChallengeV2;

public partial class PreviousGameDetails : ContentPage
{
	

	public PreviousGameDetails(Game gameData)
	{
		InitializeComponent();

		gameDetails.ItemsSource = gameData.GameDetails;
	}
}