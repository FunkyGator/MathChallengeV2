using MathChallengeV2.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace MathChallengeV2.Data
{
    // All the methods for executing the SQLite to manage the database.
    public class GameRepository
    {
        string _dbPath;
        private SQLiteConnection conn;

        public GameRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        // Checks to see if there is a database.  If there is it checks to see if there the tables are there.  If not then it creates them.
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
        
        // Adds a new Game and the Game details to the Game and GameDetail tables in the database.
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

        // Updates the database after edits made.  Currently, the only use if for when changing the ViewDetails toggle.
        internal List<Game> Update(Game game, List<GameDetails> GameDetailsList)
        { 
            game.GameDetails = GameDetailsList;

            conn.UpdateWithChildren(game);

            var gamesList = conn.GetAllWithChildren<Game>();

            return gamesList;
        }

        // Gets all of the Games and GameDetails for the Previous Games screen.  Also calls CreateTable in case the database or tables do not exist yet.
        public List<Game> GetAllGames()
        {
           try
            {
                CreateTable();

                var gamesList = conn.GetAllWithChildren<Game>();

                return gamesList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<Game>();
        }

        // Deletes a Game from the database.
        internal void Delete(int id)
        {
            try
            {
                conn = new SQLiteConnection(_dbPath);

                Game game = conn.GetWithChildren<Game>(id);

                conn.Delete(game, true);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
