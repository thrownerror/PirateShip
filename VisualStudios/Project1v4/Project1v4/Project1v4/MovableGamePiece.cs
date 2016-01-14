using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1v4
{
    /* Name: Robert Bailey
     * Purpose: This class manages all movable game pieces in a game that works similarly to
     * the Atari 2600's "Tank."  It's a parent class to Tanks and Bullets. It manages the facing 
     * and position of the movable pieces.
     * Caveats: None known
     * Date: 2/20/2015
     */ 
    public abstract class MovableGamePiece:GamePiece
    {
        //Attribute 

        //The facing of a game piece, as determined by a numberic value
        private int facing;
            //UP: 0
            //RIGHT: 1
            //DOWN: 2
            //LEFT: 3

        //Property
        public int Facing
        {
            get { return facing; }
            set { facing = value; }
        }

        //Takes a facing, position X and position Y value.  Assigns facing, and then passes
        //pX and pY to the GamePiece class
        public MovableGamePiece(int fac, int pX, int pY, string imagePath):base(pX, pY, imagePath)
        {
            facing = fac;
        }

        //The move method, to be filled in the Tank and Bullet classes
        public abstract void Move();

        //Modified toString.  Uses an if statement to determine which way the vehicle is facing and 
        //display it as a readable string.
        public override string ToString()
        {
            //An impossible value as a placeholder for compilation
            string face = "diagonally left";

            if(facing == 0)
            {
                face = "UP";
            }
            if(facing == 1)
            {
                face = "RIGHT";
            }
            if(facing == 2)
            {
                face = "DOWN";
            }
            if (facing == 3)
            {
                face = "LEFT";
            }

            return base.ToString() + " Facing: " + face;
        }
    }
}
