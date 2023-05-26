using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Hotel
    {
        [Key]
        public int H_id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Amenities { get; set; }
    }
}
