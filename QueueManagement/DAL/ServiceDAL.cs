using System.Collections.Generic;
using System.Data;
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
            string sql = $"SELECT * FROM Services WHERE BranchId = {branchId} AND IsActive = 1";
            DataTable dt = ExecuteSelect(sql);
            List<Service> list = new List<Service>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public void AddService(Service s)
        {
            string sql = $"INSERT INTO Services (ServiceName, AvgDuration, BranchId, IsActive) " +
                         $"VALUES (N'{s.ServiceName}', {s.AvgDuration}, {s.BranchId}, 1)";
            ExecuteNonQuery(sql);
        }

        public void UpdateService(Service s)
        {
            string sql = $"UPDATE Services SET ServiceName = N'{s.ServiceName}', " +
                         $"AvgDuration = {s.AvgDuration}, BranchId = {s.BranchId}, " +
                         $"IsActive = {(s.IsActive ? 1 : 0)} " +
                         $"WHERE ServiceId = {s.ServiceId}";
            ExecuteNonQuery(sql);
        }

        public void DeleteService(int serviceId)
        {
            string sql = $"DELETE FROM Services WHERE ServiceId = {serviceId}";
            ExecuteNonQuery(sql);
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