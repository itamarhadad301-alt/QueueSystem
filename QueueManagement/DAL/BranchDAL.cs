using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            string sql = "SELECT * FROM Branches WHERE BranchId = @BranchId";
            DataTable dt = ExecuteSelect(sql,
                new SqlParameter("@BranchId", branchId));
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public void AddBranch(Branch b)
        {
            string sql = "INSERT INTO Branches (BranchName, Address, IsActive) " +
                         "VALUES (@BranchName, @Address, 1)";
            ExecuteNonQuery(sql,
                new SqlParameter("@BranchName", b.BranchName),
                new SqlParameter("@Address", b.Address ?? (object)System.DBNull.Value));
        }

        public void UpdateBranch(Branch b)
        {
            string sql = "UPDATE Branches SET BranchName = @BranchName, " +
                         "Address = @Address, IsActive = @IsActive " +
                         "WHERE BranchId = @BranchId";
            ExecuteNonQuery(sql,
                new SqlParameter("@BranchName", b.BranchName),
                new SqlParameter("@Address", b.Address ?? (object)System.DBNull.Value),
                new SqlParameter("@IsActive", b.IsActive ? 1 : 0),
                new SqlParameter("@BranchId", b.BranchId));
        }

        public void DeleteBranch(int branchId)
        {
            string sql = "DELETE FROM Branches WHERE BranchId = @BranchId";
            ExecuteNonQuery(sql,
                new SqlParameter("@BranchId", branchId));
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