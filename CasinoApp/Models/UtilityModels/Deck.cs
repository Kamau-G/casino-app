using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace CasinoApp.Models.UtilityModels
{
    [NotMapped]
    public class Deck
    {
        // can instantiate the list here OR in the constructor
        private List<Card> cards = new List<Card>();

        public Deck()
        {
            // 13 values
            for (int value = 1; value <= 13; value++)
                // in each of 4 suits
                for (int suit = 1; suit <= 4; suit++)
                    // create the card and add it to the list
                    cards.Add(new Card(value, suit));
        }

        // read-only property
        public int NumCards
        {
            get
            {
                return cards.Count;
            }
        }

        // read-only property
        public bool IsEmpty
        {
            get
            {
                return cards.Count == 0;
            }
        }

        public Card this[int i]
        {
            get
            {
                return cards[i];
            }
        }

        // dealing from the deck should return a card object
        public Card Deal()
        {
            // if the deck still has cards
            if (!IsEmpty)
            {
                // get a refernce to the first card
                Card c = cards[0];
                // remove the card from the list
                // could have used cards.RemoveAt[0];
                cards.Remove(c);
                // return the first card
                return c;
            }
            // when the deck is empty, return null or throw an exception
            return null;
        }

        public void Shuffle()
        {
            Random gen = new Random();
            // go through all of the cards in the deck
            for (int i = 0; i < NumCards; i++)
            {
                // generate a random index
                int rnd = gen.Next(0, NumCards);
                // swap the card at the random index with the card at the current index
                /* Card c = cards[rnd];
                 cards[rnd] = cards[i];
                 cards[i] = c;*/
                // Updated this code
                (cards[rnd], cards[i]) = (cards[i], cards[rnd]);
            }
        }

        public override string ToString()
        {
            string output = "";
            // go through every card in the deck
            foreach (Card c in cards)
                // ask the card to convert itself to a string
                output += c.ToString() + "\n";
            return output;
        }
    }
}
