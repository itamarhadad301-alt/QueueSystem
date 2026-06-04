using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class UserBLL
    {
        private UserDAL _dal = new UserDAL();

        public List<User> GetAllUsers()
        {
            return _dal.GetAllUsers();
        }

        public User GetUserById(int userId)
        {
            return _dal.GetUserById(userId);
        }

        public User Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new System.Exception("Username and password are required");
            return _dal.Login(username, password);
        }

        public void Register(User u)
        {
            if (string.IsNullOrWhiteSpace(u.FirstName) || string.IsNullOrWhiteSpace(u.LastName))
                throw new System.Exception("First and last name are required");
            if (string.IsNullOrWhiteSpace(u.Username))
                throw new System.Exception("Username is required");
            if (string.IsNullOrWhiteSpace(u.Password))
                throw new System.Exception("Password is required");
            if (_dal.UsernameExists(u.Username))
                throw new System.Exception("Username already exists");
            if (_dal.IsPhoneExists(u.Phone))
                throw new System.Exception("A user with this phone number already exists!");

            u.Role = "User";
            _dal.AddUser(u);
        }

        public void AddUser(User u)
        {
            if (_dal.UsernameExists(u.Username))
                throw new System.Exception("Username already exists");
            if (_dal.IsPhoneExists(u.Phone))
                throw new Exception("A user with this phone number already exists!");
            _dal.AddUser(u);
        }

        public void UpdateUser(User u)
        {
            if (string.IsNullOrWhiteSpace(u.FirstName) || string.IsNullOrWhiteSpace(u.LastName))
                throw new System.Exception("First and last name are required");
            if (_dal.UsernameExists(u.Username, u.UserId))
                throw new System.Exception("Username already exists");
            if (_dal.IsPhoneExists(u.Phone, u.UserId))
                throw new Exception("A user with this phone number already exists!");
            _dal.UpdateUser(u);
        }

        public void DeleteUser(int userId)
        {
            _dal.DeleteUser(userId);
        }
    }
}