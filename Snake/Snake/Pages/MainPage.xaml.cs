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
            Label ActiveUserLbl = new Label
            {
                Text = "Welcome",
                FontSize = 25,
                TextColor = Color.Lime,
                BackgroundColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };

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
                    ActiveUserLbl,
                    ProfileBtn,
                    new BoxView { BackgroundColor = Color.Transparent },
                    PlayBtn,
                    How2PlayBtn,
                    HSBtn
                }
            };

            Checking4Users(ActiveUserLbl);
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
            //No Walls Map ENDS
            #endregion

            #region Boxed In Map
            //Boxed In Map
            await App.Database.SaveMapInfoAsync(new MapModel
            {
                MapID = 0,
                MapName = "Boxed In"
            });
            //Boxed In Map ENDS
            #endregion
        }

        //Checking for Existence of Users
        public async void Checking4Users(Label ActiveUserLbl)
        {
            //If Users dont Exist
            if (await App.Database.CheckUsers() == false)
            {
                await AddUsers();
                await AddMaps();

                Go2ProfilePage();
            }
            else {
                UserModel MyUser = await App.Database.GetActiveUserAsync();

                ActiveUserLbl.Text = "Hello " + MyUser.UserName;
            }
        }
    }
}