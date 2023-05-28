using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class Room
    {
        [Key]
        public int R_id { get; set; }
        [Required]
        public int H_id { get; set; }
        [ForeignKey("H_id")]
        public Hotel? Hotels { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Room Number should be less than 1000")]
        public int RoomNumber { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Price cannot be negative")]
        public double Price { get; set; }
        [Required]
        [Range(1,10,ErrorMessage ="Capacity should be less than 10")]
        public int Capacity { get; set; }
        [Required]
        [MaxLength(10,ErrorMessage ="Character should not exceed above 10")]
        public string? Type { get; set; }
    }
}
