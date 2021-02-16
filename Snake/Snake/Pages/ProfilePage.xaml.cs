using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Snake.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        //Textbox Input for Username
        Entry UserNameEtry;

        //User Stats
        Label BestScoreLbl;
        Label FruitLbl;
        Label ChiliLbl;

        public ProfilePage()
        {
            InitializeComponent();

            StackLayout MainstackLayout = new StackLayout();

            //Making User List
            var UserList = new List<string>();

            UserList = App.Database.GetUsersAsync().Result.Select(usr => usr.UserName).ToList();

            var UserPicker = new Picker
            {
                Title = "Change Active User",
                TitleColor = Color.ForestGreen,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };
            UserPicker.ItemsSource = UserList;

            UserPicker.SelectedIndexChanged += async (sender, args) =>
            {
                var picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;

                //Changing Active User
                if (selectedIndex != -1)
                {
                    string UserSelected = (string)picker.ItemsSource[selectedIndex];

                    //Changes Old Active User
                    UserModel OldUser = await App.Database.GetActiveUserAsync();

                    await App.Database.SaveUserAsync(new UserModel
                    {
                        UserID = OldUser.UserID,
                        UserName = OldUser.UserName,
                        FruitEaten = OldUser.FruitEaten,
                        ChiliesEaten = OldUser.ChiliesEaten,
                        Active = 0,
                        SnakeActive = OldUser.SnakeActive
                    });

                    //Makes New Active User
                    UserModel ThisUser = await App.Database.GetUserByNameAsync(UserSelected);

                    await App.Database.SaveUserAsync(new UserModel
                    {
                        UserID = ThisUser.UserID,
                        UserName = ThisUser.UserName,
                        FruitEaten = ThisUser.FruitEaten,
                        ChiliesEaten = ThisUser.ChiliesEaten,
                        Active = 1,
                        SnakeActive = ThisUser.SnakeActive
                    });

                    //Refresh Page
                    Navigation.PushAsync(new ProfilePage());
                    Navigation.RemovePage(this);
                } //Changing Active User ENDS
            };

            MainstackLayout.Children.Add(UserPicker);

            //Current User
            UserNameEtry = new Entry
            {
                TextColor = Color.Black,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };
            UserNameEtry.Unfocused += UserNameChge;

            MainstackLayout.Children.Add(UserNameEtry);

            //User Stats
            BestScoreLbl = new Label
            {
                Text = "Best Score: ",
                TextColor = Color.Black,
                FontSize = 15
            };
            MainstackLayout.Children.Add(BestScoreLbl);

            FruitLbl = new Label
            {
                Text = "Total Fruit Eaten: ",
                TextColor = Color.Black,
                FontSize = 15
            };
            MainstackLayout.Children.Add(FruitLbl);

            ChiliLbl = new Label
            {
                Text = "Total Chilies Eaten: ",
                TextColor = Color.Black,
                FontSize = 15
            };
            MainstackLayout.Children.Add(ChiliLbl);


            ScrollView scrollView = new ScrollView { Content = MainstackLayout };

            Content = MainstackLayout;
            GetActiveUserInfo();
        }

        async void GetActiveUserInfo()
        {
            //Getting Active User
            UserModel MyUser = await App.Database.GetActiveUserAsync();

            UserNameEtry.Text = MyUser.UserName;

            //User Stats

            //User High Score If the User has Played a Game
            if (await App.Database.CheckUserHS(MyUser.UserID) == false)
            {
                BestScoreLbl.Text = "Best Score: 0";
            }
            else
            {
                UserScoresModel Hscore = await App.Database.GetBestHighScoreByUserAsync(MyUser.UserID);

                BestScoreLbl.Text = "Best Score: " + Hscore.Score;
            }

            FruitLbl.Text = "Total Fruit Eaten: " + MyUser.FruitEaten;
            ChiliLbl.Text = "Total Chilies Eaten: " + MyUser.ChiliesEaten;
        }

        public async void UserNameChge(object sender, EventArgs e)
        {
            var _entry = (Entry)sender;

            //If Username had special characters
            if (_entry.Text.Any(ch => !Char.IsLetterOrDigit(ch)))
            {
                DisplayAlert("Username Invalid", "Username cannot contain special characters.", "OK");
            }
            //If Username is less then 2 or exceeds 12
            else if (_entry.Text.Length < 2 || _entry.Text.Length > 12)
            {
                DisplayAlert("Username Invalid", "Username must be between 2 and 12 characters.", "OK");
            }
            //Username is Already in Use
            else if (await App.Database.CheckUsersByName(_entry.Text) == true)
            {
                DisplayAlert("Username Invalid", "Username is already in use.", "OK");
            }
            //Input is OK
            else
            {
                //Updates Username
                UserModel MyUser = await App.Database.GetActiveUserAsync();

                await App.Database.SaveUserAsync(new UserModel
                {
                    UserID = MyUser.UserID,
                    UserName = _entry.Text,
                    FruitEaten = MyUser.FruitEaten,
                    ChiliesEaten = MyUser.ChiliesEaten,
                    Active = MyUser.Active,
                    SnakeActive = MyUser.SnakeActive
                });

                //Refresh Page
                Navigation.PushAsync(new ProfilePage());
                Navigation.RemovePage(this);
            }
        }
    }
}