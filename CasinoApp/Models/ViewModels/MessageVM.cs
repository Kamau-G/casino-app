using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CasinoApp.Models.ViewModels
{
    [NotMapped]
    public class MessageVM
    {
        [Required(ErrorMessage = "Who is this going to?")]
        public String? Rcpnt { get; set; } = null;
        [Required]
        [StringLength(256, MinimumLength = 16, ErrorMessage = "Reply must be between 16 - 256 characters")]
        public string? Text { get; set; } = "";
    }
}
