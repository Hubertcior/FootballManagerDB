using FootballManager.Data;
using FootballManager.Model;
using Microsoft.EntityFrameworkCore;

namespace FootballManager.Repository
{
    internal class PlayerRepository(Context db) : BaseRepository(db)
    {
        public void AddPlayer()
        {
            Console.WriteLine("--- ADD NEW PLAYER ---");
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter surname: ");
            string surname = Console.ReadLine() ?? "";

            int? selectedClubId = EnterClubId();

            int? selectedPositionId = EnterPositionId();

            Console.Write("Enter age: ");
            int.TryParse(Console.ReadLine(), out int age);

            var newPlayer = new Player
            {
                Name = name,
                Surname = surname,
                ClubId = selectedClubId,
                PositionId = selectedPositionId,
                Age = age
            };


            db.Players.Add(newPlayer);
            db.SaveChanges();

            Console.WriteLine($"\nSuccessfully added: {name} {surname}");
        }
        public void ShowAllPlayers()
        {
            var players = db.Players.Include(p => p.Club).Include(p => p.Position).ToList();

            foreach (var p in players)
            {
                string club = ClubNameToDisplay(p);
                string pos = PositionNameToDisplay(p);
                Console.WriteLine($"{p.Id}. {p.Name} {p.Surname} [{club}] - {pos}");
            }
        }
        public void ShowAllPositions()
        {
            Console.WriteLine("\nAvailable Positions:");
            var positions = db.Positions.ToList();
            foreach (var p in positions)
            {
                Console.WriteLine($"ID: {p.PositionId} | Name: {p.PositionName}");
            }
        }
        public void ShowPlayerByPlayerId(int id)
        {
            var player = db.Players
                .Include(p => p.Club)
                .Include(p => p.Position)
                .FirstOrDefault(p => p.Id == id);

            if (player == null)
            {
                Console.WriteLine("Player not found.");
                return;
            }

            string clubName = ClubNameToDisplay(player);
            string positionName = PositionNameToDisplay(player);

            Console.WriteLine($"{player.Id}. {player.Name} {player.Surname}, Club: {clubName}, Position: {positionName}, Age: {player.Age}");
        }

        public void ShowPlayerByPosition(int position)
        {
            var players = db.Players
                .Include(p => p.Club)
                .Include(p => p.Position)
                .Where(p => p.PositionId == position)
                .ToList();
            if (players.Count == 0)
            {
                 Console.WriteLine("No players found for the selected position.");
                return;
            }
            foreach (var player in players)
            {
                string clubName = ClubNameToDisplay(player);
                string positionName = PositionNameToDisplay(player);
                Console.WriteLine($"{player.Id}. {player.Name} {player.Surname}, Club: {clubName}, Position: {positionName}, Age: {player.Age}");
            }
        }

        public void DeletePlayerByPlayerId(int id)
        {
            var player = GetPlayerByID(id);
            if (player == null)
            {
                return;
            }
            db.Players.Remove(player);
            db.SaveChanges();
            Console.WriteLine($"{player.Id}. {player.Name} {player.Surname} deleted!");
        }

        public void UpdatePlayerClub(int playerId)
        {
            var player = GetPlayerByID(playerId);
            if (player == null)
            {
                return;
            }
            int? newClubId = EnterClubId();
            player.ClubId = newClubId;
            db.SaveChanges();
            var club = db.Clubs.FirstOrDefault(c => c.ClubId == newClubId);
            string clubName = club != null ? club.ClubName : "Free Agent";
            Console.WriteLine($"{player.Name} {player.Surname}'s club updated to {clubName}.");
        }

        public void UpdatePlayerPosition(int playerId)
        {
            var player = GetPlayerByID(playerId);
            if (player == null)
            {
                return;
            }
            int? newPositionId = EnterPositionId();
            player.PositionId = newPositionId;
            db.SaveChanges();

            var position = db.Positions.FirstOrDefault(p => p.PositionId == newPositionId);
            string positionName = position != null ? position.PositionName : "Unknown";
            Console.WriteLine($"{player.Name} {player.Surname}'s position updated to {positionName}.");
        }
        
    }
}
