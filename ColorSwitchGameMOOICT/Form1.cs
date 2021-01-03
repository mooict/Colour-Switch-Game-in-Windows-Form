using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorSwitchGameMOOICT
{
    public partial class Form1 : Form
    {

        List<Color> colors; // create a list of colors for the game

        Random rnd = new Random(); // new instance of the random class called rnd

        Random blockPosition = new Random(); // new instance of the random class called block position

        int blockColor = 0; // integer that will determine the block colors

        int i; // integer that will change the players color

        int speed = 5; // speed of the blocks in the beginning of the game

        int score = 0; // default score of the game

        bool gameOver = false; // the default game over Boolean

        int location;

        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainGameTimer(object sender, EventArgs e)
        {
           txtScore.Text = "Score: " + score;

            foreach (Control x in gameDisplay.Controls)
            {
                if (x is PictureBox)
                {

                    if ((string)x.Tag == "block")
                    {
                        x.Top += speed;


                        if (x.Top + x.Height > this.ClientSize.Height)
                        {

                            location = blockPosition.Next(400, 600) + (3 * blockPosition.Next(150, 250));

                            x.Top = location * -1;

                            blockColor = rnd.Next(colors.Count);

                            x.BackColor = colors[blockColor];

                            score += 1;
                        }

                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            // if the player and block 1 DON'T have the same background color
                            if (player.BackColor != x.BackColor)
                            {
                                // we will add the current score to the list box with the time which they were played
                                ScoreList.Items.Insert(0, "Scored: " + score + " @" + string.Format(" {0:HH:mm tt}", DateTime.Now));
                                // game over
                                gameTimer.Stop(); // stop the timer

                                gameOver = true; // set game over to true now that the player has lost
                            }
                        }
                    }
                }
            }

            if (score > 10)
            {
                speed = 12;
            }

        }

        private void KeyIsPressed(object sender, KeyPressEventArgs e)
        {
            // if the player presses the space key we do the following
            if (e.KeyChar == (char)Keys.Space && gameOver == false)
            {
                i++; // increase the i integer by 1

                // if the i integer is greater than the total colours we have in the list
                if (i > colors.Count - 1)
                {
                    // reset i back to 0
                    i = 0;
                }

                player.BackColor = colors[i]; // apply the color to players background

            }

            if (e.KeyChar == (char)Keys.R || e.KeyChar == char.ToLower((char)Keys.R) && gameOver == true)
            {
                // invoke the reset game function
                RestartGame();
            }

        }

        private void RestartGame()
        {
            block1.Top = -200; // set the block 1 to top of the screen at -80 pixels
            block2.Top = -750; // set the block 2 to top of the screen at -300 pixels
            gameOver = false;

            colors = new List<Color> { Color.Red, Color.White, Color.Purple, Color.Yellow };

            i = 0; // i is set to 0 as default
            gameTimer.Start(); // start the game timer
            speed = 8; // set the default speed to 5 for the blocks
            score = 0; // default score is 0

            txtScore.Text = "Score: " + score;


        }
    }
}
