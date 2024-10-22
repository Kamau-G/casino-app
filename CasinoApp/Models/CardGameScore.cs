using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Models
{
    public class CardGameScore{
        [Key] public int Key { get; set; }
        public int Score { get; set; }
        [ForeignKey("UserKey")]
        public AppUser? Player { get; set; }
    }
}
