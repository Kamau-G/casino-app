using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Models
{
    [NotMapped]
    public class Card
    {
        private static string[] values = { "", "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "Ten", "Jack", "Queen", "King" };
        private static string[] suits = { "", "Clubs", "Diamonds", "Hearts", "Spades" };
        private static Random generator = new Random();

        private int value1;
        private int suit;

        public Card(int cardValue = 0, int cardSuit = 0)
        {
            value1 = cardValue;
            suit = cardSuit;
        }
        public int Value
        {

            get { return value1; }
            set
            {
                if (value >= 1 && value <= 13)
                    value1 = value;
                else
                    throw new ArgumentException($"Value must be an int between 1 and 13 inclusive. Attempted Value: {value}");
            }
        }
        public int Suit
        {
            get { return suit; }
            set
            {
                if (value >= 1 && value <= 4)
                    suit = value;
                else
                    throw new ArgumentException($"Suit must be an int between 1 and 4 inclusive. Attempted Value: {value}");
            }
        }
        public bool HasMatchingSuit(Card otherCard)
        {
            return suit == otherCard.suit;
        }
        public bool HasMatchingValue(Card otherCard)
        {
            return value1 == otherCard.value1;
        }
        public bool IsAce()
        {
            return value1 == 1;
        }
        public bool IsBlack()
        {
            return suit == 1 || suit == 4;
        }
        public bool IsRed()
        {
            return suit == 2 || suit == 3;
        }
        public bool IsClubs()
        {
            return suit == 1;
        }
        public bool IsDiamonds()
        {
            return suit == 2;
        }
        public bool IsHearts()
        {
            return suit == 3;
        }
        public bool IsSpades()
        {
            return suit == 4;
        }
        public bool IsFaceCard()
        {
            return value1 >= 11 && value1 <= 13;
        }

        public override string ToString()
        {
            return values[value1] + " of " + suits[suit];
        }
    }
}
