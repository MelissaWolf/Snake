using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Snake.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; } //ID Primary Key
        public string UserName { get; set; } //UserName
        public int FruitEaten { get; set; } //Number of Fruit Eaten
        public int ChiliesEaten { get; set; } //Number of Chlies Eaten
        public int Active { get; set; } //Selects the Currently Used User
        public string SnakeActive { get; set; } //Selects the Currently Used User Snake Skin


        //Linking to UserScoresModel
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public UserScoresModel ChildUserScore { get; set; }
    }
}