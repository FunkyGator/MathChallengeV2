using SQLite;

namespace MathChallengeV2.Models
{
    [Table("gameDetails")]
    public class GameDetails
    {
        [PrimaryKey, AutoIncrement, Column("GameDetailsId")]
        public int GameDetailsId { get; set; }
        public int Id { get; set; }
        public int FirstNumber { get; set; }
        public string Operator { get; set; }
        public int SecondNumber { get; set; }
        public int Answer { get; set; }
        public string Result { get; set; }
    }
}
