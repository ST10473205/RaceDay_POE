using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay_POE.Models
{
    public class Route
    {
        [Key]
        public int RouteID { get; set; }
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public Event Event { get; set; }
        [Column(TypeName = "decimal(4,2)")]
        public decimal Distancekm { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
        [Required, MaxLength(100)]
        public string Location { get; set; }
    }
}
