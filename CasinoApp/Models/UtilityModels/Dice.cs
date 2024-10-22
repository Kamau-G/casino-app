using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Models.UtilityModels
{
    [NotMapped]
    public class Dice
    {
        public Dice(int sides = 6) { Sides = sides; }
        Random _rand = new Random();
        public int Sides { get; set; }
        public int Roll()
        {
            return _rand.Next(1, Sides + 1);
        }
    }
}
