using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solitaire
{
    class Deck
    {
        private int currentcard;
        public Card[] cards;
        public Deck()
        {
            cards = new Card[52];
            FillDeck();
        }
        void FillDeck()
        {
            currentcard = 0;
            for (int i = 0; i < 13; i++)
            {
                cards[i] = new Card(i + 1, Card.Suits.Clubs);
                cards[i + 13] = new Card(i + 1, Card.Suits.Hearts);
                cards[i + 26] = new Card(i + 1, Card.Suits.Diamonds);
                cards[i + 39] = new Card(i + 1, Card.Suits.Spades);
            }
        }
        public void Shuffle()
        {
            currentcard = 0;
            Random r = new Random();
            bool[] used = new bool[52];
            int[] randomorder = new int[52];
            int poz;
            for (int i = 0; i < 52; i++)
                used[i] = false;
            for(int i = 0; i < 52; i++)
            {
                poz = r.Next(52 - i) + 1;
                int j, k;
                k = 0;
                for(j = 0; j < 52; j++)
                {
                    if (!used[j])
                        k++;
                    if(k == poz)
                        break;
                }
                if (used[j] == false)
                    randomorder[i] = j;
                used[j] = true;
            }
            for(int i = 0; i < 52; i++)
            {
                Card aux;
                aux = cards[i];
                cards[i] = cards[randomorder[i]];
                cards[randomorder[i]] = aux;
            }

        }
        public Card DrawCard()
        {
            if (currentcard >= 52)
                currentcard -= 52;
            currentcard++;
            return cards[currentcard - 1];
            
        }

    }
}
