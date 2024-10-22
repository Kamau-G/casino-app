using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Models
{
    public class Message
    {
        [Key] public int MsgId { get; set; }
        public Message(string text)
        {
            Text = text;
        }
        public Message() { }

        [Required(ErrorMessage = "Who is this going to?")]
        public AppUser? Rcpnt { get; set; } = null;
        [Required(ErrorMessage = "User must be logged in.")]
        public AppUser? Sndr { get; set; } = null;
        [Required]
        [StringLength(256, MinimumLength = 16, ErrorMessage = "Reply must be between 16 - 256 characters")]
        public string? Text { get; set; } = "";
        //[ForeignKey("convoId")] 
        //public int? convoId { get; set; }
        public List<Message>? Replies { get; set; } = new List<Message>();
        public bool? IsRead { get; set; } = false;

        //public DateTime? Date {  get; set; }= DateTime.UtcNow;

        public void Read()
        {
            this.IsRead = true;
        }

    }
}
