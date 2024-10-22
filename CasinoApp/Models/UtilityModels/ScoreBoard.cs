using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Models.UtilityModels
{
    [NotMapped]
    public class ScoreBoard
    {
        public ScoreBoard() { }
        public ScoreBoard(string name) { UserName = name; }
        string UserName { get; set; } = "";
        int UserScore { get; set; } = 0;
    }
}
