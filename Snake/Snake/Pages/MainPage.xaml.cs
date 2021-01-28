using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public async void Nav2GameSelPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameSelectPage());
        }

        public async void Nav2How2PlayPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new How2PlayPage());
        }
    }
}