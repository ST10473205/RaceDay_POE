using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay_POE.Models
{
    public class Entry
    {
        [Key]
        public int EntryID { get; set; }
        public int ParticipantID { get; set; }
        [ForeignKey("ParticipantID")]
        public Participant Participant { get; set; }
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public Event Event { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;
        [Required, MaxLength(20)]
        public string Status { get; set; } = "Pending";
        public Results Results { get; set; }
    }
}
