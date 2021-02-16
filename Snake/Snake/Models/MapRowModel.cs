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
        public string BlockType1 { get; set; } //Determine if Block is S = Stone or E = Empty
        public string BlockType2 { get; set; }
        public string BlockType3 { get; set; }
        public string BlockType4 { get; set; }
        public string BlockType5 { get; set; }
        public string BlockType6 { get; set; }
        public string BlockType7 { get; set; }
        public string BlockType8 { get; set; }
        public string BlockType9 { get; set; }
        public string BlockType10 { get; set; }
        public string BlockType11 { get; set; }
        public string BlockType12 { get; set; }
        public string BlockType13 { get; set; }
        public string BlockType14 { get; set; }
        public string BlockType15 { get; set; }
        public string BlockType16 { get; set; }
        public string BlockType17 { get; set; }
        public string BlockType18 { get; set; }
        public string BlockType19 { get; set; }
        public string BlockType20 { get; set; }
        public string BlockType21 { get; set; }
        public string BlockType22 { get; set; }
        public string BlockType23 { get; set; }
        public string BlockType24 { get; set; }
        public string BlockType25 { get; set; }


        //Foreign Key from Map Model
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public MapRowModel ParentMapID { get; set; }

        [ForeignKey(typeof(MapRowModel))]
        public int MapID { get; set; }
    }
}