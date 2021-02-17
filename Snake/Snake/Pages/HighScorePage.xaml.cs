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
    public partial class HighScorePage : ContentPage
    {
        readonly Logic Logic;
        ListView HSListView;

        public HighScorePage(string MapName)
        {
            InitializeComponent();

            Logic = new Logic();

            StackLayout MainstackLayout = new StackLayout();

            //User Stats
            Label MapLbl = new Label
            {
                Text = "Showing Scores on Map:",
                TextColor = Color.Black,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };
            MainstackLayout.Children.Add(MapLbl);

            //Making Map List
            var MapList = new List<string>();

            MapList = App.Database.GetMapsAsync().Result.Select(nme => nme.MapName).ToList();
            MapList.Add("All");

            var MapPicker = new Picker
            {
                Title = MapName,
                TitleColor = Color.ForestGreen,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };
            MapPicker.ItemsSource = MapList;

            MapPicker.SelectedIndexChanged += async (sender, args) =>
            {
                var picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;

                //Changing HS Map
                if (selectedIndex != -1)
                {
                    //Changes Map Parameter
                    string MapSelected = (string)picker.ItemsSource[selectedIndex];

                    if (MapSelected != "All") {
                        MapSelected = Convert.ToString(await Logic.GetMapID(MapSelected));
                    }

                    Navigation.PushAsync(new HighScorePage(MapSelected));
                    Navigation.RemovePage(this);
                } //Changing HS Map ENDS
            };
            MainstackLayout.Children.Add(MapPicker);

            //List of HS
            Grid HSGrid = new Grid();
            CreateListView(MapName);
            HSGrid.Children.Add(HSListView);

            MainstackLayout.Children.Add(HSGrid);

            ScrollView scrollView = new ScrollView { Content = MainstackLayout };

            Content = scrollView;
        }

        public async void CreateListView(string MapName)
        {
            //Shows All Maps HS
            if (MapName == "All")
            {
                HSListView = new ListView
                {
                    ItemsSource = App.Database.GetHighScoresAsync().Result,
                    Margin = new Thickness(15, 0, 15, 0),
                    SelectionMode = (ListViewSelectionMode)SelectionMode.None
                };

                HSListView.ItemTemplate = new DataTemplate(typeof(HSCell));
            }
            else
            { //Shows Map HS
                HSListView = new ListView
                {
                    ItemsSource = App.Database.GetHighScoresByMapAsync(Convert.ToInt32(MapName)).Result,
                    Margin = new Thickness(15, 0, 15, 0),
                    SelectionMode = (ListViewSelectionMode)SelectionMode.None
                };

                HSListView.ItemTemplate = new DataTemplate(typeof(HSCell));
            }
        }
    }
}