using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project1v4
{
    /*
     * Name: Robert Bailey
     * Description: The "options menu" for the game - allows changing to the player name and 
     * lets the player type in a text file for a map.
     * Known errors: None
     * Date:2/20/15
     */ 
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }
        
        //Clears all text on the form
        private void clearButton_Click(object sender, EventArgs e)
        {
            player1Name.Text = "";
            player2Name.Text = "";
            mapPath.Text = "";
        }
        
        //Checks the input, and if it is all valid, runs the game
        private void button2_Click(object sender, EventArgs e)
        {
            Boolean problem = false;
            //Null inputs are invalid - won't open the game until the names are non-null
            if(player1Name.Text == ""|| player2Name.Text == "")
            {
                problem = true;
                MessageBox.Show("Invalid player names.  Make sure both players have a non-empty name.");
            }
            try
            {
                //Tests if the map is actually there as it loads
                StreamReader reader = new StreamReader(mapPath.Text);
               
            }
            catch(Exception exp)
            {
                problem = true;
                MessageBox.Show("Map file not found.  Please use a valid file path.");
            }

            //If no problem is found, hgides this window and starts the game
            if(!problem)
            {
                GameForm game = new GameForm(this, player1Name.Text, player2Name.Text, mapPath.Text);
                game.Show();
                this.Hide();
            }

        }

        private void player1Name_TextChanged(object sender, EventArgs e)
        {
            //double click accident
        }
    }
}
