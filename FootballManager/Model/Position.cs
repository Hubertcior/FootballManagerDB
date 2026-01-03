

using System.ComponentModel.DataAnnotations;

namespace FootballManager.Model
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public required string PositionName { get; set; }
    }
}
