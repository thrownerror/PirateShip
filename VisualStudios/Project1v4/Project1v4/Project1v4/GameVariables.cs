using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1v4
{
    /* Name: Robert Bailey
     * Purpose: This houses static variables of all the important cross-class attributes of the Tank Game.  Because
     * of the Windows Form format, it is difficult to make a new GameForm object - this circumvents that error and allows
     * a central repository.
     * Known errors: None
     * Date: 2/20/15
     */ 
    public static class GameVariables
    {
        public static int GameWidth = 800;
        public static int GameHeight = 600;
        public static int InnerWidth = 800;
        public static int InnerHeight = 600;
        public static int TankSpeed = 5;
        public static int BulletSpeed = 15;
        public static string WallImage = "wall.png";
        public static string BulletImage = "bullet.png";
        public static string RedTankUpImage = "redTankUp.png";
        public static string RedTankDownImage = "redTankDown.png";
        public static string RedTankLeftImage = "redTankLeft.png";
        public static string RedTankRightImage = "redTankRight.png";
        public static string GreenTankUpImage = "greenTankUp.png";
        public static string GreenTankDownImage = "greenTankDown.png";
        public static string GreenTankLeftImage = "greenTankLeft.png";
        public static string GreenTankRightImage = "greenTankRight.png";

    }
}
