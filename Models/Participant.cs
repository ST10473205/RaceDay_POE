using System.ComponentModel.DataAnnotations;

namespace RaceDay_POE.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantID { get; set; }
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required, MaxLength(10)]
        public string Gender { get; set; }
        [Required, MaxLength(100)]
        public string Password { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}
