using System.Collections.Generic;
using BLL;
using Model;

namespace WcfServiceLibrary
{
    public class Service1 : IService1
    {
        private BranchBLL _branchBLL = new BranchBLL();
        private ServiceBLL _serviceBLL = new ServiceBLL();
        private UserBLL _userBLL = new UserBLL();
        private QueueTicketBLL _queueBLL = new QueueTicketBLL();
        private CallLogBLL _logBLL = new CallLogBLL();

        // ── Auth ──────────────────────────────────────────
        public User Login(string username, string password)
        {
            return _userBLL.Login(username, password);
        }

        public void Register(User u)
        {
            _userBLL.Register(u);
        }

        // ── Branches ──────────────────────────────────────
        public List<Branch> GetAllBranches()
        {
            return _branchBLL.GetAllBranches();
        }

        public void AddBranch(Branch b)
        {
            _branchBLL.AddBranch(b);
        }

        public void UpdateBranch(Branch b)
        {
            _branchBLL.UpdateBranch(b);
        }

        public void DeleteBranch(int branchId)
        {
            _branchBLL.DeleteBranch(branchId);
        }

        // ── Services ──────────────────────────────────────
        public List<Model.Service> GetAllServices()
        {
            return _serviceBLL.GetAllServices();
        }

        public List<Model.Service> GetServicesByBranch(int branchId)
        {
            return _serviceBLL.GetServicesByBranch(branchId);
        }

        public void AddService(Model.Service s)
        {
            _serviceBLL.AddService(s);
        }

        public void UpdateService(Model.Service s)
        {
            _serviceBLL.UpdateService(s);
        }

        public void DeleteService(int serviceId)
        {
            _serviceBLL.DeleteService(serviceId);
        }

        // ── Users ─────────────────────────────────────────
        public List<User> GetAllUsers()
        {
            return _userBLL.GetAllUsers();
        }

        public User GetUserById(int userId)
        {
            return _userBLL.GetUserById(userId);
        }

        public void AddUser(User u)
        {
            _userBLL.AddUser(u);
        }

        public void UpdateUser(User u)
        {
            _userBLL.UpdateUser(u);
        }

        public void DeleteUser(int userId)
        {
            _userBLL.DeleteUser(userId);
        }

        // ── Queue ─────────────────────────────────────────
        public QueueTicket TakeTicket(int userId, int serviceId, int branchId)
        {
            return _queueBLL.TakeTicket(userId, serviceId, branchId);
        }

        public QueueTicket GetActiveTicketByUser(int userId)
        {
            return _queueBLL.GetActiveTicketByUser(userId);
        }

        public List<QueueTicket> GetTicketsByBranch(int branchId)
        {
            return _queueBLL.GetTicketsByBranch(branchId);
        }

        public List<QueueTicket> GetWaitingTickets(int branchId, int serviceId)
        {
            return _queueBLL.GetWaitingTickets(branchId, serviceId);
        }

        public int GetPositionInQueue(int ticketId, int branchId, int serviceId)
        {
            return _queueBLL.GetPositionInQueue(ticketId, branchId, serviceId);
        }

        public List<QueueTicket> GetTicketHistoryByUser(int userId)
        {
            return _queueBLL.GetTicketHistoryByUser(userId);
        }

        public void CallNextTicket(int branchId, int serviceId, int adminId)
        {
            _queueBLL.CallNextTicket(branchId, serviceId, adminId);
        }

        public void SkipTicket(int ticketId, int adminId)
        {
            _queueBLL.SkipTicket(ticketId, adminId);
        }

        public void CloseTicket(int ticketId, int adminId)
        {
            _queueBLL.CloseTicket(ticketId, adminId);
        }

        // ── CallLog ───────────────────────────────────────
        public List<CallLog> GetAllLogs()
        {
            return _logBLL.GetAllLogs();
        }
    }
}