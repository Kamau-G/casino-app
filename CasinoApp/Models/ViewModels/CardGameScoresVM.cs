using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Models.ViewModels
{
    [NotMapped]
    public class CardGameScoresVM
    {
        [Key] public int UserId { get; set; }
        public List<int> Scores { get; set; }
    }
}
