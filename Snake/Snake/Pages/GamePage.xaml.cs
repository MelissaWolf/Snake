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

        Logic Logic;

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

                //Defining Grid Size
                GridStartPoint = 0;
                GridEndPoint = 25;

            } //Single Plyr ENDS
            else
            {  //Two Plyr
                Logic.gridY = 15;

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

                Content = Grid;

                //Starts Game
                Logic.GameOn1Plyr(TheGrid, ScoreLbl);
            }
            else //Two Plyr
            {
                //Game Score
                Label ScoreLbl = new Label
                {
                    Text = "",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 25
                };
                Grid.SetRow(ScoreLbl, 19);
                Grid.SetColumn(ScoreLbl, 7);
                Grid.SetColumnSpan(ScoreLbl, 11);

                Grid.Children.Add(ScoreLbl);
                //Game Score ENDS

                //Game Score2
                Label ScoreLbl2 = new Label
                {
                    Text = "",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 25,
                    Rotation = 180
                };
                Grid.SetRow(ScoreLbl2, 3);
                Grid.SetColumn(ScoreLbl2, 7);
                Grid.SetColumnSpan(ScoreLbl2, 11);

                Grid.Children.Add(ScoreLbl2);
                //Game Score2 ENDS

                ScoreLbl.Text = "";

                Content = Grid;

                //Starts Game
                Logic.GameOn2Plyr(TheGrid, ScoreLbl, ScoreLbl2);
            }
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