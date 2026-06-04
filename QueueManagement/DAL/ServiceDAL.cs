using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class ServiceDAL : BaseDAL
    {
        public List<Service> GetAllServices()
        {
            string sql = "SELECT * FROM Services";
            DataTable dt = ExecuteSelect(sql);
            List<Service> list = new List<Service>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public List<Service> GetServicesByBranch(int branchId)
        {
            string sql = "SELECT * FROM Services WHERE BranchId = @BranchId AND IsActive = 1";
            DataTable dt = ExecuteSelect(sql,
                new SqlParameter("@BranchId", branchId));
            List<Service> list = new List<Service>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public void AddService(Service s)
        {
            string sql = "INSERT INTO Services (ServiceName, AvgDuration, BranchId, IsActive) " +
                         "VALUES (@ServiceName, @AvgDuration, @BranchId, 1)";
            ExecuteNonQuery(sql,
                new SqlParameter("@ServiceName", s.ServiceName),
                new SqlParameter("@AvgDuration", s.AvgDuration),
                new SqlParameter("@BranchId", s.BranchId));
        }

        public void UpdateService(Service s)
        {
            string sql = "UPDATE Services SET ServiceName = @ServiceName, " +
                         "AvgDuration = @AvgDuration, BranchId = @BranchId, " +
                         "IsActive = @IsActive " +
                         "WHERE ServiceId = @ServiceId";
            ExecuteNonQuery(sql,
                new SqlParameter("@ServiceName", s.ServiceName),
                new SqlParameter("@AvgDuration", s.AvgDuration),
                new SqlParameter("@BranchId", s.BranchId),
                new SqlParameter("@IsActive", s.IsActive ? 1 : 0),
                new SqlParameter("@ServiceId", s.ServiceId));
        }

        public void DeleteService(int serviceId)
        {
            string sql = "DELETE FROM Services WHERE ServiceId = @ServiceId";
            ExecuteNonQuery(sql,
                new SqlParameter("@ServiceId", serviceId));
        }

        private Service Map(DataRow row)
        {
            return new Service
            {
                ServiceId = (int)row["ServiceId"],
                ServiceName = row["ServiceName"].ToString(),
                AvgDuration = (int)row["AvgDuration"],
                BranchId = (int)row["BranchId"],
                IsActive = (bool)row["IsActive"]
            };
        }
    }
}