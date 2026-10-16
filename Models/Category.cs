using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay_POE.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public Event Event { get; set; }
        [Required, MaxLength(100)]
        public string CategoryName { get; set; }
        [Required, MaxLength(50)]
        public string CategoryType { get; set; }
        [Required, MaxLength(20)]
        public string AgeGroup { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}