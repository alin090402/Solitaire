using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace solitaire
{
    public partial class Form2 : Form
    {
        Board board;
        int NrMutari = 0;
        PictureBox[] rows = new PictureBox[7];
        PictureBox[] foundation = new PictureBox[4];
        PictureBox[] cards = new PictureBox[52];
        int nrCards = 0;
        void DisplayBoard()
        {
            //MessageBox.Show("" + board.rand[0].dimension);
            LabelMutari.Text = "Numarul de mutari: " + NrMutari;
            LabelScor.Text = "Carti plasate pe fundatie: " + (board.foundation[0].dimension + board.foundation[1].dimension + board.foundation[2].dimension + board.foundation[3].dimension);
            if (board.currentDepozit != board.sizeDepozit - 1)
            {
                Deposit.ImageLocation = "cards/back.png";
            }
            else Deposit.ImageLocation = "";
            if(board.currentDepozit != -1)
            {
                Card.ImageLocation = board.Depozit[board.currentDepozit].ImageLink();
            }
            else
            {
                Card.ImageLocation = "";
            }
            for(int i = 0; i < nrCards; i++)
            {
                this.Controls.Remove(cards[i]);
                cards[i].Dispose();
            }
            nrCards = 0;
            for(int i = 0; i < 7; i++)
            {
                if (board.rand[i].dimension > 0)
                {
                    rows[i].ImageLocation = board.rand[i].card[0].ImageLink();

                    for (int j = 1; j < board.rand[i].dimension; j++)
                    {
                        //MessageBox.Show("hey");
                        cards[nrCards] = new PictureBox();
                        cards[nrCards].SizeMode = PictureBoxSizeMode.StretchImage;
                        cards[nrCards].Anchor = rows[i].Anchor;
                        cards[nrCards].Visible = true;
                        cards[nrCards].Size = rows[i].Size;
                        cards[nrCards].Location = new Point(rows[i].Location.X, rows[i].Location.Y + 30 * j);
                        cards[nrCards].ImageLocation = board.rand[i].card[j].ImageLink();
                        cards[nrCards].Tag = i + 6;
                        cards[nrCards].Click += Mutare;
                        
                        nrCards++;
                    }


                    //MessageBox.Show("cards / 1C.png");
                }
                else rows[i].ImageLocation = "";
            }
            for (int i = 0; i < nrCards; i++)
            {
                this.Controls.Add(cards[i]);
                cards[i].BringToFront();
            }
            for (int i = 0; i < 4; i++)
                if(board.foundation[i].dimension > 0)
                {
                    foundation[i].ImageLocation = board.foundation[i].card[board.foundation[i].dimension - 1].ImageLink();
                }
                else
                {
                    foundation[i].ImageLocation = "";
                }
        }
        void Mutare(object sender, EventArgs e)
        {
            int k = (int)(sender as PictureBox).Tag;
            int errorcode = board.MoveCard(k);
            if (errorcode == 1)
            {
                MessageBox.Show("Mutare imposibila");
                board.Mutare = 0;
            }
            else if(errorcode == 2)
                NrMutari++;
            //MessageBox.Show(board.MoveCard(k).ToString() + "      " + k.ToString());
            DisplayBoard();
            if(board.CheckWin())
            {
                MessageBox.Show("Well done. You won!!");
            }

        }


        
        public Form2()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            
            InitializeComponent();
            rows[0] = Row0;
            rows[1] = Row1;
            rows[2] = Row2;
            rows[3] = Row3;
            rows[4] = Row4;
            rows[5] = Row5;
            rows[6] = Row6;
            foundation[0] = Foundation0;
            foundation[1] = Foundation1;
            foundation[2] = Foundation2;
            foundation[3] = Foundation3;
            for(int i = 0; i < 4; i++)
            {
                foundation[i].Tag = i + 2;
                foundation[i].Click += Mutare;
            }
            for(int i = 0; i < 7; i++)
            {
                rows[i].Tag = i + 6;
                rows[i].Click += Mutare;
            }
            Card.Tag = 1;
            Card.Click += Mutare;
            board = new Board();
            board.Generate();
            DisplayBoard();
            
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Row4_Click(object sender, EventArgs e)
        {

        }

        private void Deposit_Click(object sender, EventArgs e)
        {
            do
                board.currentDepozit++;
            while (board.currentDepozit < 24
                && board.folosita[board.currentDepozit]);
            if (board.currentDepozit >= 24) board.currentDepozit = -1;
            DisplayBoard();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 formmeniu = new Form1();
            //Form1 formmenu = new Form1();
            formmeniu.Show();
            this.Hide();
        }
    }
}
