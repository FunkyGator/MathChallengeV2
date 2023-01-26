using MathChallengeV2.Models;

namespace MathChallengeV2;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	public GameDifficulty Difficulty { get; set; }
	public int NumberOfQuestions { get; set; }
	public string OriginalGameType { get; set; }
	public int GamesLeft { get; set; }
	int currentQuestion = 1;
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;

	public GamePage(string gameType, GameDifficulty difficulty, int numberOfQuestions)
	{
		InitializeComponent();

		GameType = gameType;
		Difficulty = difficulty;
		NumberOfQuestions = numberOfQuestions;
		GamesLeft = numberOfQuestions;
		OriginalGameType = gameType;

		BindingContext = this;

		CreateNewQuestion();
	}

	private void CreateNewQuestion()
	{
		var random = new Random();
		var randomGameType = 0;
		int lowNum;
		int highNum;

		if (OriginalGameType == "?")
		{
			randomGameType = random.Next(1, 4);
			switch (randomGameType) 
			{
				case 1:
					GameType = "+";
					break;
				case 2:
					GameType = "-";
					break;
				case 3:
					GameType = "×";
					break;
				case 4:
					GameType = "÷";
                    break;
			}
		}

		if (GameType == "÷")
		{
			if (Difficulty == GameDifficulty.Easy)
			{
				lowNum = 10;
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
				highNum = 9999;
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

            firstNumber = random.Next(lowNum, highNum);
            secondNumber = random.Next(lowNum, highNum);

            if (GameType == "-")
			{
				while (firstNumber <= secondNumber)
				{
					firstNumber = random.Next(lowNum, highNum);
					secondNumber = random.Next(lowNum, highNum);
				}
			}
		}

		

		NumberOfQuestionsLabel.Text = $"Question Number {currentQuestion} of {NumberOfQuestions}";

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

		GamesLeft--;
		currentQuestion++;
		AnswerEntry.Text = "";

		if (GamesLeft > 0)
			CreateNewQuestion();
		else
			GameOver();
    }

	private void GameOver()
	{
		if (OriginalGameType == "?") GameType = "?";

		GameOperation gameOperation = GameType switch
        {
            "+" => GameOperation.Addition,
            "-" => GameOperation.Subtraction,
            "×" => GameOperation.Multiplication,
            "÷" => GameOperation.Division,
			"?" => GameOperation.Random,
        };

		QuestionArea.IsVisible = false;
		BackTOMenuBtn.IsVisible = true;
		GameOverLabel.Text = $"Game over! You got {score} out of {NumberOfQuestions} right";

		App.GameRepository.Add(new Game
		{
			DatePlayed = DateTime.Now,
			Type = gameOperation,
			Score = score,
			Difficulty = this.Difficulty,
			NumberOfQuestions = this.NumberOfQuestions,
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
		GamesLeft = NumberOfQuestions;

		Navigation.PushAsync(new MainPage());
	}
}