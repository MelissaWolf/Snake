using System;
using System.Collections.Generic;
using System.Text;
using Snake.Models;
using Xamarin.Forms;

namespace Snake
{
    public class HSCell : ViewCell
    {
        public HSCell()
        {
            //Instantiate Each of our Views
            var grid = new Grid();

            var UserNameLbl = new Label
            {
                TextColor = Color.Black,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Start
            };
            var ScoreLbl = new Label
            {
                TextColor = Color.Black,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalTextAlignment = TextAlignment.End
            };

            //Set Bindings
            UserNameLbl.SetBinding(Label.TextProperty, "UserName");
            ScoreLbl.SetBinding(Label.TextProperty, "Score");

            //Add Views to the View Hierarchy
            grid.Children.Add(UserNameLbl);
            grid.Children.Add(ScoreLbl, 1, 0);

            View = grid;
        }
    }
}
