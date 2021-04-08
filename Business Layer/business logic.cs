using Data_Access;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections.Generic;


// LV LANGA 218051864
namespace Business_Layer
{
    public class Class1
    {
    }

    public class clsMySQLOperations
    {
        private clsDataconnection objConn = new clsDataconnection(); // CREATE AN INSTANCE OF THE DATA ACCESS LAYER CLASS FOR ALL ITS METHODS

        public DataTable testQry()
        {
            DataTable dt = new DataTable(); // CREATING A DATATABLE TO HOW THE SQL DATA

            // HAVING A LOCAL INSTANCE OF THE CONNECTION
            var conn = objConn.CreateSQLConnection();

            // CREATING THE COLUMNS FOR THE DATATABLE TO HOUSE THE DETAILS FORM THE SQL QUERY
            dt.Columns.Add("show_ID", typeof(string));
            dt.Columns.Add("content_type", typeof(string));
            dt.Columns.Add("cast", typeof(string));
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("date_added", typeof(string));
            dt.Columns.Add("release_year", typeof(string));
            dt.Columns.Add("rating", typeof(string));
            dt.Columns.Add("duration", typeof(string));
            dt.Columns.Add("listed_in", typeof(string));
            dt.Columns.Add("content_description", typeof(string));

            // CREATING A NEW SQL COMMAND ( SOMETHING TO RUN SQL QUERIES )
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn; // SETTING THE CONNECTION FOR THE COMMAND
            cmd.CommandText = "SELECT * FROM `netflix_titles` "; // SETTING THE QUERY FOR THE COMMAND

            MySqlDataReader sqlRead = cmd.ExecuteReader(); // USING THIS TO READ THE RESULTS FROM THE QUERY 
            try
            {
                if (sqlRead.HasRows) // CHECKING IF THE QUERY HAS ROWS
                {
                    while (sqlRead.Read())
                    {
                        // ASSIGNING THE VALUES COLUMN BY COLUMN INTO VARIABLES
                        string showID = sqlRead.GetValue(0).ToString();
                        string contentType = sqlRead.GetValue(1).ToString();
                        string prodCast = sqlRead.GetValue(2).ToString();
                        string country = sqlRead.GetValue(3).ToString();
                        string dateadded = sqlRead.GetValue(4).ToString();
                        string releaseYr = sqlRead.GetValue(5).ToString();
                        string rating = sqlRead.GetValue(6).ToString();
                        string duration = sqlRead.GetValue(7).ToString();
                        string listing = sqlRead.GetValue(8).ToString();
                        string descr = sqlRead.GetValue(9).ToString();

                        // ADDING THE VARIABLES TO ROWS OF THE DATATABLE
                        dt.Rows.Add(showID, contentType, prodCast,
                            country, dateadded, releaseYr, rating,
                            duration, listing, descr);
                    }
                }
            }
            catch (Exception ex)
            {
                dt.Rows.Add(ex.Message); // IN THE EVENT THAT THERE IS AN ERROR I WRITE THAT TO THE DATATABLE
            }
            finally
            {
                sqlRead.Close(); // CLOSE THE READER OTHERWISE IT BREAKS OR WHATEVER
            }
            conn.Close();
            return dt;
        }
    }

    public class clsMyLINQOperations
    {

        public List<Titles> testQry()
        {
         clsDataconnection objConn = new clsDataconnection(); // CREATING A LOCAL INSTANCE OF THE DATA ACCESS LAYER CLASS
            DataTable dt = new DataTable(); // CREATING A DATATABLE
            var linqCSV = objConn.createLinq(); // ESSENTIALLY THE PATH TO THE CSV FILE

            var results = File.ReadAllLines(linqCSV) // RUNNING THE LINQ OPERATION 
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(Titles.ConvertRow).ToList(); // I HAD TO CREATE A OBJECT BECUASE THE CSV RUNNING COLUMN BY COLUMN WOULD BE A MESS

            return results;
        }
    }

    public class Titles
    {
        public string details { get; set; } // PLACEHOLDER FOR THE LINES READ
        


        internal static Titles ConvertRow(string row) // METHOD TO READ THE LINES AND ASSIGN TO OBJECT
        {
             clsDataconnection objConn = new clsDataconnection();
            
            return new Titles()
            {
                details = row //ASSIGNING THE DETAILS FROM THE ROW

            };

        }
    }
}