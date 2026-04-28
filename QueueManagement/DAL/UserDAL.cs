using Model;
using System;
using System.Collections.Generic;
using System.Data;



namespace DAL
{
    public class UserDAL : BaseDAL
    {
        public List<User> GetAllUsers()
        {
            string sql = "SELECT * FROM Users";
            DataTable dt = ExecuteSelect(sql);
            List<User> list = new List<User>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Map(row));
            }
            return list;
        }

        public User GetUserById(int userId)
        {
            string sql = $"SELECT * FROM Users WHERE UserId = {userId}";
            DataTable dt = ExecuteSelect(sql);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public User Login(string username, string password)
        {
            string sql = $"SELECT * FROM Users WHERE Username = '{username}' " +
                         $"AND Password = '{password}' AND IsActive = 1";
            DataTable dt = ExecuteSelect(sql);
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public bool UsernameExists(string username)
        {
            string sql = $"SELECT COUNT(*) FROM Users WHERE Username = '{username}'";
            int count = (int)ExecuteScalar(sql);
            return count > 0;
        }
        public bool IsPhoneExists(string phone, int excludeUserId = 0)
        {
            string sql = $"SELECT COUNT(*) FROM Users WHERE Phone = '{phone}' AND UserId != {excludeUserId}";
            object result = ExecuteScalar(sql);
            int count = Convert.ToInt32(result);
            return count != 0;
        }

        public void AddUser(User u)
        {
            string sql = $"INSERT INTO Users (FirstName, LastName, Username, Password, Phone, Email, Role, IsActive) " +
                         $"VALUES (N'{u.FirstName}', N'{u.LastName}', '{u.Username}', " +
                         $"'{u.Password}', '{u.Phone}', '{u.Email}', '{u.Role}', 1)";
            ExecuteNonQuery(sql);
        }

        public void UpdateUser(User u)
        {
            string sql = $"UPDATE Users SET FirstName = N'{u.FirstName}', LastName = N'{u.LastName}', " +
                         $"Phone = '{u.Phone}', Email = '{u.Email}', " +
                         $"IsActive = {(u.IsActive ? 1 : 0)} " +
                         $"WHERE UserId = {u.UserId}";
            ExecuteNonQuery(sql);
        }

        public void DeleteUser(int userId)
        {
            string sql = $"DELETE FROM Users WHERE UserId = {userId}";
            ExecuteNonQuery(sql);
        }

        private User Map(DataRow row)
        {
            return new User
            {
                UserId = (int)row["UserId"],
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Username = row["Username"].ToString(),
                Password = row["Password"].ToString(),
                Phone = row["Phone"].ToString(),
                Email = row["Email"].ToString(),
                Role = row["Role"].ToString(),
                IsActive = (bool)row["IsActive"],
                CreatedAt = (DateTime)row["CreatedAt"]
            };
        }
    }
}