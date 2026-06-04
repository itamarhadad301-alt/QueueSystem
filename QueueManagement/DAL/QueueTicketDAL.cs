using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class QueueTicketDAL : BaseDAL
    {
        public List<QueueTicket> GetTicketsByBranch(int branchId)
        {
            string sql = "SELECT * FROM QueueTickets WHERE BranchId = @BranchId " +
                         "ORDER BY TicketNumber";
            DataTable dt = ExecuteSelect(sql,
                new SqlParameter("@BranchId", branchId));
            List<QueueTicket> list = new List<QueueTicket>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public List<QueueTicket> GetWaitingTickets(int branchId, int serviceId)
        {
            string sql = "SELECT * FROM QueueTickets WHERE BranchId = @BranchId " +
                         "AND ServiceId = @ServiceId AND Status = 'Waiting' " +
                         "ORDER BY TicketNumber";
            DataTable dt = ExecuteSelect(sql,
                new SqlParameter("@BranchId", branchId),
                new SqlParameter("@ServiceId", serviceId));
            List<QueueTicket> list = new List<QueueTicket>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public QueueTicket GetActiveTicketByUser(int userId)
        {
            string sql = "SELECT * FROM QueueTickets WHERE UserId = @UserId " +
                         "AND Status IN ('Waiting', 'Called')";
            DataTable dt = ExecuteSelect(sql,
                new SqlParameter("@UserId", userId));
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public List<QueueTicket> GetTicketHistoryByUser(int userId)
        {
            string sql = "SELECT * FROM QueueTickets WHERE UserId = @UserId " +
                         "ORDER BY CreatedAt DESC";
            DataTable dt = ExecuteSelect(sql,
                new SqlParameter("@UserId", userId));
            List<QueueTicket> list = new List<QueueTicket>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public int GetNextTicketNumber(int branchId, int serviceId)
        {
            string sql = "SELECT ISNULL(MAX(TicketNumber), 0) + 1 FROM QueueTickets " +
                         "WHERE BranchId = @BranchId AND ServiceId = @ServiceId";
            return (int)ExecuteScalar(sql,
                new SqlParameter("@BranchId", branchId),
                new SqlParameter("@ServiceId", serviceId));
        }

        public int AddTicket(QueueTicket t)
        {
            string sql = "INSERT INTO QueueTickets (TicketNumber, UserId, ServiceId, BranchId, Status) " +
                         "VALUES (@TicketNumber, @UserId, @ServiceId, @BranchId, 'Waiting'); " +
                         "SELECT SCOPE_IDENTITY();";
            return Convert.ToInt32(ExecuteScalar(sql,
                new SqlParameter("@TicketNumber", t.TicketNumber),
                new SqlParameter("@UserId", t.UserId),
                new SqlParameter("@ServiceId", t.ServiceId),
                new SqlParameter("@BranchId", t.BranchId)));
        }

        public void UpdateStatus(int ticketId, string status)
        {
            string calledAt = (status == "Called") ? ", CalledAt = GETDATE()" : "";
            string completedAt = (status == "Done" || status == "Skipped")
                                  ? ", CompletedAt = GETDATE()" : "";
            string sql = $"UPDATE QueueTickets SET Status = @Status{calledAt}{completedAt} " +
                         "WHERE TicketId = @TicketId";
            ExecuteNonQuery(sql,
                new SqlParameter("@Status", status),
                new SqlParameter("@TicketId", ticketId));
        }

        private QueueTicket Map(DataRow row)
        {
            return new QueueTicket
            {
                TicketId = (int)row["TicketId"],
                TicketNumber = (int)row["TicketNumber"],
                UserId = (int)row["UserId"],
                ServiceId = (int)row["ServiceId"],
                BranchId = (int)row["BranchId"],
                Status = row["Status"].ToString(),
                CreatedAt = (DateTime)row["CreatedAt"],
                CalledAt = row["CalledAt"] == DBNull.Value ? (DateTime?)null : (DateTime)row["CalledAt"],
                CompletedAt = row["CompletedAt"] == DBNull.Value ? (DateTime?)null : (DateTime)row["CompletedAt"]
            };
        }
    }
}