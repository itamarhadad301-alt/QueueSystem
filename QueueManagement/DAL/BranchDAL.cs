using System.Collections.Generic;
using System.Data;
using Model;

namespace DAL
{
    public class BranchDAL : BaseDAL
    {
        public List<Branch> GetAllBranches()
        {
            string sql = "SELECT * FROM Branches";
            DataTable dt = ExecuteSelect(sql);
            List<Branch> list = new List<Branch>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public Branch GetBranchById(int branchId)
        {
            string sql = $"SELECT * FROM Branches WHERE BranchId = {branchId}";
            DataTable dt = ExecuteSelect(sql);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public void AddBranch(Branch b)
        {
            string sql = $"INSERT INTO Branches (BranchName, Address, IsActive) " +
                         $"VALUES (N'{b.BranchName}', N'{b.Address}', 1)";
            ExecuteNonQuery(sql);
        }

        public void UpdateBranch(Branch b)
        {
            string sql = $"UPDATE Branches SET BranchName = N'{b.BranchName}', " +
                         $"Address = N'{b.Address}', IsActive = {(b.IsActive ? 1 : 0)} " +
                         $"WHERE BranchId = {b.BranchId}";
            ExecuteNonQuery(sql);
        }

        public void DeleteBranch(int branchId)
        {
            string sql = $"DELETE FROM Branches WHERE BranchId = {branchId}";
            ExecuteNonQuery(sql);
        }

        private Branch Map(DataRow row)
        {
            return new Branch
            {
                BranchId = (int)row["BranchId"],
                BranchName = row["BranchName"].ToString(),
                Address = row["Address"].ToString(),
                IsActive = (bool)row["IsActive"]
            };
        }
    }
}