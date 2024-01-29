using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Sprint0
{
    public class LoadCSVs
    {

        public static LoadCSVs instance = new LoadCSVs();
        public static LoadCSVs Instance
        {
            get
            {
                return instance;
            }
        }

        private LoadCSVs()
        {
        }

        public Room LoadRoom(String fileName, int[] pos, Game1 game)
        {
            DataTable room = MakeTables();
            string[] rows = File.ReadAllLines(fileName);
            string[] rowVaules;
            for (int i = 0; i < rows.Length; i++)
            {
                rowVaules = rows[i].Split(',');
                DataRow row = room.NewRow();
                row.ItemArray = rowVaules;
                room.Rows.Add(row);
            }
            return RoomParse.Instance.ParseRoom(room, game);
        }
        public DataTable MakeTables()
        {
            DataTable skeleton = new DataTable();
            DataColumn column1 = new DataColumn();
            DataColumn column2 = new DataColumn();
            column1.DataType = typeof(string);
            for(int i = 0; i < 15; i++)
            {
                skeleton.Columns.Add(new DataColumn());
            }
            return skeleton;
        }





    }
}
