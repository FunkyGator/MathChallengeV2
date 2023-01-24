using SQLite;

namespace MathChallengeV2.Models
{
    [Table("game")]
    public class Game
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public GameOperation Type { get; set; }
        public int Score { get; set; }
        public DateTime DatePlayed { get; set; }
        public GameDifficulty Difficulty { get; set; }
    }

    public enum GameOperation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
    }

    public enum GameDifficulty
    {
        Easy,
        Challenging,
        Hard,
    }
}
