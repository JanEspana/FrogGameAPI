using System.ComponentModel.DataAnnotations;

namespace FrogGameAPI.Models
{
    public class Score
    {
        [Key]
        public int _id { get; set; }
        [Required]
        public int time { get; set; }
        [Required]
        public int numFlies { get; set; }
        [Required]
        public Player player { get; set; }
    }
}
