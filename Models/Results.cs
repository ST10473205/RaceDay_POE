using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay_POE.Models
{
    public class Results
    {
        [Key]
        public int ResultsID { get; set; }
        public int EntryID { get; set; }
        [ForeignKey("EntryID")]
        public Entry Entry { get; set; }
        [Required]
        public TimeSpan FinishTime { get; set; }
        public int FinishPosition { get; set; }
    }
}

