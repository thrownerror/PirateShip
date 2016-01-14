using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1v4

{
    /* Name: Robert Bailey
     * Purpose: To represent a bullet fired from a tank in a game that functions similarly
     * to the Atari 2600's "Tank."  Each tank can only have one bullet.  Bullets will fly in a 
     * straight line until they hit a wall or a tank
     * 2/20/15
     */ 
    public class Bullet:MovableGamePiece
    {
        //Attribute 

        //This tells if a bullet is active or not, determining if the owning tank
        //can fire another shot, or if the bullet can hurt the opposing tank
        private Boolean active;

        public Boolean Active
        {
            get { return active; }
            set { active = value;}
        }

        //Basic constructor - passes the position up to MovableGamePiece, makes the bullet active.
        public Bullet(int face, int pX, int pY):base(face, pX, pY, GameVariables.BulletImage)
        {
            active = true;
        }

        //Moves the bullet, happens automatically for all active bullets on a frame update
        public override void Move()  //Top left corner of the screen is 0,0
        {
            if(active) 
            {
                //If the bullet is moving up
                if(base.Facing == 0)
                {
                    //And has not reached the edge of the screen
                    if(base.Rec.Y > 0)
                    {
                        //it moves upwards
                        
                        base.rec.Location = new System.Drawing.Point (base.Rec.X, base.Rec.Y - GameVariables.BulletSpeed);
                    }
                    else
                    {
                        //Otherwise, the bullet would move past the screen's border, and is inactive
                        active = false;
                    }

                }
                    //If the bullet is moving right
                else if(base.Facing == 1)
                {
                    //And has not reached the edge of the screen
                    if (base.Rec.X < GameVariables.InnerWidth- 1)
                    {
                        //it moves right
                        base.rec.Location = new System.Drawing.Point (base.Rec.X + GameVariables.BulletSpeed, base.Rec.Y);
                    }
                    else
                    {
                        //otherwise, it has moved past the screen's right border
                        active = false;
                    }
                }
                    //If the bullet is moving down
                else if(base.Facing == 2)
                {
                    //and has not reached the bottom edge of the screen
                    if (base.Rec.Y < (GameVariables.InnerHeight - 1))
                    {
                        //It continues down
                        base.rec.Location = new System.Drawing.Point(base.Rec.X, base.Rec.Y + GameVariables.BulletSpeed);
                    }
                    else
                    {
                        //Otherwise, it has moved past the bottom border, and is inactive
                        active = false;
                    }
                }
                    //If the bullet is moving left
                else if(base.Facing == 3)
                {
                    //And has not reached the left edge of the screen
                    if (base.Rec.X > 0)
                    {
                        //bullet continues left
                        base.rec.Location = new System.Drawing.Point(base.Rec.X - GameVariables.BulletSpeed, base.Rec.Y);
                    }
                    else
                    {
                        //Otherwise, bullet is inactive
                        active = false;
                    }
                }


            }
        }

        //Creates the bullet on the screen.
        public override void Draw()
        {
            if (active)
            {
                base.Draw();
                base.PicBox.Visible = true;
                //Console.Write("o");
            }
            else
            {
                base.PicBox.Visible = false;
            }
        }

        //Modified toString
        public override string ToString()
        {
            return "Bullet: isActive: " + active + " at " + base.ToString();
        }
    }
}
