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
    public partial class GameSelectPage : ContentPage
    {

        readonly Label PlyrLbl;
        int PlyrNum = 1;

        public GameSelectPage()
        {
            InitializeComponent();

            //One Plyr or Two Plyr Option
            Switch PlyrSwitcher = new Switch
            {
                OnColor = Color.LimeGreen,
                ThumbColor = Color.ForestGreen,
                HorizontalOptions = LayoutOptions.Center,
            };
            PlyrSwitcher.Toggled += PlyrSwitcherToggled;

            PlyrLbl = new Label
            {
                Text = "Single Player",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
            };

            //Round Map
            Button NoWallsMapBtn = new Button
            {
                Text = "No Walls",
                FontSize = 25,
                BackgroundColor = Color.ForestGreen
            };
            NoWallsMapBtn.Clicked += Nav2GamePage;

            Content = new StackLayout
            {
                Margin = 50,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    PlyrSwitcher,
                    PlyrLbl,
                    new BoxView { BackgroundColor = Color.Transparent },
                    NoWallsMapBtn
                }
            };
        }

        void PlyrSwitcherToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                PlyrNum = 2;
                PlyrLbl.Text = String.Format("Two Player");
            }
            else
            {
                PlyrNum = 1;
                PlyrLbl.Text = String.Format("Single Player");
            }
        }

        public async void Nav2GamePage(object sender, EventArgs e)
        {
            //Pushing GameMode to GamePage
            await Navigation.PushAsync(new GamePage(PlyrNum, "Round"));
            Navigation.RemovePage(this);
        }
    }
}