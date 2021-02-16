using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Snake.Models
{
    public class UserScoresModel
    {
        [PrimaryKey, AutoIncrement]
        public int ScoreID { get; set; } //ID Primary Key
        public int Score { get; set; } //The Best Score of the Map


        //Foreign Key from User Model
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public UserModel ParentUserID { get; set; }

        [ForeignKey(typeof(UserModel))]
        public int UserID { get; set; }

        [ForeignKey(typeof(UserModel))]
        public string UserName { get; set; }

        //Foreign Key from Map Model
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public MapModel ParentMapID { get; set; }

        [ForeignKey(typeof(MapModel))]
        public int MapID { get; set; }
    }
}