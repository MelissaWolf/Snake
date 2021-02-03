using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Snake.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {

        readonly Logic Logic;

        public GamePage(int Plyrs, string Map)
        {
            InitializeComponent();

            Logic = new Logic();

            //Images for Grid
            Image Img;

            //Grid
            Grid Grid;
            int GridStartPoint;
            int GridEndPoint;

            //Defining based on Plyrs
            if (Plyrs == 1)
            { //Single Plyr
                Logic.gridY = 25;

                #region 1plyr Grid Definitions
                Grid = new Grid
                {
                    RowDefinitions =
                    {
                        //The Grid
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },

                        //Space
                        new RowDefinition { Height = new GridLength(50) },

                        //The Controls
                        new RowDefinition { Height = new GridLength(66) },
                        new RowDefinition { Height = new GridLength(66) },
                        new RowDefinition { Height = new GridLength(66) }
                    }
                };
                #endregion

                //Defining Grid Size
                GridStartPoint = 0;
                GridEndPoint = 25;

            } //Single Plyr ENDS
            else
            {  //Two Plyr
                Logic.gridY = 15;

                #region 2plyr Grid Definitions
                Grid = new Grid
                {
                    RowDefinitions =
                    {
                        //The Controls
                        new RowDefinition { Height = new GridLength(66) },
                        new RowDefinition { Height = new GridLength(66) },
                        new RowDefinition { Height = new GridLength(66) },

                        //Space
                        new RowDefinition { Height = new GridLength(30) },

                        //The Grid
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },
                        new RowDefinition { Height = new GridLength(15) },

                        //Space
                        new RowDefinition { Height = new GridLength(30) },

                        //The Controls
                        new RowDefinition { Height = new GridLength(66) },
                        new RowDefinition { Height = new GridLength(66) },
                        new RowDefinition { Height = new GridLength(66) }
                    }
                };
                #endregion

                //Defining Grid Size
                GridStartPoint = 4;
                GridEndPoint = 19;

                //Game Buttons
                Button UpBtn2 = new Button
                {
                    ImageSource = "Up.png",
                    CornerRadius = 10,
                    BackgroundColor = Color.DimGray
                };
                Grid.SetRow(UpBtn2, 0);
                Grid.SetColumn(UpBtn2, 8);
                Grid.SetColumnSpan(UpBtn2, 9);
                UpBtn2.Clicked += Snake2GoUp;

                Button LeftBtn2 = new Button
                {
                    ImageSource = "Left.png",
                    CornerRadius = 10,
                    BackgroundColor = Color.DimGray
                };
                Grid.SetRow(LeftBtn2, 1);
                Grid.SetColumn(LeftBtn2, 2);
                Grid.SetColumnSpan(LeftBtn2, 9);
                LeftBtn2.Clicked += Snake2GoLft;
                Button RightBtn2 = new Button
                {
                    ImageSource = "Right.png",
                    CornerRadius = 10,
                    BackgroundColor = Color.DimGray
                };
                Grid.SetRow(RightBtn2, 1);
                Grid.SetColumn(RightBtn2, 14);
                Grid.SetColumnSpan(RightBtn2, 9);
                RightBtn2.Clicked += Snake2GoRght;

                Button DownBtn2 = new Button
                {
                    ImageSource = "Down.png",
                    CornerRadius = 10,
                    BackgroundColor = Color.DimGray
                };
                Grid.SetRow(DownBtn2, 2);
                Grid.SetColumn(DownBtn2, 8);
                Grid.SetColumnSpan(DownBtn2, 9);
                DownBtn2.Clicked += Snake2GoDwn;
                //Game Buttons ENDS

                //Adding Btns to Grid
                Grid.Children.Add(UpBtn2);
                Grid.Children.Add(LeftBtn2);
                Grid.Children.Add(RightBtn2);
                Grid.Children.Add(DownBtn2);

            } //Two Plyr ENDS
            Logic.gridX = 25;

            //Array for Grid
            Block[] TheGrid = new Block[Logic.gridY * Logic.gridX];
            int GridPointIndex = 0;

            NavigationPage.SetHasNavigationBar(this, false);

            //Prevents White Gaps between Rows & Columns
            Grid.RowSpacing = 0;
            Grid.ColumnSpacing = 0;

            //Making Snake Map
            for (int r = GridStartPoint; r < GridEndPoint; r++)
            {

                //The Columns
                for (int c = 0; c < 25; c++)
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

                    GridPointIndex++;
                }
            }
            //Snake Map ENDS

            #region Arrow Keys Plyr1
            //Game Buttons
            Button UpBtn = new Button
            {
                ImageSource = "Up.png",
                CornerRadius = 10,
                BackgroundColor = Color.DimGray
            };
            Grid.SetRow(UpBtn, GridEndPoint + 1);
            Grid.SetColumn(UpBtn, 8);
            Grid.SetColumnSpan(UpBtn, 9);
            UpBtn.Clicked += SnakeGoUp;

            Button LeftBtn = new Button
            {
                ImageSource = "Left.png",
                CornerRadius = 10,
                BackgroundColor = Color.DimGray
            };
            Grid.SetRow(LeftBtn, GridEndPoint + 2);
            Grid.SetColumn(LeftBtn, 2);
            Grid.SetColumnSpan(LeftBtn, 9);
            LeftBtn.Clicked += SnakeGoLft;
            Button RightBtn = new Button
            {
                ImageSource = "Right.png",
                CornerRadius = 10,
                BackgroundColor = Color.DimGray
            };
            Grid.SetRow(RightBtn, GridEndPoint + 2);
            Grid.SetColumn(RightBtn, 14);
            Grid.SetColumnSpan(RightBtn, 9);
            RightBtn.Clicked += SnakeGoRght;

            Button DownBtn = new Button
            {
                ImageSource = "Down.png",
                CornerRadius = 10,
                BackgroundColor = Color.DimGray
            };
            Grid.SetRow(DownBtn, GridEndPoint + 3);
            Grid.SetColumn(DownBtn, 8);
            Grid.SetColumnSpan(DownBtn, 9);
            DownBtn.Clicked += SnakeGoDwn;
            //Game Buttons ENDS

            //Adding Btns to Grid
            Grid.Children.Add(UpBtn);
            Grid.Children.Add(LeftBtn);
            Grid.Children.Add(RightBtn);
            Grid.Children.Add(DownBtn);
            #endregion

            //Starting Game
            Logic.PlaceFruit(TheGrid);

            if (Plyrs == 1) //Single Plyr
            {
                //Game Score
                Label ScoreLbl = new Label
                {
                    Text = "Score: 0",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 25
                };
                Grid.SetRow(ScoreLbl, 25);
                Grid.SetColumn(ScoreLbl, 7);
                Grid.SetColumnSpan(ScoreLbl, 11);

                Grid.Children.Add(ScoreLbl);
                //Game Score ENDS

                #region Result Box
                //Result Box
                BoxView Box = new BoxView
                {
                    BackgroundColor = Color.LightGray,
                    Opacity = 0.8,
                    CornerRadius = 10,
                    IsVisible = false
                };
                Grid.SetRow(Box, 2);
                Grid.SetRowSpan(Box, 21);
                Grid.SetColumn(Box, 2);
                Grid.SetColumnSpan(Box, 21);

                Grid.Children.Add(Box);
                //Game Over Image
                Image GameOverImg = new Image
                {
                    Source = "GameOver.png",
                    Aspect = Aspect.AspectFill,
                    IsVisible = false
                };
                Grid.SetRow(GameOverImg, 1);
                Grid.SetRowSpan(GameOverImg, 8);
                Grid.SetColumn(GameOverImg, 3);
                Grid.SetColumnSpan(GameOverImg, 19);

                Grid.Children.Add(GameOverImg);

                #region Game Results Titles
                //Total Length Lbl
                Label LengthLbl = new Label
                {
                    Text = "Final Length:",
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    IsVisible = false
                };
                Grid.SetRow(LengthLbl, 8);
                Grid.SetRowSpan(LengthLbl, 2);
                Grid.SetColumn(LengthLbl, 3);
                Grid.SetColumnSpan(LengthLbl, 15);

                Grid.Children.Add(LengthLbl);
                //Total Fruit Lbl
                Label FruitLbl = new Label
                {
                    Text = "Total Fruit Eaten:",
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    IsVisible = false
                };
                Grid.SetRow(FruitLbl, 10);
                Grid.SetRowSpan(FruitLbl, 2);
                Grid.SetColumn(FruitLbl, 3);
                Grid.SetColumnSpan(FruitLbl, 15);

                Grid.Children.Add(FruitLbl);
                //Total Chilies Lbl
                Label ChiliLbl = new Label
                {
                    Text = "Total Chilies Eaten:",
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    IsVisible = false
                };
                Grid.SetRow(ChiliLbl, 12);
                Grid.SetRowSpan(ChiliLbl, 2);
                Grid.SetColumn(ChiliLbl, 3);
                Grid.SetColumnSpan(ChiliLbl, 15);

                Grid.Children.Add(ChiliLbl);
                //Final Score Lbl
                Label FinalScoreLbl = new Label
                {
                    Text = "Final Score:",
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    IsVisible = false
                };
                Grid.SetRow(FinalScoreLbl, 14);
                Grid.SetRowSpan(FinalScoreLbl, 2);
                Grid.SetColumn(FinalScoreLbl, 3);
                Grid.SetColumnSpan(FinalScoreLbl, 15);

                Grid.Children.Add(FinalScoreLbl);

                Label[] EndGameMenuTitles = new Label[4];
                EndGameMenuTitles[0] = LengthLbl;
                EndGameMenuTitles[1] = FruitLbl;
                EndGameMenuTitles[2] = ChiliLbl;
                EndGameMenuTitles[3] = FinalScoreLbl;
                #endregion
                #region Results Stats
                //Shows Total Length
                Label LengthNumLbl = new Label
                {
                    Text = "",
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    IsVisible = false
                };
                Grid.SetRow(LengthNumLbl, 8);
                Grid.SetRowSpan(LengthNumLbl, 2);
                Grid.SetColumn(LengthNumLbl, 17);
                Grid.SetColumnSpan(LengthNumLbl, 5);

                Grid.Children.Add(LengthNumLbl);
                //Shows Total Fruit
                Label FruitNumLbl = new Label
                {
                    Text = "",
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    IsVisible = false
                };
                Grid.SetRow(FruitNumLbl, 10);
                Grid.SetRowSpan(FruitNumLbl, 2);
                Grid.SetColumn(FruitNumLbl, 17);
                Grid.SetColumnSpan(FruitNumLbl, 5);

                Grid.Children.Add(FruitNumLbl);
                //Shows Total Chilies
                Label ChiliNumLbl = new Label
                {
                    Text = "",
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    IsVisible = false
                };
                Grid.SetRow(ChiliNumLbl, 12);
                Grid.SetRowSpan(ChiliNumLbl, 2);
                Grid.SetColumn(ChiliNumLbl, 17);
                Grid.SetColumnSpan(ChiliNumLbl, 5);

                Grid.Children.Add(ChiliNumLbl);
                //Shows Total Chilies
                Label FinalScoreNumLbl = new Label
                {
                    Text = "",
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    IsVisible = false
                };
                Grid.SetRow(FinalScoreNumLbl, 14);
                Grid.SetRowSpan(FinalScoreNumLbl, 2);
                Grid.SetColumn(FinalScoreNumLbl, 17);
                Grid.SetColumnSpan(FinalScoreNumLbl, 5);

                Grid.Children.Add(FinalScoreNumLbl);

                Label[] EndGameMenuResults = new Label[4];
                EndGameMenuResults[0] = LengthNumLbl;
                EndGameMenuResults[1] = FruitNumLbl;
                EndGameMenuResults[2] = ChiliNumLbl;
                EndGameMenuResults[3] = FinalScoreNumLbl;
                #endregion
                //Menu Button
                Button MenuBtn = new Button
                {
                    Text = "Menu",
                    TextColor = Color.Black,
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Color.ForestGreen,
                    CornerRadius = 10,
                    IsVisible = false
                };
                Grid.SetRow(MenuBtn, 17);
                Grid.SetRowSpan(MenuBtn, 5);
                Grid.SetColumn(MenuBtn, 3);
                Grid.SetColumnSpan(MenuBtn, 8);
                MenuBtn.Clicked += Back2Menu;

                Grid.Children.Add(MenuBtn);
                //Rematch Button
                Button PlayAgainBtn = new Button
                {
                    Text = "Play Again",
                    TextColor = Color.LimeGreen,
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Color.Black,
                    CornerRadius = 10,
                    IsVisible = false
                };
                Grid.SetRow(PlayAgainBtn, 17);
                Grid.SetRowSpan(PlayAgainBtn, 5);
                Grid.SetColumn(PlayAgainBtn, 14);
                Grid.SetColumnSpan(PlayAgainBtn, 8);
                PlayAgainBtn.Clicked += PlayAgain;

                Grid.Children.Add(PlayAgainBtn);

                Button[] EndGameMenuBtns = new Button[2];
                EndGameMenuBtns[0] = MenuBtn;
                EndGameMenuBtns[1] = PlayAgainBtn;
                //Result Box ENDS
                #endregion

                Content = Grid;

                //Starts Game
                Logic.GameOn1Plyr(TheGrid, ScoreLbl, Box, GameOverImg, EndGameMenuBtns, EndGameMenuTitles, EndGameMenuResults);
            }
            else //Two Plyr
            {
                //Game Result
                Label ResultLbl = new Label
                {
                    Text = "",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 25
                };
                Grid.SetRow(ResultLbl, 19);
                Grid.SetColumn(ResultLbl, 7);
                Grid.SetColumnSpan(ResultLbl, 11);

                Grid.Children.Add(ResultLbl);
                //Game Result ENDS

                //Game Result2
                Label ResultLbl2 = new Label
                {
                    Text = "",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 25,
                    Rotation = 180
                };
                Grid.SetRow(ResultLbl2, 3);
                Grid.SetColumn(ResultLbl2, 7);
                Grid.SetColumnSpan(ResultLbl2, 11);

                Grid.Children.Add(ResultLbl2);
                //Game Result2 ENDS

                #region Result Box
                //Result Box
                BoxView Box = new BoxView
                {
                    BackgroundColor = Color.LightGray,
                    Opacity = 0.8,
                    CornerRadius = 10,
                    IsVisible = false
                };
                Grid.SetRow(Box, 4);
                Grid.SetRowSpan(Box, 15);
                Grid.SetColumn(Box, 2);
                Grid.SetColumnSpan(Box, 21);

                Grid.Children.Add(Box);
                //Game Over Image
                Image GameOverImg = new Image
                {
                    Source = "GameOver.png",
                    Aspect = Aspect.AspectFill,
                    IsVisible = false
                };
                Grid.SetRow(GameOverImg, 4);
                Grid.SetRowSpan(GameOverImg, 8);
                Grid.SetColumn(GameOverImg, 3);
                Grid.SetColumnSpan(GameOverImg, 19);

                Grid.Children.Add(GameOverImg);
                //Menu Button
                Button MenuBtn = new Button
                {
                    Text = "Menu",
                    TextColor = Color.Black,
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Color.ForestGreen,
                    CornerRadius = 10,
                    IsVisible = false
                };
                Grid.SetRow(MenuBtn, 13);
                Grid.SetRowSpan(MenuBtn, 5);
                Grid.SetColumn(MenuBtn, 3);
                Grid.SetColumnSpan(MenuBtn, 8);
                MenuBtn.Clicked += Back2Menu;

                Grid.Children.Add(MenuBtn);
                //Rematch Button
                Button RematchBtn = new Button
                {
                    Text = "Rematch",
                    TextColor = Color.LimeGreen,
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Color.Black,
                    CornerRadius = 10,
                    IsVisible = false
                };
                Grid.SetRow(RematchBtn, 13);
                Grid.SetRowSpan(RematchBtn, 5);
                Grid.SetColumn(RematchBtn, 14);
                Grid.SetColumnSpan(RematchBtn, 8);
                RematchBtn.Clicked += PlayAgain;

                Grid.Children.Add(RematchBtn);

                Button[] EndGameMenuBtns = new Button[2];
                EndGameMenuBtns[0] = MenuBtn;
                EndGameMenuBtns[1] = RematchBtn;
                //Result Box ENDS
                #endregion

                Content = Grid;

                //Starts Game
                Logic.GameOn2Plyr(TheGrid, ResultLbl, ResultLbl2, Box, GameOverImg, EndGameMenuBtns);
            }

            //Game Btns
            void Back2Menu(object sender, EventArgs e)
            {
                //Refreshes Current Page
                Navigation.PushAsync(new MainPage());
                Navigation.RemovePage(this);
            }

            void PlayAgain(object sender, EventArgs e)
            {
                //Refreshes Current Page
                Navigation.PushAsync(new GamePage(Plyrs, Map));
                Navigation.RemovePage(this);
            }
            //Game Btns ENDS
        }

        //Snake Diection Btns
        //Changes Snake Direction to Up
        public void SnakeGoUp(object sender, EventArgs e)
        {
            if (Logic.LastSnakeDir != 'D')
            {
                Logic.SnakeDir = 'U';
            }
        }

        //Changes Snake Direction to Lft
        public void SnakeGoLft(object sender, EventArgs e)
        {
            if (Logic.LastSnakeDir != 'R')
            {
                Logic.SnakeDir = 'L';
            }
        }

        //Changes Snake Direction to Right
        public void SnakeGoRght(object sender, EventArgs e)
        {
            if (Logic.LastSnakeDir != 'L')
            {
                Logic.SnakeDir = 'R';
            }
        }
        //Changes Snake Direction to Down
        public void SnakeGoDwn(object sender, EventArgs e)
        {
            if (Logic.LastSnakeDir != 'U')
            {
                Logic.SnakeDir = 'D';
            }
        }
        //Snake Diection Btns ENDS

        //Snake Diection Btns Plyr2
        //Changes Snake Direction to Up
        public void Snake2GoUp(object sender, EventArgs e)
        {
            if (Logic.LastSnake2Dir != 'D')
            {
                Logic.Snake2Dir = 'U';
            }
        }

        //Changes Snake Direction to Lft
        public void Snake2GoLft(object sender, EventArgs e)
        {
            if (Logic.LastSnake2Dir != 'R')
            {
                Logic.Snake2Dir = 'L';
            }
        }

        //Changes Snake Direction to Right
        public void Snake2GoRght(object sender, EventArgs e)
        {
            if (Logic.LastSnake2Dir != 'L')
            {
                Logic.Snake2Dir = 'R';
            }
        }
        //Changes Snake Direction to Down
        public void Snake2GoDwn(object sender, EventArgs e)
        {
            if (Logic.LastSnake2Dir != 'U')
            {
                Logic.Snake2Dir = 'D';
            }
        }
        //Snake Diection Btns Plyr2 ENDS
    }
}