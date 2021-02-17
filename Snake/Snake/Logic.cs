using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;
using Xamarin.Forms;

namespace Snake.Pages
{
    class Logic
    {
        //The Grid
        public Block[] TheGrid;

        //Snake Direction
        public char LastSnakeDir = 'U';
        public char SnakeDir = 'U';
        bool SnakeAlive = true;
        public string SnakeColor = "";

        int HeadLoc = 0;

        //Single Player only
        int ChiliNum = 0;

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

        //Is Game Active
        bool GameActive = true;



        //Gets MapID
        public async Task<int> GetMapID(string ThisMapName)
        {
            MapModel ThisMap = await App.Database.GetMapByNameAsync(ThisMapName);

            return ThisMap.MapID;
        }

        #region Make Blocks
        #region Empty
        //Makes Empty Block
        public async Task MakeEmptyBlock(Grid Grid, Image Img, int c, int r, int GridPointIndex)
        {
            Grid.Children.Add(new BoxView
            {
                BackgroundColor = Color.LightGreen,
                Margin = 0
            }, c, r);
            Grid.Children.Add(Img = new Image
            {
                BackgroundColor = Color.LightGreen,
                Margin = 0
            }, c, r);

            TheGrid[GridPointIndex] = new Block(true, false, false, Img);
        }
        //Makes Empty Block ENDS
        #endregion

        #region Wall
        //Makes Wall Block
        public async Task MakeWallBlock(Grid Grid, Image Img, int c, int r, int GridPointIndex)
        {
            Grid.Children.Add(new BoxView
            {
                BackgroundColor = Color.DarkGray,
                Margin = 0
            }, c, r);
            Grid.Children.Add(Img = new Image
            {
                BackgroundColor = Color.DarkGray,
                Margin = 0
            }, c, r);

            TheGrid[GridPointIndex] = new Block(false, false, false, Img);
            TheGrid[GridPointIndex].Img.Source = "Wall.png";
        }
        //Makes Wall Block ENDS
        #endregion
        #endregion

        #region Make Maps
        //Map Types
        #region No Walls
        //No Walls
        public async Task MakeNoWallMap(int GridStartPoint, int GridEndPoint, int GridPointIndex, Image Img, Grid Grid)
        {
            for (int r = GridStartPoint; r < GridEndPoint; r++)
            {
                //The Columns
                for (int c = 0; c < 25; c++)
                {
                    await MakeEmptyBlock(Grid, Img, c, r, GridPointIndex);
                    GridPointIndex++;
                }
            } //Map is Made
        }
        //No Walls ENDS
        #endregion

