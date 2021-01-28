using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Snake.Pages
{
    class Logic
    {
        //Snake Direction
        public char LastSnakeDir = 'U';
        public char SnakeDir = 'U';
        bool SnakeAlive = true;
        public string SnakeColor = "";

        int HeadLoc = 0;

        //Snake2 Direction (Two Plyr)
        public char LastSnake2Dir = 'L';
        public char Snake2Dir = 'L';
        bool Snake2Alive = false;
        public string Snake2Color = "DarkGreen";

        int HeadLoc2 = 0;

        //The Grid
        public int gridY;
        public int gridX;

        //Total Fruit on the Board
        int FruitCount = 0;

        bool GameActive = true;


        //Making a portal or 'round' map
        public int CheckYnX(int thisY, int thisX, out int newX)
        {
            //Defining Y & X
            int newY = thisY;
            newX = thisX;

            //For Y
            if (thisY >= gridY)
            {
                newY = 0;
            }
            else if (thisY < 0)
            {
                newY = gridY - 1;
            }
            //For X
            if (thisX >= gridX)
            {
                newX = 0;
            }
            else if (thisX < 0)
            {
                newX = gridX - 1;
            }

            return newY;
        } //Check Y n X ENDS


        //Updates Placement Based on Direction
        public int UpdatePlacement(char dir, int y, int x, out int newX)
        {
            switch (dir)
            {
                case 'U': y--; break;
                case 'L': x--; break;
                case 'R': x++; break;
                case 'D': y++; break;
            }

            newX = x;

            return y;
        } //UpdatePlacement ENDS

        //Rotates Img Based on Direction
        public void ChngImgRotation(char thisChar, Image thisImage)
        {
            switch (thisChar)
            {
                case 'U': thisImage.Rotation = 0; break;
                case 'R': thisImage.Rotation = 90; break;
                case 'D': thisImage.Rotation = 180; break;
                case 'L': thisImage.Rotation = 270; break;
            }
        } //Chng Img Rotation ENDS

        public bool Check4Tie(int thisHead, int oppHead)
        {
            bool Result = false;

            //If Snake bumped into Snake2's Head or Vise Versa
            if (thisHead == oppHead)
            {
                Result = true;
            }

            return Result;
        } //Chng Img Rotation ENDS


        //Controls Movement of Snakes
        public async Task<Tuple<int, int, string, int, int, int, int>> MoveSnake(Block[] TheGrid, int SnakeNum,
                                    int headY, int headX, string tailDir, int tailY, int tailX, int CurrLength, int ChiliCoolDown)
        {
            int lastHeadY = headY;
            int lastHeadX = headX;

            char ThisLastSnakeDir;
            char ThisSnakeDir;

            string thisSnakeColor;

            //Updating Directions
            if (SnakeNum == 1) //Snake 1
            {
                ThisLastSnakeDir = LastSnakeDir;
                ThisSnakeDir = SnakeDir;
                thisSnakeColor = SnakeColor;
            }
            else //Snake 2
            {
                ThisLastSnakeDir = LastSnake2Dir;
                ThisSnakeDir = Snake2Dir;
                thisSnakeColor = Snake2Color;
            }

            //Getting Directions
            headY = UpdatePlacement(ThisSnakeDir, headY, headX, out int newHeadX);
            headX = newHeadX;

            //Confirms Snake Direction Change
            ThisLastSnakeDir = ThisSnakeDir;

            //Add Tail Dir
            tailDir += ThisSnakeDir;

            //Updating Directions
            if (SnakeNum == 1) //Snake 1
            {
                LastSnakeDir = ThisLastSnakeDir;
                SnakeDir = ThisSnakeDir;
            }
            else //Snake 2
            {
                LastSnake2Dir = ThisLastSnakeDir;
                Snake2Dir = ThisSnakeDir;
            }

            //Making a portal or 'round' map
            headY = CheckYnX(headY, headX, out int newPortalHeadX);
            headX = newPortalHeadX;

            //If snake bumps into object
            if (TheGrid[(headY * gridX) + headX].SafeBlock == false)
            {
                TheGrid[(headY * gridX) + headX].Img.BackgroundColor = Color.DarkRed;

                //Rotates Head Img
                ChngImgRotation(ThisLastSnakeDir, TheGrid[(lastHeadY * gridX) + lastHeadX].Img);

                //Update Snakes State
                if (SnakeNum == 1) //Snake 1
                {
                    SnakeAlive = false;
                }
                else //Snake 2
                {
                    Snake2Alive = false;
                }

                GameActive = false;
            }
            else if (GameActive != false)
            {
                //If Snake Eats the Fruit
                if (TheGrid[(headY * gridX) + headX].HasFruit == true)
                {
                    //If Fruit Was a Chilie
                    if (TheGrid[(headY * gridX) + headX].HotChili == true)
                    {
                        //Removes Chili Effect
                        TheGrid[(headY * gridX) + headX].HotChili = false;

                        //Starts Hot Timer
                        ChiliCoolDown = 25;
                    }

                    //Removes Fruit
                    TheGrid[(headY * gridX) + headX].HasFruit = false;
                    TheGrid[(headY * gridX) + headX].Img.Source = "Empty.png";

                    //Adds to Score
                    CurrLength++;

                    //Removes Fruit from Count
                    FruitCount--;

                }
                //No Fruit thus No new Snake growth
                else
                {
                    //Updating Tail Location
                    tailY = UpdatePlacement(Convert.ToChar(tailDir.Substring(0, 1)), tailY, tailX, out int newTailX);
                    tailX = newTailX;

                    tailDir = tailDir.Substring(1);

                    //Making a portal or 'round' map
                    tailY = CheckYnX(tailY, tailX, out int newPortalTailX);
                    tailX = newPortalTailX;

                    //Moves  Snake Tail
                    TheGrid[(tailY * gridX) + tailX].Img.Source = "Empty.png";
                    TheGrid[(tailY * gridX) + tailX].Img.Rotation = 0;
                    TheGrid[(tailY * gridX) + tailX].SafeBlock = true;

                    //Finds out the Snakes New Tail Placement
                    int tempTailY = tailY;
                    int tempTailX = tailX;

                    tempTailY = UpdatePlacement(Convert.ToChar(tailDir.Substring(0, 1)), tempTailY, tempTailX, out int newTTailX);
                    tempTailX = newTTailX;

                    //Making a portal or 'round' map
                    tempTailY = CheckYnX(tempTailY, tempTailX, out int newTempTailX);
                    tempTailX = newTempTailX;

                    //Changes Body Img to Tail
                    TheGrid[(tempTailY * gridX) + tempTailX].Img.Source = thisSnakeColor + "SnakeTail.png";

                    //Rotates Head Img
                    ChngImgRotation(Convert.ToChar(tailDir.Substring(0, 1)), TheGrid[(tempTailY * gridX) + tempTailX].Img);

                } //No Fruit ENDS


                //Moving Snake Head
                TheGrid[(headY * gridX) + headX].Img.Source = thisSnakeColor + "SnakeHead.png";
                TheGrid[(headY * gridX) + headX].SafeBlock = false;

                //Rotates Head Img
                ChngImgRotation(ThisLastSnakeDir, TheGrid[(headY * gridX) + headX].Img);

                //Changes Old Head to Body
                int tempHeadY = headY;
                int tempHeadX = headX;

                //Updates Postion changes values to the Opposite of the UpdatePlacement Func
                switch (Convert.ToChar(tailDir.Substring(tailDir.Length - 1)))
                {
                    case 'U': tempHeadY++; break;
                    case 'L': tempHeadX++; break;
                    case 'R': tempHeadX--; break;
                    case 'D': tempHeadY--; break;
                }

                //Making a portal or 'round' map
                tempHeadY = CheckYnX(tempHeadY, tempHeadX, out int newTempHeadX);
                tempHeadX = newTempHeadX;

                //Changes Body Img to Tail
                TheGrid[(tempHeadY * gridX) + tempHeadX].Img.Source = thisSnakeColor + "SnakeBody.png";

                //Is Snake has recently eaten Chilie
                if (ChiliCoolDown > 0)
                {
                    //Cools down Snake
                    ChiliCoolDown--;

                    //A faster speed for Snake
                    await Task.Delay(100);
                }
                //Is Snake has NOT recently eaten Chilie
                else
                {
                    await Task.Delay(325 - (CurrLength / 5));
                }
            }

            //Setting Values
            return new Tuple<int, int, string, int, int, int, int>(headY, headX, tailDir, tailY, tailX, CurrLength, ChiliCoolDown);
        } //Function MoveSnake ENDS

        //Allows 2Plyr Snake to Move
        public async Task Moving2Plyrs(Block[] TheGrid, int SnakeNum,
                                    int headY, int headX, string tailDir, int tailY, int tailX, int CurrLength, int ChiliCoolDown)
        {
            //Moving Snake while Snakes are Alive
            while (GameActive == true)
            {
                //Moving Snake1
                var Snake1Info = await MoveSnake(TheGrid, SnakeNum,
                                                 headY, headX, tailDir, tailY, tailX, CurrLength, ChiliCoolDown);

                //Updating data Snake 1
                headY = Snake1Info.Item1;
                headX = Snake1Info.Item2;
                tailDir = Snake1Info.Item3;
                tailY = Snake1Info.Item4;
                tailX = Snake1Info.Item5;
                CurrLength = Snake1Info.Item6;
                ChiliCoolDown = Snake1Info.Item7;
            }

            //Update Snakes State
            if (SnakeNum == 1) //Snake 1
            {
                HeadLoc = (headY * gridX) + headX;
            }
            else //Snake 2
            {
                HeadLoc2 = (headY * gridX) + headX;
            }
        } //Moving2Plyrs ENDS


        //Snake Game Logic 1Plyr
        public async void GameOn1Plyr(Block[] TheGrid, Label ScoreLbl)
        {
            //Length starts at 0
            int CurrLength = 0;

            //Chilie Cool Down
            int ChiliCoolDown = 0;

            //Placing Snake Head in Center of Grid
            int headY = gridY / 2;
            int headX = gridX / 2;

            //String to contain Direction tail follows
            string tailDir = "";

            //Tails Point
            int tailY = headY + 5;
            int tailX = headX;

            //Sets Up Snake with start length of 5
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                { //Places Snake Head
                    TheGrid[((headY + i) * gridX) + headX].Img.Source = SnakeColor + "SnakeHead.png";
                }
                else if (i == 4) //Places Snake Tail
                {
                    TheGrid[((headY + i) * gridX) + headX].Img.Source = SnakeColor + "SnakeTail.png";
                }
                else //Places Snake Bodies
                {
                    TheGrid[((headY + i) * gridX) + headX].Img.Source = SnakeColor + "SnakeBody.png";
                }

                TheGrid[((headY + i) * gridX) + headX].SafeBlock = false;

                tailDir += "U";
            }


            //Gives Users a Chance to Get Ready
            await Task.Delay(3000);


            //Moving Snake while Alive
            while (SnakeAlive == true && GameActive == true)
            {
                //Moving Snake
                var Snake1Info = await MoveSnake(TheGrid, 1,
                                                 headY, headX, tailDir, tailY, tailX, CurrLength, ChiliCoolDown);

                //Updating data
                headY = Snake1Info.Item1;
                headX = Snake1Info.Item2;
                tailDir = Snake1Info.Item3;
                tailY = Snake1Info.Item4;
                tailX = Snake1Info.Item5;
                CurrLength = Snake1Info.Item6;
                ChiliCoolDown = Snake1Info.Item7;

                //Displaying new Score
                ScoreLbl.Text = "Score: " + (CurrLength * 5);
            }
            //Moving Snake while Alive ENDS

        }
        //Function GameOn1Plyr ENDS

        //Snake Game Logic 2Plyr
        public async void GameOn2Plyr(Block[] TheGrid, Label plyr1ScoreLbl, Label plyr2ScoreLbl)
        {
            //Snake 1 -----
            //Updates Direction for 2plyr
            LastSnakeDir = 'R';
            SnakeDir = 'R';

            //Placing Snake Head in Center of Grid
            int headY = 10;
            int headX = gridX / 2;

            //String to contain Direction tail follows
            string tailDir = "";

            //Tails Point
            int tailY = headY;
            int tailX = headX - 5;

            //Sets Up Snake1 with start length of 5
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                { //Places Snake Head
                    TheGrid[(headY * gridX) + headX - i].Img.Source = SnakeColor + "SnakeHead.png";
                    TheGrid[(headY * gridX) + headX - i].Img.Rotation = 90;
                }
                else if (i == 4) //Places Snake Tail
                {
                    TheGrid[(headY * gridX) + headX - i].Img.Source = SnakeColor + "SnakeTail.png";
                    TheGrid[(headY * gridX) + headX - i].Img.Rotation = 90;
                }
                else //Places Snake Bodies
                {
                    TheGrid[(headY * gridX) + headX - i].Img.Source = SnakeColor + "SnakeBody.png";
                }

                TheGrid[(headY * gridX) + headX - i].SafeBlock = false;

                tailDir += "R";
            }
            //Snake 1 ENDS -----

            //Snake 2 -----

            //Snake is Alive
            Snake2Alive = true;

            //Placing Snake Head in Center of Grid
            int headY2 = 4;
            int headX2 = gridX / 2;

            //String to contain Direction tail follows
            string tailDir2 = "";

            //Tails Point
            int tailY2 = headY2;
            int tailX2 = headX2 + 5;

            //Sets Up Snake2 with start length of 5
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                { //Places Snake Head
                    TheGrid[(headY2 * gridX) + headX2 + i].Img.Source = Snake2Color + "SnakeHead.png";
                    TheGrid[(headY2 * gridX) + headX2 + i].Img.Rotation = 270;
                }
                else if (i == 4) //Places Snake Tail
                {
                    TheGrid[(headY2 * gridX) + headX2 + i].Img.Source = Snake2Color + "SnakeTail.png";
                    TheGrid[(headY2 * gridX) + headX2 + i].Img.Rotation = 270;
                }
                else //Places Snake Bodies
                {
                    TheGrid[(headY2 * gridX) + headX2 + i].Img.Source = Snake2Color + "SnakeBody.png";
                }

                TheGrid[(headY2 * gridX) + headX2 + i].SafeBlock = false;

                tailDir2 += "L";
            }
            //Snake 2 ENDS -----

            //Gives Users a Chance to Get Ready
            await Task.Delay(3000);

            Task plyr1Moving = this.Moving2Plyrs(TheGrid, 1,
                                                     headY, headX, tailDir, tailY, tailX, 0, 0);
            Task plyr2Moving = this.Moving2Plyrs(TheGrid, 2,
                                                     headY2, headX2, tailDir2, tailY2, tailX2, 0, 0);

            await Task.WhenAll(plyr1Moving, plyr2Moving);

            //While Game is Running Wait for Game to End

            //Checking in Case of a Tie
            if (Check4Tie(HeadLoc, HeadLoc2) == true)
            {
                plyr1ScoreLbl.Text = plyr2ScoreLbl.Text = "Tie";
            } //Snake Wins
            else if (SnakeAlive == true)
            {
                plyr1ScoreLbl.Text = "Win";
                plyr1ScoreLbl.TextColor = Color.ForestGreen;

                plyr2ScoreLbl.Text = "Lose";
                plyr2ScoreLbl.TextColor = Color.DarkRed;
            } //Snake2 Wins
            else
            {
                plyr1ScoreLbl.Text = "Lose";
                plyr1ScoreLbl.TextColor = Color.DarkRed;

                plyr2ScoreLbl.Text = "Win";
                plyr2ScoreLbl.TextColor = Color.ForestGreen;
            }
        }
        //Function GameOn2Plyr ENDS


        public async void PlaceFruit(Block[] TheGrid)
        {
            Random luck = new Random();

            int FruitCoolDown = 0;

            //All Fruit Types
            string[] FruitTypes = new string[9];
            FruitTypes[0] = "Apple.png";
            FruitTypes[1] = "Blueberry.png";
            FruitTypes[2] = "Cherry.png";
            FruitTypes[3] = "Chili.png";
            FruitTypes[4] = "Grapes.png";
            FruitTypes[5] = "Orange.png";
            FruitTypes[6] = "WaterMelon.png";
            FruitTypes[7] = "Peach.png";
            FruitTypes[8] = "Pineapple.png";

            while (GameActive == true)
            {
                //Random Placement of fruit
                int randomY = luck.Next(0, gridY);
                int randomX = luck.Next(0, gridX);

                //Reduces Fruit CoolDown
                if (FruitCoolDown > 0)
                {
                    FruitCoolDown--;
                }

                //Random Chance for fruit once in a while
                if (luck.Next(0, 5) == 0 && TheGrid[((randomY) * gridX) + randomX].SafeBlock == true && FruitCount < 5 && FruitCoolDown == 0)
                {
                    int randFruit = luck.Next(0, 9);

                    //Adds to Fruit Count & Starts Cooldown
                    FruitCount++;
                    FruitCoolDown = 5;

                    //Places Fruit
                    TheGrid[(randomY * gridX) + randomX].Img.Source = FruitTypes[randFruit];
                    TheGrid[(randomY * gridX) + randomX].HasFruit = true;

                    if (FruitTypes[randFruit] == "Chili.png")
                    {
                        TheGrid[((randomY) * gridX) + randomX].HotChili = true;
                    }
                }

                await Task.Delay(200);
            }
            //Function PlaceFruit ENDS
        }
    }
}
