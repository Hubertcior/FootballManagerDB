using FootballManager.Data;
using FootballManager.Model;

namespace FootballManager.Repository
{
    internal class BaseRepository(Context db)
    {
        protected readonly Context db = db;

        public int? EnterClubId()
        {
            ShowClubs();
            Console.Write("\nSelect Club ID or type 0 if Free Agent: ");
            if (int.TryParse(Console.ReadLine(), out int clubId))
            {
                if (clubId == 0)
                {
                    return null;

                }
                if (db.Clubs.Any(c => c.ClubId == clubId))
                {
                    return clubId;
                }
                else
                {
                    Console.WriteLine($"Error: Club with ID {clubId} does not exist.");
                    return EnterClubId();
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return EnterClubId();
            }
        }
        private void ShowClubs()
        {
            Console.WriteLine("\nAvailable Clubs:");
            var clubs = db.Clubs.ToList();
            foreach (var c in clubs)
            {
                Console.WriteLine($"ID: {c.ClubId} | Name: {c.ClubName}");
            }
        }

        public int? EnterPositionId()
        {
            ShowPositions();
            Console.Write("\nSelect Position ID or type 0 if unknown: ");
            if (int.TryParse(Console.ReadLine(), out int positionId))
            {
                if (positionId == 0)
                {
                    return null;

                }
                if (db.Positions.Any(p => p.PositionId == positionId))
                {
                    return positionId;
                }
                else
                {
                    Console.WriteLine($"Error: Position with ID {positionId} does not exist.");
                    return EnterPositionId();
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return EnterPositionId();
            }
        }

        private void ShowPositions()
        {
            Console.WriteLine("\nAvailable Positions:");
            var positions = db.Positions.ToList();
            foreach (var p in positions)
            {
                Console.WriteLine($"ID: {p.PositionId} | Name: {p.PositionName}");
            }
        }
        public static string ClubNameToDisplay(Player player)
        {
            if (player.Club == null) {
                return "Free Agent";
            }
            return player.Club.ClubName;
        }
        public static string ClubNameToDisplay(Coach coach)
        {
            if (coach.Club == null) {
                return "Free Agent";
            }
            return coach.Club.ClubName;
        }
        public static string PositionNameToDisplay(Player player)
        {
            if (player.Position == null) {
                return "N/A";
            }
            return player.Position.PositionName;
        }
        public Player? GetPlayerByID(int id)
        {
            var player = db.Players.Find(id);
            if (player == null)
            {
                Console.WriteLine("Player not found.");
            }
            return player;
        }
        protected int ReadPositiveInt(string msg)
        {
            while (true) {
                Console.WriteLine($"{msg}");
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                {
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
        } 
    }
}