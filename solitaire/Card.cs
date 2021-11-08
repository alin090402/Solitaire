using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solitaire
{
    class Card
    {

        //public static string[] Colors = { "Red", "Black" };
        //public static string[] Suits = { "Clubs", "Diamnods", "Hearts", "Spades" };
        public enum Colors
        {
            Red,
            Black
        }
        public enum Suits
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public bool hidden;
        private int value;
        private Suits suit;
        private Colors color;
        /// <summary>
        /// create a new card with the given param
        /// </summary>
        /// <param name="value"> The value of the created card</param>
        /// <param name="color"> The color of the created card</param>
        /// <param name="suit"> The suit of the created card</param>
        public Card(int _value,  Suits _suit)
        {
            value = _value;
            suit = _suit;
            if (_suit == Suits.Diamonds || suit == Suits.Hearts)
                color = Colors.Red;
            else color = Colors.Black;
            hidden = false;
        }
        public void Hide()
        {
            hidden = true;
        }
        public void Show()
        {
            hidden = false;
        }

        /// <summary>
        /// return the value of the card
        /// </summary>
        public int Value
        {
            get
            {
                return value;
            }
        }
        /// <summary>
        /// the color of the card
        /// </summary>
        public string Color
        {
            get
            {
                return color.ToString();
            }
        }
        /// <summary>
        /// the suit of the card
        /// </summary>
        public string Suit
        {
            get
            {
                return suit.ToString();
            }
        }
        public string ImageLink()
        {
            if (hidden == true)
                return "cards/back.png";
            string link = "cards/";
            link = link + value;
            if (suit == Suits.Diamonds)
                link = link + "D";
            if (suit == Suits.Clubs)
                link = link + "C";
            if (suit == Suits.Hearts)
                link = link + "H";
            if (suit == Suits.Spades)
                link = link + "S";
            link = link + ".png";
            return link;
        }

    }
}
