using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public abstract class BaseDAL
    {
        protected static string ConnectionString;

        protected BaseDAL()
        {
            // נתיב יחסי לפרויקט  
            //  Database2.mdf שבו נמצא
            string DBfileName = "Database1.mdf";
            string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DAL\"));
            ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                projectDir + DBfileName + ";Integrated Security=True";

            EnsureDatabaseSchema(projectDir);

            // נתיב מוחלט -לא מומלץ
            //ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\MyProject\\LayersExample2\\DAL\\Database1.mdf;Integrated Security=True";
            //ConnectionString =
            //@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\soft\source\repos\pizza\DAL\Database1.mdf;Integrated Security=True";
            //   @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\soft\source\repos\boba325\pizza\DAL\Database1.mdf;Integrated Security=True";
        }

        private static string DBPath()
        {
            // מקבל את כתובת ה-exe
            string s = Environment.CurrentDirectory;

            // פירוק המחרוזת למערך לפי '\'
            string[] ss = s.Split('\\');

            // הורדה של 3 תיקיות מהסוף (bin → Debug → netX)
            int x = ss.Length - 3;

            // שינוי שם התיקייה האחרונה
            ss[x] = "DAL";

            // חיתוך המערך לאורך החדש
            Array.Resize(ref ss, x + 1);

            // חיבור חזרה למחרוזת עם '\'
            string s1 = string.Join("\\", ss);

            return s1;
        }

        protected DataTable ExecuteSelect(string sql)
        {
            DataTable table = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                HandleError(ex, sql);
                throw; // חשוב! לא לבלוע שגיאה
            }
            finally
            {
                conn.Close();
            }

            return table;
        }

        protected int ExecuteNonQuery(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleError(ex, sql);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        protected object ExecuteScalar(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleError(ex, sql);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        protected virtual void HandleError(Exception ex, string sql)
        {
            // כאן אפשר:
            // לכתוב לקובץ
            // לשמור ל-DB
            // לשלוח לוג

            Console.WriteLine("SQL Error:");
            Console.WriteLine(sql);
            Console.WriteLine(ex.Message);
        }

        private static void EnsureDatabaseSchema(string projectDir)
        {
            string schemaPath = Path.Combine(projectDir, "DatabaseSchema.sql");
            if (!File.Exists(schemaPath))
            {
                return;
            }

            string sql = File.ReadAllText(schemaPath);
            if (string.IsNullOrWhiteSpace(sql))
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}



