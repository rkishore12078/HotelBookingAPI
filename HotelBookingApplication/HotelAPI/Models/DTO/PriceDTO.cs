using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models.DTO
{
    public class PriceDTO
    {
        public int H_Id { get; set; }
        [Required]
        public double MinValue { get; set; }
        [Required]
        public double MazValue { get; set; }
    }
}
