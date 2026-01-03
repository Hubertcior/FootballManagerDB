using System.ComponentModel.DataAnnotations;

namespace FootballManager.Model
{
    public class Club 
    {
        [Key]
        public int ClubId {  get; set; }
        public required string ClubName { get; set; }
        public required int DateOfEstablishment { get; set; }

        public List<Player> Players { get; set; } = [];
        public Coach? Coach { get; set; }
    }
}
