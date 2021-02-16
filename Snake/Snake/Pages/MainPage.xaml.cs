using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;
using Snake.Pages;
using Xamarin.Forms;

namespace Snake.Pages
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            //Profile Btn
            Button ProfileBtn = new Button
            {
                Text = "My Profile",
                FontSize = 25,
                BackgroundColor = Color.ForestGreen
            };
            ProfileBtn.Clicked += Nav2ProfilPage;

            //Play Game Btn
            Button PlayBtn = new Button
            {
                Text = "Play",
                FontSize = 25,
                BackgroundColor = Color.ForestGreen
            };
            PlayBtn.Clicked += Nav2GameSelPage;

            //How 2 Play Btn
            Button How2PlayBtn = new Button
            {
                Text = "How to Play",
                FontSize = 25,
                BackgroundColor = Color.ForestGreen
            };
            How2PlayBtn.Clicked += Nav2How2PlayPage;

            //High Scores
            Button HSBtn = new Button
            {
                Text = "High Scores",
                FontSize = 25,
                BackgroundColor = Color.ForestGreen
            };
            HSBtn.Clicked += Nav2HSPage;


            Content = new StackLayout
            {
                Margin = 50,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    ProfileBtn,
                    new BoxView { BackgroundColor = Color.Transparent },
                    PlayBtn,
                    How2PlayBtn,
                    HSBtn
                }
            };

            Checking4Users();
        }

        public async void Nav2ProfilPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        public async void Nav2GameSelPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameSelectPage());
        }

        public async void Nav2How2PlayPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new How2PlayPage());
        }

        public async void Nav2HSPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HighScorePage("All"));
        }

        //Refreshes the Page
        public void Go2ProfilePage()
        {
            //Refreshes Current Page
            Navigation.PushAsync(new ProfilePage());
        }

        //Adding Users if there is no User Data
        public async Task AddUsers()
        {
            //Creates 4 Users there will only ever be 4 User Slots
            await App.Database.SaveUserAsync(new UserModel
            {
                UserID = 0,
                UserName = "Save1",
                FruitEaten = 0,
                ChiliesEaten = 0,
                Active = 1,
                SnakeActive = ""
            });

            for (int i = 2; i < 5; i++)
            {
                //Adds Default Users to DB
                await App.Database.SaveUserAsync(new UserModel
                {
                    UserID = 0,
                    UserName = "Save" + i,
                    FruitEaten = 0,
                    ChiliesEaten = 0,
                    Active = 0,
                    SnakeActive = ""
                });

            } //End of User Loop
        }

        //Adding Maps if there is no User Data
        public async Task AddMaps()
        {
            #region No Walls Map
            //No Walls Map
            await App.Database.SaveMapInfoAsync(new MapModel
            {
                MapID = 0,
                MapName = ("No Walls")
            });

            for (int i = 0; i < 25; i++)
            {
                //Adds Map Rows to DB
                await App.Database.SaveMapRowAsync(new MapRowModel
                {
                    MapRowID = 0,
                    RowNum = i,
                    BlockType1 = "E",
                    BlockType2 = "E",
                    BlockType3 = "E",
                    BlockType4 = "E",
                    BlockType5 = "E",
                    BlockType6 = "E",
                    BlockType7 = "E",
                    BlockType8 = "E",
                    BlockType9 = "E",
                    BlockType10 = "E",
                    BlockType11 = "E",
                    BlockType12 = "E",
                    BlockType13 = "E",
                    BlockType14 = "E",
                    BlockType15 = "E",
                    BlockType16 = "E",
                    BlockType17 = "E",
                    BlockType18 = "E",
                    BlockType19 = "E",
                    BlockType20 = "E",
                    BlockType21 = "E",
                    BlockType22 = "E",
                    BlockType23 = "E",
                    BlockType24 = "E",
                    BlockType25 = "E",
                    MapID = 1
                });

            } //End of MapRow Loop
              //No Walls Map ENDS
            #endregion

            #region Boxed In Map
            //Boxed In Map
            await App.Database.SaveMapInfoAsync(new MapModel
            {
                MapID = 0,
                MapName = "Boxed In"
            });

            for (int i = 0; i < 25; i++)
            {
                if (i == 0 || i == 24)
                {
                    //Adds Map Rows to DB
                    await App.Database.SaveMapRowAsync(new MapRowModel
                    {
                        MapRowID = 0,
                        RowNum = i,
                        BlockType1 = "S",
                        BlockType2 = "S",
                        BlockType3 = "S",
                        BlockType4 = "S",
                        BlockType5 = "S",
                        BlockType6 = "S",
                        BlockType7 = "S",
                        BlockType8 = "S",
                        BlockType9 = "S",
                        BlockType10 = "S",
                        BlockType11 = "S",
                        BlockType12 = "S",
                        BlockType13 = "S",
                        BlockType14 = "S",
                        BlockType15 = "S",
                        BlockType16 = "S",
                        BlockType17 = "S",
                        BlockType18 = "S",
                        BlockType19 = "S",
                        BlockType20 = "S",
                        BlockType21 = "S",
                        BlockType22 = "S",
                        BlockType23 = "S",
                        BlockType24 = "S",
                        BlockType25 = "S",
                        MapID = 2
                    });
                }
                else
                {
                    //Adds Map Rows to DB
                    await App.Database.SaveMapRowAsync(new MapRowModel
                    {
                        MapRowID = 0,
                        RowNum = i,
                        BlockType1 = "S",
                        BlockType2 = "E",
                        BlockType3 = "E",
                        BlockType4 = "E",
                        BlockType5 = "E",
                        BlockType6 = "E",
                        BlockType7 = "E",
                        BlockType8 = "E",
                        BlockType9 = "E",
                        BlockType10 = "E",
                        BlockType11 = "E",
                        BlockType12 = "E",
                        BlockType13 = "E",
                        BlockType14 = "E",
                        BlockType15 = "E",
                        BlockType16 = "E",
                        BlockType17 = "E",
                        BlockType18 = "E",
                        BlockType19 = "E",
                        BlockType20 = "E",
                        BlockType21 = "E",
                        BlockType22 = "E",
                        BlockType23 = "E",
                        BlockType24 = "E",
                        BlockType25 = "S",
                        MapID = 2
                    });
                }
            } //End of MapRow Loop
              //Boxed In Map ENDS
            #endregion
        }

        //Checking for Existence of Users
        public async void Checking4Users()
        {
            //If Users dont Exist
            if (await App.Database.CheckUsers() == false)
            {
                await AddUsers();
                await AddMaps();

                Go2ProfilePage();
            }
        }
    }
}