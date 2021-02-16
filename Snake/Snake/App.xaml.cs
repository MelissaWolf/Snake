using System;
using System.IO;
using Snake.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Snake
{
    public partial class App : Application
    {
        static SnakeDB database;

        public static SnakeDB Database
        {
            get
            {
                if (database == null)
                {
                    database = new SnakeDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SnakeDB.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Pages.MainPage());
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