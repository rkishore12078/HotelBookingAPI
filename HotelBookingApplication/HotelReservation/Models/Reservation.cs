using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Models
{
    public class Reservation
    {
        [Key]
        public int R_id { get; set; }
        [Required]
        public uint U_id { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int H_id { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
