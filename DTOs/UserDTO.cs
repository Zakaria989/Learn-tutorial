using System.ComponentModel.DataAnnotations;

namespace LearnTutorial.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters long.", MinimumLength = 3)]
        public required string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters long.", MinimumLength = 6)]
        public required string Password { get; set; }
        public int Id { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
