using System.ComponentModel.DataAnnotations;

namespace MVCtest.Models
{
    public class SignupModel
    {
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is missing or invalid")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage ="Incorrect or missing password")]
        public string Password { get; set; }

    }
}
