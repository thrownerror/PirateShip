using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Project1v4
{
    /* Name: Robert Bailey
     * Purpose: Parent class to all pieces used in a game siliar to the Atari 2600's "Tank"
     * Mainly manages the position of the piece
     * Caveat: None Known
     * Date: 2/14/2015
     */ 
    public abstract class GamePiece
    {   
        //Attributes

        public Rectangle rec;
        private PictureBox picBox;
        //Properties

        public Rectangle Rec
        {
            get { return rec; }
            set { rec = value; }
        }
        public PictureBox PicBox
        {
            get { return picBox; }
            set { picBox = value; }
        }
        //Sets the piece's position and the Picture's location
        public GamePiece(int pX, int pY, string imagePath)
        {
            PicBox = new PictureBox();
            PicBox.Location = new System.Drawing.Point(pX, pY);
            PicBox.SizeMode = PictureBoxSizeMode.AutoSize;
            PicBox.BorderStyle = BorderStyle.FixedSingle;
            try
            {
                //attempts to grab an image file from the bin/debug folder
                PicBox.Image = System.Drawing.Image.FromFile(imagePath);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading image file: " + ex.Message);
            }
            rec = new Rectangle(pX, pY, PicBox.Width, PicBox.Height);

        }

        //This method creates the piece on the board.  The Wall, Tank, and Bullet classes will
        //all eventually have a complete method to do this.  
        public virtual void Draw()
        {
            //Console.CursorLeft = posX;
            //Console.CursorTop = posY;

            PicBox.Location = new System.Drawing.Point(rec.X, rec.Y);
        }

        //Collision detection.  Checks if the calling game piece collides with 
        //the parameterized game piece.
        public Boolean IsColliding(GamePiece gp1)
        {
            //If the position X and position Y cordinates are the same, the pieces are colliding
            if(this.rec.IntersectsWith(gp1.Rec))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Modified ToString
        public override string ToString()
        {
            return "PosX: " + rec.X + " PosY: " + rec.Y + " Image file: " + PicBox.Image;
        }
    }
}
