using System;
using System.IO;
using Snake.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Snake
{
    public partial class App : Application
    {
        static SnakeDatabase database;

        public static SnakeDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SnakeDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Contacts.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}