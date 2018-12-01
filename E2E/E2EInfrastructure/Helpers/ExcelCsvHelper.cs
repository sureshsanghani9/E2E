using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2EInfrastructure.Helpers
{
    public static class ExcelCsvHelper
    {
        public static DataTable ConvertCsvToDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                StreamToDataTable(sr, dt);
            }

            return dt;
        }

        public static DataTable ConvertCsvToDataTable(Stream stream)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(stream))
            {
                StreamToDataTable(sr, dt);
            }

            return dt;
        }

        private static void StreamToDataTable(StreamReader sr, DataTable dt)
        {
            string[] headers = sr.ReadLine().Split(',');
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }

            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                if (rows.Length > 1)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i].Trim();
                    }

                    dt.Rows.Add(dr);
                }
            }
        }

        public static DataTable ConvertXslxToDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

                oledbConn.Close();
            }

            return dt;

        }
    }
}
