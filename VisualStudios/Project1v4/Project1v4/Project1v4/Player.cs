using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1v4
{
    /* Name: Robert Bailey
     * Purpose: To create a player for a tank game, which is similar to "Tank" on the Atari 2600
     * There will be two players, who each control a tank.  The first player to lose five tanks loses.
     * Caveats: None known
     * Date: 2/14/2015
     */ 
    public class Player
    {
        //Attribues
        private string name;
        private int lostTanks; //keeps track of lost tanks
        private int playerNum; //Gives the player number for another form of identification besides name

        private Boolean loser; //Used by Game class to check if the player has lost

        //Properties
        public string Name
        {
            get { return name; }
            set { name = value;}
        }

        public int LostTanks
        {
            get { return lostTanks; }
            set { lostTanks = value; }
        }

        public int PlayerNum
        {
            get { return playerNum; }
            set { playerNum = value; }
        }

        public Boolean Loser
        {
            get { return loser; }
        }

        //Basic constructor, sets name and number
        public Player(string nm, int playNum)
        {
            loser = false;
            lostTanks = 0;
            name = nm;
            playerNum = playNum;
        }

        //Increases the player's lost tank counter
        //If it reaches 5, the player has lost the game and calls the LoseGame method
        public void LoseTank()
        {
            lostTanks++;
            if(lostTanks >= 5)
            {
                //If the player has lost all of his/her 5 tanks,
                //then the loseGame method is called
                //First step in ending the game
                LoseGame();
            }
        }

        //This method sets the loser property to true.
        //Second step in the process of ending the game.
        public void LoseGame()
        {
            loser = true;
        }

        //Overriden ToString method
        public override string ToString()
        {
            return "Name: " + name + " Lost tanks: " + lostTanks + " Player Number: " + playerNum;
        }
    }
}
