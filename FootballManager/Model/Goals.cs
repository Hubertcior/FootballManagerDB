using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FootballManager.Model
{
    public class Goals
    {
        [Key]
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public int ScoredGoals { get; set; }
        public int Assists { get; set; }
        public Player? Player { get; set; }
    }
}
