using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project1v3
{
    /* Name: Robert Bailey
     * Purpose: This class manages the various pieces of a game deisgned to emulate
     * the Atari 2600's "Tank" video game.  These pieces include the players, tanks, bullets
     * and walls (terrain).  This class has the game loop, input gathering and processing, 
     * and the collected draw methods.
     * Caveats: None Known
     * Date: 2/13/15
     */ 
    class Game
    {
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

        //Basic constructor, simply places the walls, tanks and bullets on the "board"
        public Game()
        {
            //Any posX and posY values set here are just placeholder values before the map.txt is read
            //Walls are in the walle list because there can be any number of walls 
            walle = new List<Wall>();
            playerOne = new Player("One", 1);
            playerTwo = new Player("Two", 2);

            //sets bullets, but starts them inactive to avoid instant death
            bullOne = new Bullet(1, 5, 5);
            bullOne.Active = false;
            bullTwo = new Bullet(1, 10, 10);
            bullTwo.Active = false;

            //sets tanks, with an assigned player and bullet
            //The placement is default if the map.txt file has no values
            tankOne = new Tank(playerOne, bullOne, 1, 10, 20);  
            tankTwo = new Tank(playerTwo, bullTwo, 3, 20, 20);

            //Reads the map file in bin/debug
            ReadMap("map.txt");  //this can be changed, and should be the map file.
            DrawGame();
            //The under-the-hood properties
            gameOver = false;
            Console.CursorVisible = false;  //avoids a blinking cursor
            //sets window properties.  Uses default BufferWidth to avoid an error
            Console.BufferHeight = 30;
            Console.WindowHeight = 30;
        
        }
          // Based off of the map.txt file, the first two lines are read as tanks.
         // This method goes through the line, and process it as tank parameters

        private void SetupTank(string info, Tank tnk)
        {
            //Splits the line into three paraemeters - x position, y position, facnig
            string[] splitter = info.Split(',');

            //Double check that there are enough parameters
            if(splitter.Length == 3)
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
                    tnk.PosX = pX;
                    tnk.PosY = pY;
                    tnk.Facing = face;
                }
                    //Error catching
                catch (System.ArgumentOutOfRangeException argRange)
                {
                    Console.Clear();
                    Console.WriteLine("Please confine map.txt values to the window size (80 wide, 30 tall): " + argRange.Message);
                }
                catch (IOException ioe)
                {
                    Console.Clear();
                    Console.WriteLine("IOException, please check map.txt formatting: " + ioe.Message);
                }
                catch(IndexOutOfRangeException indRange)
                {
                    Console.Clear();
                    Console.WriteLine("Index out of range exception, double check map.txt formatting (lines 1 and 2 have 3 values, all other lines have 2 values: " + indRange.Message);
                }
                    //For everything else, there's mastercard
                catch(Exception generic)
                {
                    Console.Clear();
                    Console.WriteLine("Exception: " + generic.Message);
                }
            }
        }
        //Creates a wall and addes it to the Walle List.
        //Info comes from lines 3+ of map.txt, and follow the format
        //positionX, positionY
        private void CreateWall(string info)
        {
            //Splits the line
            string[]splitter = info.Split(',');

            //double check that there are enough parameters to process
            if(splitter.Length >= 2)
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
                catch(IOException ioe)
                {
                    Console.Clear();
                    Console.WriteLine("IOE error: " + ioe.Message);
                }
                    //Error with the cursor placement due to given positions from the file
                catch(System.ArgumentOutOfRangeException argRange)
                {
                    Console.Clear();
                    Console.WriteLine("Please confine map.txt values to the window size (80 wide, 30 tall): " + argRange.Message);
                }
                    //The array is too large or too small - erorr from map.txt formating
                catch (IndexOutOfRangeException indRange)
                {
                    Console.Clear();
                    Console.WriteLine("Index out of range exception, double check map.txt formatting (lines 1 and 2 have 3 values, all other lines have 2 values: " + indRange.Message);
                }
                    //For everything else, there's generic catches
                catch (Exception generic)
                {
                    Console.Clear();
                    Console.WriteLine("Exception: " + generic.Message);
                }
            }
        }

        //Reads the map from the map.txt file, the path (map.txt) is located in the constructor.
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
                if(line != null)
                {
                    SetupTank(line, tankOne);//creates the tank off of the read data
                }

                //This second line is assumed to be the second tank
                line = input.ReadLine();
                
                if(line != null)
                {
                    SetupTank(line, tankTwo);
                }

                //All other lines are assumed to be walls
                while((line = input.ReadLine()) != null)
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
                Console.Clear();
                Console.WriteLine("Please confine map.txt values to the window size (80 wide, 30 tall): " + argRange.Message);
            }
                //Error with the file
            catch(IOException ioe)
            {
                EndGame();
                Console.WriteLine("Error reading from setup file.  IOException: " + ioe.Message);
            }
                //Generic error 
            catch (Exception generic)
            {
                Console.Clear();
                Console.WriteLine("Exception: " + generic.Message);
            }
        }

        //Thism method is called when the game loop ends, clears the board
        //And prints out the victor
        public void EndGame()
        {
            Console.Clear();
            Console.WriteLine("Game Over ");

            //Checks to see which player lost the game, prints out a message
            //accordingly
            if(playerOne.Loser)
            {
                Console.WriteLine("Player Two wins!");
            }
            if(playerTwo.Loser)
            {
                Console.WriteLine("Player One wins!");
            }
        }

        //Clears the board, and draws the new position.  
        //Essentially, the frame updater
        public void DrawGame()
        {
            Console.Clear();
            bullOne.Draw();
            bullTwo.Draw();
            tankOne.Draw();
            tankTwo.Draw();
            foreach (Wall border in Walle)
            {
                border.Draw();
            }
        }

        //Code taken from Milestone3.docx
        /// <summary>
        /// Get keyboard input and return it
        /// </summary>
        public char[] GetInput()
        {
            // A list of characters
            List<char> input = new List<char>();

            // Loop while keys are available or we hit 10 keys
            for (int i = 0; Console.KeyAvailable && i < 10; i++)
            {
                // Read a key (preventing it from being printed) 
                // and put it in the key list (if it's not in there yet)
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (!input.Contains(info.KeyChar))
                    input.Add(info.KeyChar);
            }

            // Use up any remaining key presses
            while (Console.KeyAvailable)
            {
                // Read a single key
                Console.ReadKey(true);
            }

            // Convert the list to an array and return
            return input.ToArray();
        }

        //Process the character array from the supplied GetInput
        public void ProcessInput()
        {
            char[] input = GetInput();

            //Sets every character in input to a lowercase letter.  This allows
            //one switch case for each input, as opposed to two.  Also, if someone
            //hits caps lock, the game doesn't seem to freeze.
            for(int i = 0; i < input.Length; i++)
            {
                input[i] = char.ToLower(input[i]);
            }
            
            //Uses a switch statement to process all of the characters from input
            foreach(char c in input)
            {
                //Currently, only uses lowercase letters
                switch(c)
                {
                    //Quits the game
                    case 'q':
                        gameOver = true;
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
            }
        }

        public void DetectCollisions()
        {
            //bullet collision detection
            //Checks if the bullets have collided with the tanks
            //If so, reduces the tank's health and the bullet becomes inactive
            if(bullOne.Active)
            {
                if(bullOne.IsColliding(tankTwo))
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
            foreach(Wall wales in Walle)
            {
                if(tankOne.IsColliding(wales))
                {
                    tankOne.Reverse();
                }
                if(tankTwo.IsColliding(wales))
                {
                    tankTwo.Reverse();
                }
                if(bullOne.IsColliding(wales))
                {
                    bullOne.Active = false;
                }
                if(bullTwo.IsColliding(wales))
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
                while (!gameOver)
                {
                    ProcessInput();  //this manages if movement/firing occurs for the tanks
                    bullOne.Move(); //moves the bullets because they are automated
                    bullTwo.Move();
                    DetectCollisions();  //does what it says
                    DrawGame(); //draws every updated condition

                    //Checks if either player has lost
                    if (playerOne.Loser)
                    {
                        EndGame();
                        gameOver = true;
                    }
                    if (playerTwo.Loser)
                    {
                        EndGame();
                        gameOver = true;
                    }
                    System.Threading.Thread.Sleep(100);  //has a delay of 100ms to avoid flickering
                }
                EndGame();
            }
                //This has nothing to do with the actual file processing, so the IOException and IndexOutOfRange
                //don't have specific errors.  However, the cursor could have ended out side of the window, warranting
                //an ArgumentOurOfRangeException
            catch (System.ArgumentOutOfRangeException argRange)
            {
                Console.Clear();
                Console.WriteLine("Please confine map.txt values to the window size (80 wide, 30 tall): " + argRange.Message);
            }
            catch (Exception generic)
            {
                Console.Clear();
                Console.WriteLine("Exception: " + generic.Message);
            }
        }
    }
}
