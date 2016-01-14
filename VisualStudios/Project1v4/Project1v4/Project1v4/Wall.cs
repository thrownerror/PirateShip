using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1v4
{
    /* Name: Robert Bailey
     * Purpose: Creats a wall (terrain) for a tank game siliar to "Tank" on the Atari 2600
     * Bullets and tanks will not be allowed to move through walls
     * Caveats: None known
     * Date: 2/20/2015
     */ 
    public class Wall:GamePiece
    {
        //No extra properites or attributes.  The ones from the parent class GamePiece are all the wall needs.

        //Basic constructor - just sets the position of the wall
        public Wall(int pX, int pY):base(pX, pY, GameVariables.WallImage)
        {
            //no extra attributes
        }

        


        //Modified ToString method
        public override string ToString()
        {
            return "There is a wall at: " + base.ToString();
        }
    }
}
