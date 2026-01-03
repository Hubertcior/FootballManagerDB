using System.ComponentModel.DataAnnotations;

namespace FootballManager.Model
{
    public class Coach
    {
        [Key]
        public int CoachId { get; set; }
        public required string FirstName { get; set; }
        public required string Surname { get; set; }
        public int ExperienceYears { get; set; }
        public int? ClubId { get; set; }
        public Club? Club { get; set; }
    }
}
