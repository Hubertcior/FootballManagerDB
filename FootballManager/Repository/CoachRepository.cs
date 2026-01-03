using FootballManager.Data;
using FootballManager.Model;
using Microsoft.EntityFrameworkCore;


namespace FootballManager.Repository
{
    internal class CoachRepository(Context db) : BaseRepository(db)
    {
        public void AddCoach()
        {
            Console.WriteLine("--- ADD NEW COACH ---");
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Enter surname: ");
            string surname = Console.ReadLine() ?? "";
            Console.Write("Enter expirience years: ");
            int.TryParse(Console.ReadLine(), out int years);

            int? clubId = EnterClubId();

            var newCoach = new Coach
            {
                FirstName = name,
                Surname = surname,
                ExperienceYears = years,
                ClubId = clubId
            };
            db.Coaches.Add(newCoach);
            db.SaveChanges();
            Console.WriteLine($"\nSuccessfully added: {name} {surname}");
        }

        public void ShowAllCoaches()
        {
            var coaches = db.Coaches.Include(c => c.Club).ToList();
            foreach (var c in coaches)
            {
                string club = ClubNameToDisplay(c);
                Console.WriteLine($"{c.CoachId}. {c.FirstName} {c.Surname}, Experience: {c.ExperienceYears} years. Club: {club}");
            }
        }
        public void UpdateCoachClub(int coachId)
        {
            var coach = db.Coaches.Find(coachId);
            if (coach != null)
            {
                int? newClubId = EnterClubId();
                coach.ClubId = newClubId;
                db.SaveChanges();
                Console.WriteLine($"\nSuccessfully updated club for Coach ID {coachId}");
            }
            else
            {
                Console.WriteLine($"Error: Coach with ID {coachId} does not exist.");
            }
        }

        public void DeleteCoachByCoachId(int coachId)
        {
            var coach = db.Coaches.Find(coachId);
            if (coach != null)
            {
                db.Coaches.Remove(coach);
                db.SaveChanges();
                Console.WriteLine($"\nSuccessfully deleted Coach {coach.FirstName} {coach.Surname}.");
            }
            else
            {
                Console.WriteLine($"Error: Coach with ID {coachId} does not exist.");
            }
        }

    }
}
