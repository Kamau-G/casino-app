using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Models.ViewModels
{
    [NotMapped]
    public class DiceGameScoresVM
    {
        [Key] public int UserId { get; set; }
        public List<DiceGameScore> Scores { get; set; }
    }
}
