using System.Collections.Generic;
using System.ServiceModel;
using Model;

namespace WcfServiceLibrary
{
    [ServiceContract]
    public interface IService1
    {
        // ── Auth ──────────────────────────────────────────
        [OperationContract]
        User Login(string username, string password);

        [OperationContract]
        void Register(User u);

        // ── Branches ──────────────────────────────────────
        [OperationContract]
        List<Branch> GetAllBranches();

        [OperationContract]
        void AddBranch(Branch b);

        [OperationContract]
        void UpdateBranch(Branch b);

        [OperationContract]
        void DeleteBranch(int branchId);

        // ── Services ──────────────────────────────────────
        [OperationContract]
        List<Model.Service> GetAllServices();

        [OperationContract]
        List<Model.Service> GetServicesByBranch(int branchId);

        [OperationContract]
        void AddService(Model.Service s);

        [OperationContract]
        void UpdateService(Model.Service s);

        [OperationContract]
        void DeleteService(int serviceId);

        // ── Users ─────────────────────────────────────────
        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        User GetUserById(int userId);

        [OperationContract]
        void AddUser(User u);

        [OperationContract]
        void UpdateUser(User u);

        [OperationContract]
        void DeleteUser(int userId);

        // ── Queue ─────────────────────────────────────────
        [OperationContract]
        QueueTicket TakeTicket(int userId, int serviceId, int branchId);

        [OperationContract]
        QueueTicket GetActiveTicketByUser(int userId);

        [OperationContract]
        List<QueueTicket> GetTicketsByBranch(int branchId);

        [OperationContract]
        List<QueueTicket> GetWaitingTickets(int branchId, int serviceId);

        [OperationContract]
        int GetPositionInQueue(int ticketId, int branchId, int serviceId);

        [OperationContract]
        List<QueueTicket> GetTicketHistoryByUser(int userId);

        [OperationContract]
        void CallNextTicket(int branchId, int serviceId, int adminId);

        [OperationContract]
        void SkipTicket(int ticketId, int adminId);

        [OperationContract]
        void CloseTicket(int ticketId, int adminId);

        // ── CallLog ───────────────────────────────────────
        [OperationContract]
        List<CallLog> GetAllLogs();
    }
}
