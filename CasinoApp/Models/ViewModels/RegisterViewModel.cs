using System.ComponentModel.DataAnnotations;


    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(256)]
        public string? Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(256)]
        public string? Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(256)]
        public string? Lastname { get; set; } = string.Empty;

        // from IdentityUser class
        [Required(ErrorMessage = "Please enter an email address.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string? Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; } = string.Empty;
    }
