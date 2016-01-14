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
/* Name: Robert Bailey
* Purpose: This class manages the various pieces of a game deisgned to emulate
* the Atari 2600's "Tank" video game.  These pieces include the players, tanks, bullets
* and walls (terrain).  This class has the game loop, input gathering and processing, 
* and the collected draw methods.  This form also allows for graphics and images for the tanks
 * and walls in the game
* Caveats: None Known
* Date: 2/20/15
*/
    public partial class GameForm : Form
    {
        private InfoForm info;
        private string player1Name;
        private string player2Name;
        private string mapText;
                    //Attribute declaration, spaced out by category
            private Player playerOne;
            private Player playerTwo;

            private Tank tankOne;
            private Tank tankTwo;

            private Bullet bullOne;
            private Bullet bullTwo;

            private List<Wall> walle;         //List to contain every wall as one object

            private Boolean gameOver;

            //Properties declarations for the above attributes
            public Player PlayerOne
            {
                get { return playerOne; }
                set { playerOne = value; }
            }

            public Player PlayerTwo
            {
                get { return playerTwo; }
                set { playerTwo = value; }
            }

            public Tank TankOne
            {
                get { return tankOne; }
                set { tankOne = value; }
            }
            public Tank TankTwo
            {
                get { return tankTwo; }
                set { tankTwo = value; }
            }

            public Bullet BullOne
            {
                get { return bullOne; }
                set { bullOne = value; }
            }
            public Bullet BullTwo
            {
                get { return bullTwo; }
                set { bullTwo = value; }
            }

            public List<Wall> Walle
            {
                get { return walle; }
                set { walle = value; }
            }

            public Boolean GameOver
            {
                get { return gameOver; }
            }



        public string Player1Name
        {
            get { return player1Name;}
        }

        public string Player2Name
        {
            get { return player2Name;}
        }


        //Basic constructor, simply places the walls, tanks and bullets on the "board"
        public GameForm(InfoForm i, string p1, string p2, string map)
        {
            info = i;
            player1Name = p1;
            player2Name = p2;
            mapText = map;
            InitializeComponent();

                //Any posX and posY values set here are just placeholder values before the map.txt is read
                //Walls are in the walle list because there can be any number of walls 
                walle = new List<Wall>();
                playerOne = new Player(player1Name, 1);
                playerTwo = new Player(player2Name, 2);

                //sets bullets, but starts them inactive to avoid instant death
                bullOne = new Bullet(1, 5, 5);
                bullOne.Active = false;
                bullTwo = new Bullet(1, 10, 10);
                bullTwo.Active = false;

                //sets tanks, with an assigned player and bullet
                //The placement is default if the map.txt file has no values
                tankOne = new Tank(playerOne, bullOne, 1, 10, 20, 1);
                tankTwo = new Tank(playerTwo, bullTwo, 3, 20, 20, 2);

                //Reads the map file in bin/debug
                ReadMap(mapText);  //this can be changed, and should be the map file.
                DrawGame();
                //The under-the-hood properties
                gameOver = false;
                //Console.CursorVisible = false;  //avoids a blinking cursor
                //sets window properties.  Uses default BufferWidth to avoid an error
                this.Width = GameVariables.GameWidth;
                this.Height = GameVariables.GameHeight;
                GameVariables.InnerHeight = this.ClientSize.Height;
                GameVariables.InnerWidth = this.ClientSize.Width;

                //Adds each piece of the game to the form's Controls directory
                this.Controls.Add(tankOne.PicBox);
                this.Controls.Add(tankTwo.PicBox);
                this.Controls.Add(bullOne.PicBox);
                this.Controls.Add(bullTwo.PicBox);
                foreach(Wall w in walle)
                {
                    this.Controls.Add(w.PicBox);
                }
                gameTimer.Start();
                //GameLoop();
            }
            // Based off of the map.txt file, the first two lines are read as tanks.
            // This method goes through the line, and process it as tank parameters


        private void GameForm_Load(object sender, EventArgs e)
        {
            //accidental double click
        }

            private void SetupTank(string info, Tank tnk)
            {
                //Splits the line into three paraemeters - x position, y position, facnig
                string[] splitter = info.Split(',');

                //Double check that there are enough parameters
                if (splitter.Length == 3)
                {
                    int pX;
                    int pY;
                    int face;
                    //attempts to parse the parameters
                    Boolean testF = int.TryParse(splitter[0], out pX);
                    Boolean testX = int.TryParse(splitter[1], out pY);
                    Boolean testY = int.TryParse(splitter[2], out face);
                    try
                    {
                        //assigns the parameters

                        tnk.rec.Location = new System.Drawing.Point (pX, pY);
                        tnk.Facing = face;
                    }
                    //Error catching
                    catch (System.ArgumentOutOfRangeException argRange)
                    {
                        MessageBox.Show("Please confine map.txt values to the window size (80 wide, 30 tall): " + argRange.Message);
                    }
                    catch (IOException ioe)
                    {
                        MessageBox.Show("IOException, please check map.txt formatting: " + ioe.Message);
                    }
                    catch (IndexOutOfRangeException indRange)
                    {
                        MessageBox.Show("Index out of range exception, double check map.txt formatting (lines 1 and 2 have 3 values, all other lines have 2 values: " + indRange.Message);
                    }
                    //For everything else, there's mastercard
                    catch (Exception generic)
                    {
                        MessageBox.Show("Exception: " + generic.Message);
                    }
                }
            }
            //Creates a wall and addes it to the Walle List.
            //Info comes from lines 3+ of map.txt, and follow the format
            //positionX, positionY
            private void CreateWall(string info)
            {
                //Splits the line
                string[] splitter = info.Split(',');

                //double check that there are enough parameters to process
                if (splitter.Length >= 2)
                {
                    try
                    {
                        int pX;
                        int pY;
                        //attempts to parse the parameters
                        Boolean testX = int.TryParse(splitter[0], out pX);
                        Boolean testY = int.TryParse(splitter[1], out pY);

                        //assigns the parameters, puts the wall in the list
                        Wall greatWallOfGDAPS = new Wall(pX, pY);
                        Walle.Add(greatWallOfGDAPS);
                    }
                    //error catching - all errors cause the screen to clear and the error message to print

                        //Error having read the file
                    catch (IOException ioe)
                    {
                        MessageBox.Show("IOE error: " + ioe.Message);
                    }
                    //Error with the cursor placement due to given positions from the file
                    catch (System.ArgumentOutOfRangeException argRange)
                    {
                        MessageBox.Show("Please confine map.txt values to the window size (80 wide, 30 tall): " + argRange.Message);
                    }
                    //The array is too large or too small - erorr from map.txt formating
                    catch (IndexOutOfRangeException indRange)
                    {
                        MessageBox.Show("Index out of range exception, double check map.txt formatting (lines 1 and 2 have 3 values, all other lines have 2 values: " + indRange.Message);
                    }
                    //For everything else, there's generic catches
                    catch (Exception generic)
                    {
                       MessageBox.Show("Exception: " + generic.Message);
                    }
                }
            }

            //Reads the map from the map.txt file, the path (standard: map.txt) is located in the constructor.
            private void ReadMap(string path)
            {
                //The StreamReader reads the file
                StreamReader input = null;

                //Tries to read the file
                try
                {
                    input = new StreamReader(path);

                    string line = null;

                    //This first line is assumed to be a tank based off of formating
                    line = input.ReadLine();
                    if (line != null)
                    {
                        SetupTank(line, tankOne);//creates the tank off of the read data
                    }

                    //This second line is assumed to be the second tank
                    line = input.ReadLine();

                    if (line != null)
                    {
                        SetupTank(line, tankTwo);
                    }

                    //All other lines are assumed to be walls
                    while ((line = input.ReadLine()) != null)
                    {
                        CreateWall(line);
                    }
                    //closes the file
                    input.Close();

                }
                //error catching  - all errors cause the screen to clear and the error message to print

                    //cursor is in the wrong place due to the drawing 
                catch (System.ArgumentOutOfRangeException argRange)
                {
                    MessageBox.Show("Please confine map.txt values to the window size (80 wide, 30 tall): " + argRange.Message);
                }
                //Error with the file
                catch (IOException ioe)
                {
                    EndGame();
                    MessageBox.Show("Error reading from setup file.  IOException: " + ioe.Message);
                }
                //Generic error 
                catch (Exception generic)
                {
                    MessageBox.Show("Exception: " + generic.Message);
                }
            }

            //Thism method is called when the game loop ends, clears the board
            //And prints out the victor
            public void EndGame()
            {
                gameTimer.Stop();
                //controlYourWindows = 0;
                //Console.Clear();
                //Console.WriteLine("Game Over ");

                //Checks to see which player lost the game, prints out a message
                //accordingly
                //{
                    if (playerOne.Loser)
                    {
                        MessageBox.Show("Game Over!\nPlayer Two wins!");

                    }
                    else if (playerTwo.Loser)
                    {
                        MessageBox.Show("Game Over!\nPlayer One wins!");

                    }
                    else
                    {
                        MessageBox.Show("Game ended early - someone hit q.");

                    }

                    Close();
                    //controlYourWindows++;
             }
               // return;
            

            //Clears the board, and draws the new position.  
            //Essentially, the frame updater
            public void DrawGame()
            {
                //Console.Clear();
                bullOne.Draw();
                bullTwo.Draw();
                tankOne.Draw();
                tankTwo.Draw();
                foreach (Wall border in Walle)
                {
                    border.Draw();
                }
            }



            public void DetectCollisions()
            {
                //bullet collision detection
                //Checks if the bullets have collided with the tanks
                //If so, reduces the tank's health and the bullet becomes inactive
                if (bullOne.Active)
                {
                    if (bullOne.IsColliding(tankTwo))
                    {
                        tankTwo.TakeHit();
                        bullOne.Active = false;
                    }
                }
                if (bullTwo.Active)
                {
                    if (bullTwo.IsColliding(tankOne))
                    {
                        tankOne.TakeHit();
                        bullTwo.Active = false;
                    }
                }

                //Collisions with the walls
                //Goes through the Wall list (Walle), and checks if tanks or bullets
                //have collided with the walls
                //Tanks reverse if they have, bullets become inactive
                foreach (Wall wales in Walle)
                {
                    if (tankOne.IsColliding(wales))
                    {
                        tankOne.Reverse();
                    }
                    if (tankTwo.IsColliding(wales))
                    {
                        tankTwo.Reverse();
                    }
                    if (bullOne.IsColliding(wales))
                    {
                        bullOne.Active = false;
                    }
                    if (bullTwo.IsColliding(wales))
                    {
                        bullTwo.Active = false;
                    }
                }
            }

            //The main game loop.  Until the gameOver conditions are met,
            //keeps refreshing the screen
            public void GameLoop()
            {
                try
                {
                        bullOne.Move(); //moves the bullets because they are automated
                        bullTwo.Move();
                        DetectCollisions();  //does what it says
                        DrawGame(); //draws every updated condition
                        //Updates the information on the window's top bar
                        Text = player1Name + "'s Remaining tanks: " + (5 - playerOne.LostTanks) + " Current Health: " + tankOne.Health + "  " + player2Name + "'s Remaining tanks: " + (5 - playerTwo.LostTanks) + " Current Health: " + tankTwo.Health;
                        //Checks if either player has lost
                        if (playerOne.Loser)
                        {
                            EndGame();

                        }
                        if (playerTwo.Loser)
                        {
                            EndGame();
                        }

                }
                //This has nothing to do with the actual file processing, so the IOException and IndexOutOfRange
                //don't have specific errors.  However, the cursor could have ended out side of the window, warranting
                //an ArgumentOurOfRangeException
                catch (System.ArgumentOutOfRangeException argRange)
                {

                    MessageBox.Show("Please confine map.txt values to the window size (80 wide, 30 tall): " + argRange.Message);
                }
                catch (Exception generic)
                {
                    MessageBox.Show("Exception: " + generic.Message);
                }
            }
            
            //Reopens the info window if the game window closes
            private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
            {
                info.Show();
            }

            private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
            {
                //Currently, only uses lowercase letters
                switch (e.KeyChar)
                {
                    //Quits the game
                    case 'q':
                        gameOver = true;
                        EndGame();
                        break;


                    //Tank 1 Controls

                    //Movement - follows standard WASD controls
                    case 'w':
                        tankOne.Facing = 0;
                        tankOne.Move();
                        break;
                    case 'a':
                        tankOne.Facing = 3;
                        tankOne.Move();
                        break;
                    case 'd':
                        tankOne.Facing = 1;
                        tankOne.Move();
                        break;
                    case 's':
                        tankOne.Facing = 2;
                        tankOne.Move();
                        break;
                    //Fires the bullet
                    case 'f':
                        tankOne.Fire();
                        break;


                    //Tank 2 Controls

                    //Movement - uses IJKL controls
                    case 'i':
                        tankTwo.Facing = 0;
                        tankTwo.Move();
                        break;
                    case 'j':
                        tankTwo.Facing = 3;
                        tankTwo.Move();
                        break;
                    case 'l':
                        tankTwo.Facing = 1;
                        tankTwo.Move();
                        break;
                    case 'k':
                        tankTwo.Facing = 2;
                        tankTwo.Move();
                        break;

                    //Fires the bullet
                    case 'h':
                        tankTwo.Fire();
                        break;

                    default:
                        break;
                }
                DetectCollisions();
            }
            
            //For every tick of the gameTimer, the gameLoop is called
            private void gameTimer_Tick(object sender, EventArgs e)
            {
                GameLoop();
            }
        }
    }

    

