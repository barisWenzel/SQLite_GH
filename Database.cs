using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Data;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using System.Linq;



namespace SQLite_GH
{
    public static class Database
    {


        public static SQLiteConnection connection;


        #region Methods

        public static void CreateDatabase(string name, string dir)
        {
            connection = new SQLiteConnection("Data Source=" + dir + name + ".db;Version=3;");

            if (!File.Exists(dir + name + ".db"))
            {
                SQLiteConnection.CreateFile(dir + name + ".db");
            }

        }



        public static void SetUpConnectionString(string dir)
        {
            connection= new SQLiteConnection("Data Source=" + dir +";Version=3;");
        }


        public static void CreateTable(SQLiteConnection connection, string table, List<GH_String> columns, List<string> dataType, bool overrideDb)
        {
            using (connection)//use the using keyword to prevent issues if it crashes during writing 
            {
                connection.Open();
                //Skip if exists
                //if (overrideDb)
                //{
                //    var fm = connection.Database;

                //    if(File.Exists(Dir + Name + ".db"))
                //        File.Delete(Dir + Name + ".db");
                //}

                var delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS " + table;
                delTableCmd.ExecuteNonQuery();

                //Create table with column

                //var concatenated = "(";
                //for (int i = 0; i < columns.Count; i++)
                //{
                //    concatenated += columns[i];
                //    concatenated += " ";
                //    concatenated += dataType[i];
                //    if (i < columns.Count - 1)
                //        concatenated += ",";
                //}

                //concatenated += ")";

                //Create table with column
                string query = "create table " + table + Util.concatenate(columns);
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();// need to be closed? since it is inside a using statement supuestamente 
            }
        }

        public static void CreateTable(SQLiteConnection connection, string name, bool overrideIt)
        {

            connection.Open();
            if (overrideIt == true)
            {
                //Skip if exists
                var delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS " + name;
                delTableCmd.ExecuteNonQuery();
            }

            else
            {
                //Create table with column
                string query = "CREATE TABLE IF NOT EXISTS " + name;
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }


        public static void CreateEntry(SQLiteConnection connection, string table, List<GH_String> columns, GH_Structure<GH_String> values)
        {
            //https://www.sqlitetutorial.net/sqlite-insert/
            //Concatenate columns
            var concatenatedColumns = Util.concatenate(columns);

            //concatenate entries
            string concatenatedEntries = string.Empty;
            for (int i = 0; i < values.Branches.Count; i++)
            {
                concatenatedEntries += Util.concatenate(values.Branches[i]);
                if (i < values.Branches.Count - 1)
                    concatenatedEntries += ",";
            }

            using ( connection) //use the using keyword to prevent issues if it craches during writing
            {
                connection.Open();
                var query = "INSERT INTO " + table + string.Format("{0} {1}{2};", concatenatedColumns, " VALUES", concatenatedEntries);
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();

            }
        }



        //this method is completly taken from stackoverflow, I missed save the link
        public static List<System.Object> ReadValues(SQLiteConnection connection, string column, string table)
        {
            
            connection.Open();
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT " + column + " FROM " + table;
            //selectCmd.CommandText = "SELECT * From " + table + " Where " + column + " IS NULL";

            var output = new List<System.Object>();
            using (var reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var message = reader.GetValue(0);
                    output.Add(message);
                }
            }
            connection.Close();
            return output;
        }

        //this method is completly taken from stackoverflow, I missed save the link
        public static List<string> GetColumns(string table, SQLiteConnection connection)
        {

            var columnNames = new List<string>();
            connection.Open();
            var cmd = new SQLiteCommand("select * from " + table, connection);
            var dr = cmd.ExecuteReader();
            for (var i = 0; i < dr.FieldCount; i++)
            {
                columnNames.Add(dr.GetName(i));
            }
            return columnNames;
        }

        //this method is completly taken from stackoverflow, I missed save the link
        public static List<string> GetTableNames(SQLiteConnection connection)
        {

            List<string> tables = new List<string>();

            // executes query that select names of all tables in master table of the database
            String query = "SELECT name FROM sqlite_master " +
              "WHERE type = 'table'" +
              "ORDER BY 1";

            DataTable table = new DataTable();
            using (connection)
            {
                connection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        table.Load(rdr);
                    }
                }
                connection.Close();
            }

            foreach (DataRow row in table.Rows)
            {
                tables.Add(row.ItemArray[0].ToString());
            }

            return tables;

        }

        #endregion

    }

}
