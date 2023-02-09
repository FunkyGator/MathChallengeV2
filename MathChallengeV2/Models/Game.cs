using SQLite;
using SQLiteNetExtensions.Attributes;

// This class defines the data structure that is pulled from the "game" table of the database.
namespace MathChallengeV2.Models
{
    [Table("game")]
    public class Game
    {
        public bool ViewDetails { get; set; }

        [PrimaryKey, AutoIncrement, Column("GameId")]
        public int GameId { get; set; }

        public GameOperation Type { get; set; }
        public int Score { get; set; }
        public DateTime DatePlayed { get; set; }
        public GameDifficulty Difficulty { get; set; }
        public int NumberOfQuestions { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<GameDetails> GameDetails { get; set; }

        // Method for changing the status of the ViewDetails Property
        public void ToggleViewDetails()
        {
            ViewDetails = !ViewDetails;
        }
    }

    public enum GameOperation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Random
    }

    public enum GameDifficulty
    {
        Easy,
        Challenging,
        Hard,
    }
}
