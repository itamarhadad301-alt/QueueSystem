using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



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
            string sql = "SELECT * FROM Users WHERE UserId = @UserId";
            DataTable dt = ExecuteSelect(sql,
                new SqlParameter("@UserId", userId));
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public User Login(string username, string password)
        {
            string sql = "SELECT * FROM Users WHERE Username = @Username " +
                         "AND Password = @Password AND IsActive = 1";
            DataTable dt = ExecuteSelect(sql,
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password));
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public bool UsernameExists(string username, int excludeUserId = 0)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND UserId != @ExcludeUserId";
            int count = (int)ExecuteScalar(sql,
                new SqlParameter("@Username", username),
                new SqlParameter("@ExcludeUserId", excludeUserId));
            return count > 0;
        }

        public bool IsPhoneExists(string phone, int excludeUserId = 0)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE Phone = @Phone AND UserId != @ExcludeUserId";
            object result = ExecuteScalar(sql,
                new SqlParameter("@Phone", phone),
                new SqlParameter("@ExcludeUserId", excludeUserId));
            int count = Convert.ToInt32(result);
            return count != 0;
        }

        public void AddUser(User u)
        {
            string sql = "INSERT INTO Users (FirstName, LastName, Username, Password, Phone, Email, Role, IsActive) " +
                         "VALUES (@FirstName, @LastName, @Username, @Password, @Phone, @Email, @Role, 1)";
            ExecuteNonQuery(sql,
                new SqlParameter("@FirstName", u.FirstName),
                new SqlParameter("@LastName", u.LastName),
                new SqlParameter("@Username", u.Username),
                new SqlParameter("@Password", u.Password),
                new SqlParameter("@Phone", u.Phone ?? (object)DBNull.Value),
                new SqlParameter("@Email", u.Email ?? (object)DBNull.Value),
                new SqlParameter("@Role", u.Role));
        }

        public void UpdateUser(User u)
        {
            string sql = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, " +
                         "Username = @Username, Password = @Password, " +
                         "Phone = @Phone, Email = @Email, Role = @Role, " +
                         "IsActive = @IsActive " +
                         "WHERE UserId = @UserId";
            ExecuteNonQuery(sql,
                new SqlParameter("@FirstName", u.FirstName),
                new SqlParameter("@LastName", u.LastName),
                new SqlParameter("@Username", u.Username),
                new SqlParameter("@Password", u.Password),
                new SqlParameter("@Phone", u.Phone ?? (object)DBNull.Value),
                new SqlParameter("@Email", u.Email ?? (object)DBNull.Value),
                new SqlParameter("@Role", u.Role),
                new SqlParameter("@IsActive", u.IsActive ? 1 : 0),
                new SqlParameter("@UserId", u.UserId));
        }

        public void DeleteUser(int userId)
        {
            string sql = "DELETE FROM Users WHERE UserId = @UserId";
            ExecuteNonQuery(sql,
                new SqlParameter("@UserId", userId));
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