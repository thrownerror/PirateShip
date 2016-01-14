using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Project1v4
{
    /* Name: Robert Bailey
     * Purpose: To create the tanks for a game similar to Atari 2600
     * Each tank has a health value (dies in two hits), an assigned bullet, and an assigned player.
     * Caveats: None known
     * Date: 2/20/2015
     */ 
    public class Tank:MovableGamePiece
    {
        //Attribute declarations
        private Player controller;
        private int health;
        private Bullet bill;
        private Bitmap[] scrapbooking;

        //Property declarations
        public Player Controller
        {
            //No set, so that tanks can't be swapped between players mid game
            get { return controller; }
        }

        public int Health
        {
            //Allows the Game class to change the Health value
            get { return health; }
            set { health = value; }
        }

        public Bullet Bill
        {
            //Each tank only has one bullet, so there isn't a set for new bullets
            get { return bill; }
        }
        public Bitmap[] Scrapbooking
        {
            get { return scrapbooking; }
        }
        
        //Constructor, just sets the player, bullet, health, facing, and position
        public Tank(Player play, Bullet bul, int face, int pX, int pY, int playerNumber):base(face, pX, pY, GameVariables.RedTankDownImage)
        {
            scrapbooking = new Bitmap[4];
            controller = play;
            //If the player is playerOne, creates an array of red images
            if(playerNumber == 1)
            {
                scrapbooking[0] = new Bitmap(GameVariables.RedTankUpImage);
                scrapbooking[1] = new Bitmap(GameVariables.RedTankRightImage);
                scrapbooking[2] = new Bitmap(GameVariables.RedTankDownImage);
                scrapbooking[3] = new Bitmap(GameVariables.RedTankLeftImage);
            }
            //Else,creates an array of green images
            if (playerNumber == 2)
            {
                scrapbooking[0] = new Bitmap(GameVariables.GreenTankUpImage);
                scrapbooking[1] = new Bitmap(GameVariables.GreenTankRightImage);
                scrapbooking[2] = new Bitmap(GameVariables.GreenTankDownImage);
                scrapbooking[3] = new Bitmap(GameVariables.GreenTankLeftImage);
            }
            bill = bul;
            health = 2;
        }

        //When called, reduces the tanks health by one to a minimum of 0.  
        //Also checks if the tank is dead
        public void TakeHit()
        {
            if (health > 0)
            {
                health--;
            }
            if(IsDead())
            {
                //If the tank is dead, the player loses his tank and 
                //the tank's health is reset
                controller.LoseTank();
                health = 2;
            }
        }

        //Checks if the tank is dead
        public Boolean IsDead()
        {
            if(health > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //Moves the tank, with the top left corner being 0,0
        public override void Move()
        {
            //If the tank is moving upwards
            if (base.Facing == 0)
            {
                //And isn't at the top edge of the screen
                if (base.Rec.Y> 0)
                {
                    //It moves up
                    base.rec.Location = new System.Drawing.Point (base.Rec.X, base.Rec.Y - GameVariables.TankSpeed);
                }
            }
            //If the tank is moving right
            else if (base.Facing == 1)
            {
                //And isn't at the right edge of the screen

                //The addition is needed here because Rec.X refers only to the top left corner of the Tank's rectangle, which shouldn't hit the windows right edge
                if ((base.Rec.X + base.Rec.Width) < (GameVariables.InnerWidth - 1))
                {
                    //It moves right
                    base.rec.Location = new System.Drawing.Point(base.Rec.X + GameVariables.TankSpeed, base.Rec.Y);
                }
            }
            //If the tank is moving down
            else if (base.Facing == 2)
            {
                //And isn't at the bottom edge of the screen

                //Addition is needed for the same reasons as above, just with the window's botom edge
                if ((base.Rec.Y + base.Rec.Height) < (GameVariables.InnerHeight - 1))
                {
                    //It moves down
                    base.rec.Location = new System.Drawing.Point(base.Rec.X, base.Rec.Y + GameVariables.TankSpeed);
                }
            }
            //If the moving left
            else if (base.Facing == 3)
            {
                //And isn't at the left edge of the screen
                if (base.Rec.X > 0)
                {
                    //It moves left
                    base.rec.Location = new System.Drawing.Point(base.Rec.X - GameVariables.TankSpeed, base.Rec.Y);
                }
                
            }
        }

        //Places the tank on the screen, with a different character to
        //represent each facing
        public override void Draw()
        {
            base.Draw();
            if(base.Facing == 0)//up
            {
                base.PicBox.Image = scrapbooking[0];
            }
            else if(base.Facing == 1)//right
            {
                base.PicBox.Image = scrapbooking[1];
            }
            else if(base.Facing == 2)//down
            {
                base.PicBox.Image = scrapbooking[2];
            }
            else if(base.Facing == 3)//left
            {
                base.PicBox.Image = scrapbooking[3];
            }
        }

        //The tank fires a bullet in the direction it's facing
        public void Fire()
        {
            //Each tank can only has one active bullet, this checks that
            if(bill.Active == false)
            {
                //Bullet's starting conditions are determined by the firing tank's positon/facing
                bill.Active = true;
                bill.Facing = this.Facing;
                //System.Drawing.Point = new Point(billX, billY);
                int billX = this.Rec.X + (int)(.5 * this.PicBox.Width);
                int billY = this.Rec.Y + (int)(.5 * this.PicBox.Height);
                System.Drawing.Point p = new Point(billX, billY);

                bill.rec.Location = p; 
                //System.
                //bill.rec.Location = System
               // bill.Active = true;
            }

        }

        //Moves the tank one space opposite of its facing
        //Does not change the tank's position.
        //Used as part of the collision detection, forcing the tank back 
        //so it doesn't run into the scenary.
        public void Reverse()
        {
            if (base.Facing == 0)//up
            {
                base.rec.Location = new System.Drawing.Point (base.Rec.X, base.Rec.Y + GameVariables.TankSpeed);  //moves down
            }
            else if (base.Facing == 1)//right
            {
                base.rec.Location = new System.Drawing.Point(base.Rec.X - GameVariables.TankSpeed, base.Rec.Y); 
            }
            else if (base.Facing == 2)//down
            {
                base.rec.Location = new System.Drawing.Point(base.Rec.X, base.Rec.Y - GameVariables.TankSpeed); 
            }
            else if (base.Facing == 3)//left
            {
                base.rec.Location = new System.Drawing.Point(base.Rec.X + GameVariables.TankSpeed, base.Rec.Y);
            }
        }

        //Modified ToString method
        public override string ToString()
        {
            return "Tank owned by player " + Controller.Name + ": Health: " + health + " at " + base.ToString() + " has " + bill;
        }
    }
}
