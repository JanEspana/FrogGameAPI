using System.ComponentModel.DataAnnotations;

namespace FrogGameAPI.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Character {  get; set; }
    }
}
