using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Snake.Models
{
    public class MapRowModel
    {
        [PrimaryKey, AutoIncrement]
        public int MapRowID { get; set; } //ID Primary Key
        public int RowNum { get; set; } //What Row this Is
        public char BlockType { get; set; } //Determine if Block is S = Stone or E = Empty

        public event PropertyChangedEventHandler PropertyChanged; //Helps with Binding Context with this Model


        //Foreign Key from Map Model
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public MapModel ParentMapID { get; set; }

        [ForeignKey(typeof(MapModel))]
        public int MapID { get; set; }
    }
}