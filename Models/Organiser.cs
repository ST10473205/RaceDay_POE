using System.ComponentModel.DataAnnotations;

namespace RaceDay_POE.Models
{
    public class Organiser
    {
        [Key]
        public int OrganiserID { get; set; }
        [Required, MaxLength(50)]
        public string OrganiserName { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
