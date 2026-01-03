using FootballManager.Data;
using FootballManager.Model;


namespace FootballManager.Repository
{
    internal class ClubsRepository(Context db) : BaseRepository(db)
    {
        public void AddClub()
        {
            Console.WriteLine("--- ADD NEW CLUB ---");
            Console.Write("Enter name: ");
            string name = Console.ReadLine() ?? "";

            int dateOfEstablished = ReadPositiveInt("Date of Established");

            Club club = new Club
            {
                ClubName = name,
                DateOfEstablishment = dateOfEstablished
            };


            db.Clubs.Add(club);
            db.SaveChanges();
        }
        public void ShowAllClubs()
        {
            db.Clubs.ToList().ForEach(c =>
            {
                Console.WriteLine($"{c.ClubId}. {c.ClubName}, Established: {c.DateOfEstablishment}");
            });
        }

        public void DeleteClubByClubId(int clubId)
        {
            var club = db.Clubs.Find(clubId);
            if (club == null)
            {
                Console.WriteLine($"Error: Club with ID {clubId} does not exist.");
                return;
            }

            var playersInClub = db.Players.Where(p => p.ClubId == clubId).ToList();
            foreach (var p in playersInClub)
            {
                p.ClubId = null;
            }

            var coachInClub = db.Coaches.FirstOrDefault(c => c.ClubId == clubId);
            if (coachInClub != null)
            {
                coachInClub.ClubId = null;
            }

            db.Clubs.Remove(club);
            db.SaveChanges();
            Console.WriteLine($"\nSuccessfully deleted club {club.ClubName}. Players are now Free Agents.");
        }
    }
}
