using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solitaire
{
    class Row
    {
        public Card[] card;
        public int dimension;
        public Row()
        {
            card = new Card[20];
            dimension = 0;
        }

    }
    class Board
    {
        public int Mutare = 0;
        public bool[] folosita;
        public Row[] foundation;
        public Row[] rand;
        public Card[] Depozit;
        public int currentDepozit = -1, sizeDepozit = 24;
        public Deck deck;
        public Board()
        {
            folosita = new bool[24];
            foundation = new Row[4];
            rand = new Row[7];
            Depozit = new Card[24];
            deck = new Deck();
            for (int i = 0; i < 24; i++)
                folosita[i] = false;
            for (int i = 0; i < 7; i++)
                rand[i] = new Row(); 
            for (int i = 0; i < 4; i++)
                foundation[i] = new Row();
        }
        public bool CheckWin()
        {
            for(int i = 0; i < 4; i++)
            {
                if(foundation[i].dimension != 13)
                    return false;
            }
            return true;

        }
        public int MoveCard(int k)
        {
            /**
             * k = 1 card
             * K = 2,5 Fundation - 2
             * k = 6-12 Row - 6
             */
            if(k == 1)
            {
                Mutare = 1;
                return 0;
            }
            if (Mutare == k)
            {
                return 0;
            }
            if (Mutare == 0)
            {
                Mutare = k;
                return 0;
            }
            if (Mutare == 1) // Muta cartea din pachet
            {
                if (2 <= k && k <= 5) /// pe fundatie
                {
                    k -= 2;
                    if (foundation[k].dimension == 0 &&
                        Depozit[currentDepozit].Value == 1)
                    {
                        foundation[k].card[foundation[k].dimension] = Depozit[currentDepozit];
                        foundation[k].dimension++;
                    }
                    else if (foundation[k].dimension > 0 &&
                        Depozit[currentDepozit].Value == foundation[k].card[foundation[k].dimension - 1].Value + 1 &&
                        Depozit[currentDepozit].Suit == foundation[k].card[foundation[k].dimension - 1].Suit
                        )
                    {
                        foundation[k].card[foundation[k].dimension] = Depozit[currentDepozit];
                        foundation[k].dimension++;
                    }
                    else return 1;
                }
                else /// pe un rand
                {
                    k -= 6;
                    if (rand[k].dimension == 0 && Depozit[currentDepozit].Value == 13)
                    {
                        rand[k].card[rand[k].dimension] = Depozit[currentDepozit];
                        rand[k].dimension++;
                    }
                    else if (rand[k].dimension > 0 &&
                        Depozit[currentDepozit].Value == rand[k].card[rand[k].dimension - 1].Value - 1 &&
                        Depozit[currentDepozit].Color != rand[k].card[rand[k].dimension - 1].Color)
                    {
                        rand[k].card[rand[k].dimension] = Depozit[currentDepozit];
                        rand[k].dimension++;
                    }
                    else return 1;
                }
                folosita[currentDepozit] = true;
                sizeDepozit--;
                while(currentDepozit >= 0 && folosita[currentDepozit])
                    currentDepozit--;


            }
            else if(Mutare >= 2 && Mutare <=5) /// muta cartea de pe fundatie 
            {
                Mutare -= 2;
                if (foundation[Mutare].dimension == 0) return 1;

                if (k == 1)
                {
                    Mutare = k;
                    return 0;
                }
                else if(k >= 2 && k <= 5) /// pe alta fundatie
                {
                    k -= 2;
                    if (foundation[k].dimension == 0
                        && foundation[Mutare].card[foundation[Mutare].dimension - 1].Value == 1)
                    {
                        foundation[k].card[foundation[k].dimension] = foundation[Mutare].card[foundation[Mutare].dimension - 1];
                        foundation[k].dimension++;
                        foundation[Mutare].dimension--;
                    }
                    else return 1;
                }
                else ///pe un rand
                {
                    k -= 6;
                    if (rand[k].dimension == 0 
                        && foundation[Mutare].card[foundation[Mutare].dimension - 1].Value == 13)
                    {
                        rand[k].card[rand[k].dimension] = foundation[Mutare].card[foundation[Mutare].dimension - 1];
                        rand[k].dimension++;
                        foundation[Mutare].dimension--;
                    }
                    else if (rand[k].dimension > 0 &&
                        foundation[Mutare].card[foundation[Mutare].dimension - 1].Value == rand[k].card[rand[k].dimension - 1].Value - 1 &&
                        foundation[Mutare].card[foundation[Mutare].dimension - 1].Color != rand[k].card[rand[k].dimension - 1].Color)
                    {
                        rand[k].card[rand[k].dimension] = foundation[Mutare].card[foundation[Mutare].dimension - 1];
                        rand[k].dimension++;
                        foundation[Mutare].dimension--;
                    }
                    else return 1;

                }

            }
            else /// de pe un rand
            {
                Mutare -= 6;
                if (rand[Mutare].dimension == 0) return 1;

                if (k == 1)
                {
                    Mutare = k;
                    return 0;
                }
                else if( 2 <= k && k <= 5) /// pe fundatie
                {
                    k -= 2;
                    if (foundation[k].dimension == 0
                        && rand[Mutare].card[rand[Mutare].dimension - 1].Value == 1)
                    {
                        foundation[k].card[foundation[k].dimension] = rand[Mutare].card[rand[Mutare].dimension - 1];
                        foundation[k].dimension++;
                        rand[Mutare].dimension--;
                    }
                    else if(foundation[k].dimension > 0
                        && rand[Mutare].card[rand[Mutare].dimension - 1].Value == foundation[k].card[foundation[k].dimension - 1].Value + 1
                        && rand[Mutare].card[rand[Mutare].dimension - 1].Suit == foundation[k].card[foundation[k].dimension - 1].Suit)
                    {
                        foundation[k].card[foundation[k].dimension] = rand[Mutare].card[rand[Mutare].dimension - 1];
                        foundation[k].dimension++;
                        rand[Mutare].dimension--;
                    }
                    else return 1;
                }
                else /// pe alt rand
                {
                    k -= 6;
                    int i, n;
                    n = rand[Mutare].dimension - 1;
                    if (rand[k].dimension == 0)
                    {
                        for (i = n; i >= 0; i--)
                        {
                            if (rand[Mutare].card[i].hidden == false &&
                                rand[Mutare].card[i].Value == 13)
                                break;
                        }
                        if (i != -1)
                            for (; i <= n; i++)
                            {
                                rand[k].card[rand[k].dimension] = rand[Mutare].card[i];
                                rand[k].dimension++;
                                rand[Mutare].dimension--;
                            }
                        else return 1;
                    }
                    else
                    {

                       for(i = n; i >= 0; i--)
                        {
                            if(rand[Mutare].card[i].hidden == false &&
                                rand[Mutare].card[i].Value == rand[k].card[rand[k].dimension - 1].Value - 1
                                && rand[Mutare].card[i].Color != rand[k].card[rand[k].dimension - 1].Color)
                                break;
                        }
                        if (i != -1)
                            for (; i <= n; i++)
                            {
                                rand[k].card[rand[k].dimension] = rand[Mutare].card[i];
                                rand[k].dimension++;
                                rand[Mutare].dimension--;
                            }
                        else return 1;
                    }
                }



            }
            for (int i = 0; i < 7; i++)
                if(rand[i].dimension > 0)
                    rand[i].card[rand[i].dimension - 1].Show();
            Mutare = 0;
            return 2;

        }

        public void Generate()
        {
            deck.Shuffle();
            rand[0].card[0] = deck.DrawCard();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    rand[i].card[j] = deck.DrawCard();
                    rand[i].dimension++;
                    rand[i].card[j].Hide();
                }

                rand[i].card[i] = deck.DrawCard();
                rand[i].dimension++;
                //System.Windows.Forms.MessageBox.Show(" ");
                rand[i].card[i].Show();
            }
            for (int i = 0; i < 24; i++)
                Depozit[i] = deck.DrawCard();
        }
        

    }
}
