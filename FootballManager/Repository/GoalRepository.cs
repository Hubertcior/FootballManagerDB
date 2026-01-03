using FootballManager.Data;
using FootballManager.Model;
using Microsoft.EntityFrameworkCore;


namespace FootballManager.Repository
{
    internal class GoalRepository(Context db) : BaseRepository(db)
    {
        public void SetPlayerGoals(int playerID)
        {
            var player = db.Players
                .Include(p => p.Goals)
                .FirstOrDefault(p => p.Id == playerID);
            if (player != null)
            {
                Console.WriteLine("Enter number of goals scored:");
                if (int.TryParse(Console.ReadLine(), out int newGoals))
                { 
                    if (player.Goals == null)
                    {
                        player.Goals = new Goals { PlayerId = player.Id, ScoredGoals = newGoals };
                        db.Goals.Add(player.Goals);
                    }
                    else
                    {
                        player.Goals.ScoredGoals = newGoals;
                    }

                    db.SaveChanges();
                    Console.WriteLine("Goals updated successfully!");
                }
            }
            else
            {
                Console.WriteLine("Player not found");
                return;
            }

        }

        public void SetPlayerAsists(int playerID)
        {
            var player = db.Players
                .Include(p => p.Goals)
                .FirstOrDefault(p => p.Id == playerID);
            if (player != null)
            {
                Console.WriteLine("Enter number of assists delivered:");
                if (int.TryParse(Console.ReadLine(), out int newAsist))
                {
                    if (player.Goals == null)
                    {
                        player.Goals = new Goals { PlayerId = player.Id, Assists = newAsist};
                        db.Goals.Add(player.Goals);
                    }
                    else
                    {
                        player.Goals.Assists = newAsist;
                    }

                    db.SaveChanges();
                    Console.WriteLine("Asists updated successfully!");
                }
            }
            else
            {
                Console.WriteLine("Player not found");
                return;
            }

        }

        public void ShowBestStriker()
        {
            var topScorer = db.Goals
                .Include(g => g.Player)
                .OrderByDescending(g => g.ScoredGoals)
                .FirstOrDefault();

            if (topScorer == null)
            {
                Console.WriteLine("No goals data available.");
                return;
            }
            else
            {
                Console.WriteLine($"Top Scorer: {topScorer.Player?.Name} {topScorer.Player?.Surname} with {topScorer.ScoredGoals} goals.");
            }
        }
        public void ShowBestAssistant()
        {
            var topAssistant = db.Goals
                .Include(g => g.Player)
                .OrderByDescending(g => g.Assists)
                .FirstOrDefault();

            if (topAssistant == null)
            {
                Console.WriteLine("No Assists data available.");
                return;
            }
            else
            {
                Console.WriteLine($"Top Assistant: {topAssistant.Player?.Name} {topAssistant.Player?.Surname} with {topAssistant.ScoredGoals} assists.");
            }
        }
        public void ShowPlayerGoals(int playerID)
        {
            var playerGoals = db.Goals
                .Include(g => g.Player)
                .FirstOrDefault(g => g.PlayerId == playerID);
            if (playerGoals == null)
            {
                Console.WriteLine("No goals data available for this player.");
                return;
            }
            else
            {
                Console.WriteLine($"Player: {playerGoals.Player?.Name} {playerGoals.Player?.Surname} - Goals: {playerGoals.ScoredGoals}, Assists: {playerGoals.Assists}");
            }
        }
    }
}
