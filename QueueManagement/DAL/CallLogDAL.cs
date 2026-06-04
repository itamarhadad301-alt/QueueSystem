using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class CallLogDAL : BaseDAL
    {
        public List<CallLog> GetAllLogs()
        {
            string sql = "SELECT * FROM CallLog ORDER BY CalledAt DESC";
            DataTable dt = ExecuteSelect(sql);
            List<CallLog> list = new List<CallLog>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public List<CallLog> GetLogsByDate(DateTime date)
        {
            string sql = "SELECT * FROM CallLog WHERE CAST(CalledAt AS DATE) = @Date " +
                         "ORDER BY CalledAt DESC";
            DataTable dt = ExecuteSelect(sql,
                new SqlParameter("@Date", date.Date));
            List<CallLog> list = new List<CallLog>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public void AddLog(CallLog log)
        {
            string sql = "INSERT INTO CallLog (TicketId, CalledBy, Notes) " +
                         "VALUES (@TicketId, @CalledBy, @Notes)";
            ExecuteNonQuery(sql,
                new SqlParameter("@TicketId", log.TicketId),
                new SqlParameter("@CalledBy", log.CalledBy),
                new SqlParameter("@Notes", log.Notes ?? ""));
        }

        private CallLog Map(DataRow row)
        {
            return new CallLog
            {
                LogId = (int)row["LogId"],
                TicketId = (int)row["TicketId"],
                CalledBy = (int)row["CalledBy"],
                CalledAt = (DateTime)row["CalledAt"],
                Notes = row["Notes"].ToString()
            };
        }
    }
}