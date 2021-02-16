using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Snake.Models
{
    public class MapModel
    {
        [PrimaryKey, AutoIncrement]
        public int MapID { get; set; } //ID Primary Key
        public string MapName { get; set; } //Map's Name


        //Linking to UserScoresModel
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public UserScoresModel ChildUserScore { get; set; }


        //Linking to MapRowModel
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public MapRowModel ChildMapRow { get; set; }
    }
}