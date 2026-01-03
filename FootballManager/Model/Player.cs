using System.ComponentModel.DataAnnotations;

namespace FootballManager.Model
{
    public class Player
    {
        [Key] 
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public int? ClubId { get; set; }
        public Club? Club { get; set; }
        public int? PositionId { get; set; }
        public Position? Position { get; set; }
        public required int Age { get; set; }
        public Goals? Goals { get; set; }
    }
}
