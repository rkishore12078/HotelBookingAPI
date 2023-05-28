using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        [MaxLength(20,ErrorMessage ="UserName should be 20 characters only")]
        public string Username { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Name should be 30 characters only")]
        public string Name { get; set; }

        public byte[]? Password { get; set; }

        public byte[]? HashKey { get; set; }
        [Required]
        [MaxLength(10,ErrorMessage ="Phone Numbers should be 10 characters")]
        [RegularExpression(@"^[0-9]{10}$")]
        public string? PhoneNumber { get; set; }
        [Required]
        [Range(18,100,ErrorMessage ="Age should greater than 18 and less than 100")]
        [DefaultValue(0)]
        public int Age { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
