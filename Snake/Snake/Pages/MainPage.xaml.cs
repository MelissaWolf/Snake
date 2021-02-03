using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;
using Snake.Pages;
using Xamarin.Forms;

namespace Snake
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
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

            //Checking4Users();
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

        //Refreshes the Page
        public void Go2ProfilePage()
        {
            //Refreshes Current Page
            Navigation.PushAsync(new ProfilePage());
            Navigation.RemovePage(this);
        }

        //Adding Users if there is no User Data
        public async void AddUsers()
        {
            //Creates 4 Users there will only ever be 4 User Slots
            for (int i = 1; i < 5; i++)
            {
                //Adds Default Users to DB
                await App.Database.SaveUserAsync(new UserModel
                {
                    UserID = 0,
                    UserName = "Player" + i,
                    ChiliesEaten = 0,
                    FruitEaten = 0,
                    Active = false
                });

            } // end of the loop

            Go2ProfilePage();
        }

        //Checking for Existence of Users
        public async void Checking4Users()
        {
            bool UsersState = await App.Database.CheckUsers();

            //If Users dont Exist
            if (UsersState == true)
            {
                AddUsers();
            }
        }
    }
}