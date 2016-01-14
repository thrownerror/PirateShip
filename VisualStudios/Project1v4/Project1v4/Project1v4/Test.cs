using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1v3
{
    /* Name: Robert Bailey
     * Purpose: To run a variation on the Atari 2600 game "Tank" using Visual Studios.  
     * The variation is run by calling the Game class (more detail there), 
     * which manages the different pieces of the game.  The game is played between two people
     * on one keyboard, controlling opposing tanks who fire bullets at each other.
     * It ends when one player has been destroyed 5 times.
     * Caveats:  None known
     * Date: 2/14/15
     */ 
    class Test
    {
        static void Main(string[] args)
        {
            //Creates the game, and calls the loop
            Game warGames = new Game();
            warGames.GameLoop();

        }
    }
}
