using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MathChallengeV2.Models
{
    [Table("game")]
    public class Game
    {
        [PrimaryKey, AutoIncrement, Column("GameId")]
        public int GameId { get; set; }
        public GameOperation Type { get; set; }
        public int Score { get; set; }
        public DateTime DatePlayed { get; set; }
        public GameDifficulty Difficulty { get; set; }
        public int NumberOfQuestions { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<GameDetails> GameDetails { get; set; }
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