        #region Boxed In
        //Boxed In
        public async Task MakeBoxedInMap(int GridStartPoint, int GridEndPoint, int GridPointIndex, Image Img, Grid Grid)
        {
            //The Columns
            //Top Row
            for (int c = 0; c < 25; c++)
            {
                await MakeWallBlock(Grid, Img, c, GridStartPoint, GridPointIndex);
                GridPointIndex++;
            }
            //Top Row ENDS

            //Middle Rows
            for (int r = GridStartPoint + 1; r < GridEndPoint - 1; r++)
            {
                await MakeWallBlock(Grid, Img, 0, r, GridPointIndex);
                GridPointIndex++;

                //The Columns
                for (int c = 1; c < 24; c++)
                {
                    await MakeEmptyBlock(Grid, Img, c, r, GridPointIndex);
                    GridPointIndex++;
                }

                await MakeWallBlock(Grid, Img, 24, r, GridPointIndex);
                GridPointIndex++;
            }
            //Middle Rows ENDS

            //Bottom Row
            for (int c = 0; c < 25; c++)
            {
                await MakeWallBlock(Grid, Img, c, GridEndPoint - 1, GridPointIndex);

                GridPointIndex++;
            }
            //Bottom Row ENDS
        }
        //Boxed In ENDS
        #endregion
        //Map Types ENDS
        #endregion

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
        public async Task<Tuple<int, int, string, int, int, int, int>> MoveSnake(int SnakeNum,
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
            }
            else //Snake 2
            {
                LastSnake2Dir = ThisLastSnakeDir;
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
                        if (SnakeNum == 1) //Snake 1
                        {
                            ChiliNum++;
                        }

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
        public async Task Moving2Plyrs(int SnakeNum,
                                    int headY, int headX, string tailDir, int tailY, int tailX, int CurrLength, int ChiliCoolDown)
        {
            //Moving Snake while Snakes are Alive
            while (GameActive == true)
            {
                //Moving Snake1
                var Snake1Info = await MoveSnake(SnakeNum,
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
        public async void GameOn1Plyr(string MapName, Label ScoreLbl, BoxView Box, Image GameOverImg, Button[] EndGameMenuBtns, Label[] EndGameMenuTitles, Label[] EndGameMenuResults)
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
                var Snake1Info = await MoveSnake(1, headY, headX, tailDir, tailY, tailX, CurrLength, ChiliCoolDown);

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
            ScoreLbl.Text = "";
            //Moving Snake while Alive ENDS

            //Gives Users a Chance to See Result
            await Task.Delay(1000);

            //Showing GameOver Box
            Box.IsVisible = true;
            GameOverImg.IsVisible = true;
            EndGameMenuResults[0].Text = (CurrLength + 5) + "m";
            EndGameMenuResults[1].Text = Convert.ToString(CurrLength);
            EndGameMenuResults[2].Text = Convert.ToString(ChiliNum);
            EndGameMenuResults[3].Text = Convert.ToString(CurrLength * 5);

            //Making Visible
            for (int i = 0; i < EndGameMenuBtns.Length; i++)
            {
                EndGameMenuBtns[i].IsVisible = true;
            }

            for (int i = 0; i < EndGameMenuTitles.Length; i++)
            {
                EndGameMenuTitles[i].IsVisible = true;
            }

            for (int i = 0; i < EndGameMenuResults.Length - 1; i++)
            {
                EndGameMenuResults[i].IsVisible = true;
            }
            //Showing GameOver Box ENDS

            //Adds Stats to DB
            //Adds/Updates Map High Score

            int MapID = await GetMapID(MapName);
            UserModel MyUser = await App.Database.GetActiveUserAsync();

            if (await App.Database.CheckUserMapHS(MyUser.UserID, MapID) == true)
            {
                UserScoresModel MyCurrHScore = await App.Database.GetBestHighScoreByUserMapAsync(MyUser.UserID, MapID);

                //Only Updates Score if New Score is Better
                if (MyCurrHScore.Score < (CurrLength * 5))
                {
                    await App.Database.SaveHighScoreAsync(new UserScoresModel
                    {
                        ScoreID = MyCurrHScore.ScoreID,
                        Score = (CurrLength * 5),
                        MapID = MapID,
                        UserID = MyUser.UserID,
                        UserName = MyUser.UserName
                    });

                    EndGameMenuResults[4].IsVisible = true;
                }
            }
            //No Previous Score in DB
            else
            {
                await App.Database.SaveHighScoreAsync(new UserScoresModel
                {
                    ScoreID = 0,
                    Score = CurrLength * 5,
                    MapID = MapID,
                    UserID = MyUser.UserID,
                    UserName = MyUser.UserName
                });

                EndGameMenuResults[4].IsVisible = true;
            }

            //Updates User
            await App.Database.SaveUserAsync(new UserModel
            {
                UserID = MyUser.UserID,
                UserName = MyUser.UserName,
                FruitEaten = (MyUser.FruitEaten + CurrLength),
                ChiliesEaten = (MyUser.ChiliesEaten + ChiliNum),
                Active = 1,
                SnakeActive = ""
            });
        }
        //Function GameOn1Plyr ENDS

        //Snake Game Logic 2Plyr
        public async void GameOn2Plyr(Label plyr1ResultLbl, Label plyr2ResultLbl, BoxView Box, Image GameOverImg, Button[] EndGameMenuBtns)
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

            Task plyr1Moving = this.Moving2Plyrs(1, headY, headX, tailDir, tailY, tailX, 0, 0);
            Task plyr2Moving = this.Moving2Plyrs(2, headY2, headX2, tailDir2, tailY2, tailX2, 0, 0);

            await Task.WhenAll(plyr1Moving, plyr2Moving);

            //While Game is Running Wait for Game to End

            //Checking in Case of a Tie
            if (Check4Tie(HeadLoc, HeadLoc2) == true)
            {
                plyr1ResultLbl.Text = plyr2ResultLbl.Text = "Tie";
            } //Snake Wins
            else if (SnakeAlive == true)
            {
                plyr1ResultLbl.Text = "Win";
                plyr1ResultLbl.TextColor = Color.ForestGreen;

                plyr2ResultLbl.Text = "Lose";
                plyr2ResultLbl.TextColor = Color.DarkRed;
            } //Snake2 Wins
            else
            {
                plyr1ResultLbl.Text = "Lose";
                plyr1ResultLbl.TextColor = Color.DarkRed;

                plyr2ResultLbl.Text = "Win";
                plyr2ResultLbl.TextColor = Color.ForestGreen;
            }

            //Gives Users a Chance to See Result
            await Task.Delay(1000);

            //Showing GameOver Box
            Box.IsVisible = true;
            GameOverImg.IsVisible = true;

            for (int i = 0; i < EndGameMenuBtns.Length; i++)
            {
                EndGameMenuBtns[i].IsVisible = true;
            }
            //Showing GameOver Box ENDS
        }
        //Function GameOn2Plyr ENDS


        public async void PlaceFruit()
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
                if (luck.Next(0, 5) == 0 && TheGrid[((randomY) * gridX) + randomX].SafeBlock == true && TheGrid[((randomY) * gridX) + randomX].HasFruit == false && FruitCount < 5 && FruitCoolDown == 0)
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
