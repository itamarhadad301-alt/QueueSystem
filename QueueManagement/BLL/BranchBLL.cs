using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public class BranchBLL
    {
        private BranchDAL _dal = new BranchDAL();

        public List<Branch> GetAllBranches()
        {
            return _dal.GetAllBranches();
        }

        public Branch GetBranchById(int branchId)
        {
            return _dal.GetBranchById(branchId);
        }

        public void AddBranch(Branch b)
        {
            if (string.IsNullOrWhiteSpace(b.BranchName))
                throw new System.Exception("Branch name cannot be empty");
            _dal.AddBranch(b);
        }

        public void UpdateBranch(Branch b)
        {
            if (string.IsNullOrWhiteSpace(b.BranchName))
                throw new System.Exception("Branch name cannot be empty");
            _dal.UpdateBranch(b);
        }

        public void DeleteBranch(int branchId)
        {
            _dal.DeleteBranch(branchId);
        }
    }
}