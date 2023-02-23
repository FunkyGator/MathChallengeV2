using MathChallengeV2.Models;

namespace MathChallengeV2;


// Codebehind for the GamePage
public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	public GameDifficulty Difficulty { get; set; }
	public int NumberOfQuestions { get; set; }
	public string OriginalGameType { get; set; }
	public int GamesLeft { get; set; }
	public int Answer { get; set; }

	List<GameDetails> GameDetailsList = new();
	int currentQuestion = 1;
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;
	int correctAnswer;

	public GamePage(string gameType, GameDifficulty difficulty, int numberOfQuestions)
	{
		InitializeComponent();

		Loaded += GamePage_Loaded;

		GameType = gameType;
		Difficulty = difficulty;
		NumberOfQuestions = numberOfQuestions;
		GamesLeft = numberOfQuestions;
		OriginalGameType = gameType;

		BindingContext = this;

		CreateNewQuestion();
	}

	// When the page is loaded put the focus on AnswerEntry.
	private void GamePage_Loaded(object sender, EventArgs e)
	{
		AnswerEntry.Focus();
	}

	// Creates a new question.
	private void CreateNewQuestion()
	{
		var random = new Random();
		int randomGameType;
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

            firstNumber = random.Next(lowNum, highNum);
            secondNumber = random.Next(lowNum, highNum);

            while (firstNumber <= secondNumber || firstNumber % secondNumber != 0)
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
		string questionType = "";

		switch (GameType)
		{
			case "+":
				questionType = "Addition";
				break;
			case "-":
				questionType = "Subtraction";
				break;
			case "×":
				questionType = "Multiplication";
                break;
			case "÷":
				questionType = "Division";
				break;
        }

		if (OriginalGameType == "?") questionType = "Random";


		QuestionType.Text = $"{questionType} Challenge";

		NumberOfQuestionsLabel.Text = $"Question Number {currentQuestion} of {NumberOfQuestions}";

        QuestionLabel.Text = $"{firstNumber} {GameType} {secondNumber} = ";

		AnswerEntry.Focus();
	}

	// Validates that the answer given is an integer.  If not prompts to make it an integer.
    private void ValidateAnswer(object sender, EventArgs e)
    {
        if (Int32.TryParse(AnswerEntry.Text, out int answer)) ValidAnswerSubmitted(answer);
        else
        {
			AnswerLabel.TextColor = Colors.Red;
            AnswerLabel.Text = "Your answer needs to be a Whole Number.";
        }
    }

	// When a valid answer is given this evaluates whether it is correct or not.
	// Also stores the correct answer if wrong to reveal at the end if the player wishes to see the correct answer.
    private void ValidAnswerSubmitted(int answer)
	{
		Answer = answer;
		var isCorrect = false;


		switch (GameType)
		{
			case "+":
				if (Answer == firstNumber + secondNumber) isCorrect = true;
				else correctAnswer = firstNumber + secondNumber;
				break;

			case "-":
				if (Answer == firstNumber - secondNumber)
				isCorrect = true;
				else correctAnswer = firstNumber - secondNumber;
				break;

			case "×":
				if (Answer == firstNumber * secondNumber) isCorrect = true;
				else correctAnswer = firstNumber * secondNumber;
				break;

			case "÷":
				if (Answer == firstNumber / secondNumber) isCorrect = true;
				else correctAnswer = firstNumber / secondNumber;
                break;
		}

		ProcessAnswer(isCorrect);

		GamesLeft--;
		currentQuestion++;

    }

	// Displays the result of the answer and adds the problem to the GameDetailsList to be added to the database later.
    private void ProcessAnswer(bool isCorrect)
    {
        score = isCorrect ? score += 1 : score;

        if (isCorrect)
        {
			AnswerLabel.TextColor = Colors.Green;
            AnswerLabel.Text = "Correct!";
            SubmitAnswer.IsVisible = false;
            Continue.IsVisible = true;
        }
        else
        {
			AnswerLabel.TextColor = Colors.Red;
            AnswerLabel.Text = "Incorrect!";
            AnswerEntry.TextColor = Colors.Red;
            SubmitAnswer.IsVisible = false;
            IncorrectAnswer.IsVisible = true;
        }

		GameDetailsList.Add(new GameDetails()
		{
			FirstNumber = firstNumber,
			Operator = GameType,
			SecondNumber= secondNumber,
			Answer = this.Answer,
			Result = AnswerLabel.Text,
		});
    }

	// Displays the Game Over! message and adds the results of the game to the database.
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
              _ => GameOperation.Random,
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
		},  GameDetailsList) ;
	}

	// Returns to the MainPage when the button is pressed.
	// Resets game parameters upon exiting.
	private void OnBackToMenuBtnClicked(object sender, EventArgs e)
	{
		score = 0;
		GamesLeft = NumberOfQuestions;

		Navigation.PopAsync();
	}

	// Displays the next question when the Continue button is pressed after a correnct answer.
	// Also clears the AnswerLabel and AnswerEntry values for the next question.
	// Keeps track of the GamesLeft and takes player to the game over screen when done.
    private void ContinueBtnClicked(object sender, EventArgs e)
    {
		AnswerLabel.Text = "";
		SubmitAnswer.IsVisible = true;
		Continue.IsVisible = false;
        AnswerEntry.Text = "";
		AnswerEntry.TextColor = Colors.White;

        if (GamesLeft > 0) CreateNewQuestion();
        else GameOver();
    }

	// Displays the next question when the Continue button is pressed after an incorrect answer.
	// Also clears the AnswerLabel and AnswerEntry values for the next question.
	// Keeps track of the GamesLeft and takes player to the game over screen when done.
    private void IncorrectContinueBtnClicked(object sender, EventArgs e)
    {
		AnswerLabel.Text = "";
		SubmitAnswer.IsVisible = true;
		IncorrectAnswer.IsVisible = false;
        AnswerEntry.Text = "";
		AnswerEntry.TextColor = Colors.White;

        if (GamesLeft > 0) CreateNewQuestion();
        else GameOver();
    }

	// Displays the correct answer if the player wishes to see it.
    private void IncorrectBtnClicked(object sender, EventArgs e)
    {
		AnswerEntry.TextColor = Colors.Green;
		AnswerEntry.Text = correctAnswer.ToString();
    }
}