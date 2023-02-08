using MathChallengeV2.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace MathChallengeV2.Data
{
    public class GameRepository
    {
        string _dbPath;
        private SQLiteConnection conn;

        public GameRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void CreateTable()
        {
            try
            {
                conn = new SQLiteConnection(_dbPath);
                conn.CreateTable<Game>();
                conn.CreateTable<GameDetails>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal void Add(Game game, List<GameDetails> GameDetailsList)
        {
            try
            {
                conn = new SQLiteConnection(_dbPath);

                conn.Insert(game);

                foreach(GameDetails GameDetail in GameDetailsList)
                {
                    conn.Insert(GameDetail);
                }

                game.GameDetails = GameDetailsList;
                conn.UpdateWithChildren(game);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<Game> GetAllGames()
        {
           try
            {
                CreateTable();
                // return conn.Table<Game>().ToList();

                var gamesList = conn.GetAllWithChildren<Game>();

                return gamesList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<Game>();
        }

        internal void Delete(int id)
        {
            try
            {
                conn = new SQLiteConnection(_dbPath);
                Game game = conn.GetWithChildren<Game>(id);
                conn.Delete(game, true);
                //conn.Delete(new GameDetails { GameDetailsId = id });
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
