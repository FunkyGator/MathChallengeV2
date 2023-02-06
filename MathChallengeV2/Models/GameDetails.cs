using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MathChallengeV2.Models
{
    [Table("gameDetails")]
    public class GameDetails
    {
        [PrimaryKey, AutoIncrement, Column("GameDetailsId")]
        public int GameDetailsId { get; set; }

        [ForeignKey(typeof(Game)), Column("GameId")]
        public int GameId { get; set; }
        public int FirstNumber { get; set; }
        public string Operator { get; set; }
        public int SecondNumber { get; set; }
        public int Answer { get; set; }
        public string Result { get; set; }

        [ManyToOne]
        public Game Game { get; set; }
    }
}
