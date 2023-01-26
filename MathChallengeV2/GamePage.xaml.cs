using MathChallengeV2.Models;

namespace MathChallengeV2;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	public GameDifficulty Difficulty { get; set; }
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;
	const int totalQuestions = 2;
	int gamesLeft = totalQuestions;

	public GamePage(string gameType, GameDifficulty difficulty)
	{
		InitializeComponent();
		GameType = gameType;
		Difficulty = difficulty;
		BindingContext = this;

		CreateNewQuestion();
	}

	private void CreateNewQuestion()
	{
		var random = new Random();
		int lowNum;
		int highNum;

		if (GameType == "÷")
		{
			if (Difficulty == GameDifficulty.Easy)
			{
				lowNum = 1;
				highNum = 99;
			}
			else if (Difficulty == GameDifficulty.Challenging)
			{
				lowNum = 100;
				highNum = 999;
			}
			else
			{
				lowNum = 1000;
				highNum= 9999;
			}

			while (firstNumber < secondNumber || firstNumber % secondNumber != 0)
			{
				firstNumber = random.Next(lowNum, highNum);
				secondNumber = random.Next(lowNum, highNum);
			}
		}
		else
		{
			if (Difficulty == GameDifficulty.Easy)
			{
				lowNum = 1;
				highNum = 9;
			}
			else if (Difficulty == GameDifficulty.Challenging)
			{
				lowNum = 10;
				highNum = 99;
			}
			else
			{
				lowNum = 100;
				highNum = 999;
			}
		}
		firstNumber = random.Next(lowNum, highNum);
		secondNumber = random.Next(lowNum, highNum);

		QuestionLabel.Text = $"{firstNumber} {GameType} {secondNumber}";
	}

	private void OnAnswerSubmitted(object sender, EventArgs e)
	{
		var answer = Int32.Parse(AnswerEntry.Text);
		var isCorrect = false;

		switch (GameType)
		{
			case "+":
				isCorrect = answer == firstNumber + secondNumber;
				break;
			case "-":
				isCorrect= answer == firstNumber - secondNumber;
				break;
			case "×":
				isCorrect= answer == firstNumber * secondNumber;
				break;
			case "÷":
				isCorrect= answer == firstNumber / secondNumber;
                break;
		}

        ProcessAnswer(isCorrect);

		gamesLeft--;
		AnswerEntry.Text = "";

		if (gamesLeft > 0)
			CreateNewQuestion();
		else
			GameOver();
    }

	private void GameOver()
	{
		GameOperation gameOperation = GameType switch
        {
            "+" => GameOperation.Addition,
            "-" => GameOperation.Subtraction,
            "×" => GameOperation.Multiplication,
            "÷" => GameOperation.Division,
        };

		QuestionArea.IsVisible = false;
		BackTOMenuBtn.IsVisible = true;
		GameOverLabel.Text = $"Game over! You got {score} out of {totalQuestions} right";

		App.GameRepository.Add(new Game
		{
			DatePlayed = DateTime.Now,
			Type = gameOperation,
			Score = score,
			Difficulty = this.Difficulty,
		}) ;
	}

	private void ProcessAnswer(bool isCorrect) 
	{
		score = isCorrect ? score += 1 : score;
		AnswerLabel.Text = isCorrect ? "Correct!" : "Incorrect!";
	}

	private void OnBackToMenu(object sender, EventArgs e)
	{
		score = 0;
		gamesLeft = totalQuestions;

		Navigation.PushAsync(new MainPage());
	}
}