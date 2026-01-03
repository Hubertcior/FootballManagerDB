using FootballManager.Data;
using FootballManager.Model;
using FootballManager.Repository;

namespace FootballManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Context())
            {
                SeedDatabase(db);
                PlayerRepository playerRepo = new PlayerRepository(db);
                ClubsRepository clubsRepository = new ClubsRepository(db);
                CoachRepository coachRepository = new CoachRepository(db);
                GoalRepository goalRepository = new GoalRepository(db);

                var actions = new Dictionary<string, MenuAction>
            {
                { "1", new MenuAction("Show All Players", playerRepo.ShowAllPlayers) },
                { "2", new MenuAction("Show Players by Position", () => FilterByPosition(playerRepo)) },
                { "3", new MenuAction("Add New Player", playerRepo.AddPlayer) },
                { "4", new MenuAction("Update Player Position", () => UpdatePlayerField(playerRepo,playerRepo.UpdatePlayerPosition)) },
                { "5", new MenuAction("Update Player Club", () => UpdatePlayerField(playerRepo, playerRepo.UpdatePlayerClub)) },
                { "6", new MenuAction("Delete Player", () => UpdatePlayerField(playerRepo, playerRepo.DeletePlayerByPlayerId)) },
                { "7", new MenuAction("Show All Clubs", clubsRepository.ShowAllClubs) },
                { "8", new MenuAction("Add New Club", clubsRepository.AddClub) },
                { "9", new MenuAction("Add New Coach", coachRepository.AddCoach) },
                { "10", new MenuAction("Show All Coaches", coachRepository.ShowAllCoaches)},
                { "11", new MenuAction("Update Coach Club",() => UpdateCoachField(coachRepository ,coachRepository.UpdateCoachClub))},
                { "12", new MenuAction("Set Player Goals", () => UpdatePlayerField(playerRepo, goalRepository.SetPlayerGoals))  },
                { "13", new MenuAction("Show Best Striker", goalRepository.ShowBestStriker) },
                { "14", new MenuAction("Set Player Assists", () => UpdatePlayerField(playerRepo, goalRepository.SetPlayerAsists))  },
                { "15", new MenuAction("Show Best Assistant", goalRepository.ShowBestAssistant) },
                { "16", new MenuAction("Show Player Stats", ()=> ShowPlayerGoals(playerRepo, goalRepository))  },
                { "17", new MenuAction("Delete Coach",() => UpdateCoachField(coachRepository ,coachRepository.DeleteCoachByCoachId))},
                { "18", new MenuAction("Delete Club", () => UpdateClubField(clubsRepository, clubsRepository.DeleteClubByClubId)) },
                { "0", new MenuAction("Exit", () => Environment.Exit(0)) }
            };

                while (true)
                {
                    Console.Clear();

                    var keys = actions.Keys.ToList();
                    int columns = 4;

                    Console.WriteLine("=== FOOTBALL MANAGER ===");
                    for (int i = 0; i < keys.Count; i += columns)
                    {
                        string line = "";
                        for (int j = 0; j < columns; j++)
                        {
                            if (i + j < keys.Count)
                            {
                                var key = keys[i + j];
                                var description = actions[key].Description;
                                line += string.Format("{0}. {1,-28}", key, description);
                            }
                        }
                        Console.WriteLine(line);
                    }

                    Console.Write("\nSelect option: ");
                    string choice = Console.ReadLine() ?? "";

                    if (actions.TryGetValue(choice, out var menuAction))
                    {
                        menuAction.Action.Invoke();
                        WaitForKey();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option!");
                        WaitForKey();
                    }
                }
            }

            static void UpdatePlayerField(PlayerRepository repo,Action<int> repoMethod)
            { 
                repo.ShowAllPlayers();
                Console.Write("Enter Player ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                    repoMethod(id);
            }
            static void UpdateClubField(ClubsRepository repo, Action<int> repoMethod)
            {
                repo.ShowAllClubs();
                Console.Write("Enter Club ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                    repoMethod(id);
            }
            static void UpdateCoachField(CoachRepository repo, Action<int> repoMethod)
            {
                repo.ShowAllCoaches();
                Console.Write("Enter Coach ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                    repoMethod(id);
            }

            static void ShowPlayerGoals(PlayerRepository repo, GoalRepository goal)
            {
                repo.ShowAllPlayers();
                Console.Write("Enter Player ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                    goal.ShowPlayerGoals(id);
            }

            static void FilterByPosition(PlayerRepository repo)
            {
                repo.ShowAllPositions();
                Console.Write("Type position ID: ");
                if (int.TryParse(Console.ReadLine(), out int pId))
                    repo.ShowPlayerByPosition(pId);
            }

            static void SeedDatabase(Context db)
            {
                if (!db.Positions.Any())
                {
                    Console.WriteLine("Seeding default positions...");

                    var defaultPositions = new List<Position>
                {
                    new Position { PositionId = 1, PositionName = "Striker" },
                    new Position { PositionId = 2, PositionName = "Midfielder" },
                    new Position { PositionId = 3, PositionName = "Defender" },
                    new Position { PositionId = 4, PositionName = "Goalkeeper" }
                };

                    db.Positions.AddRange(defaultPositions);
                    db.SaveChanges();
                    Console.WriteLine("Positions initialized!");
                }
            }
            static void WaitForKey()
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

    }
}