using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay_POE.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public int OrganiserID { get; set; }
        [ForeignKey("OrganiserID")]
        public Organiser Organiser { get; set; }
        [Required, MaxLength(100)]
        public string EventName { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required, MaxLength(50)]
        public string EventType { get; set; }
        public ICollection<Route> Routes { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Entry> Entries
        {
            get; set;
        }
    }
}
