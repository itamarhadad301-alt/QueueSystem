using System;
using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public class QueueTicketBLL
    {
        private QueueTicketDAL _dal = new QueueTicketDAL();

        public List<QueueTicket> GetTicketsByBranch(int branchId)
        {
            return _dal.GetTicketsByBranch(branchId);
        }

        public List<QueueTicket> GetWaitingTickets(int branchId, int serviceId)
        {
            return _dal.GetWaitingTickets(branchId, serviceId);
        }

        public QueueTicket GetActiveTicketByUser(int userId)
        {
            return _dal.GetActiveTicketByUser(userId);
        }

        public List<QueueTicket> GetTicketHistoryByUser(int userId)
        {
            return _dal.GetTicketHistoryByUser(userId);
        }

        public QueueTicket TakeTicket(int userId, int serviceId, int branchId)
        {
            // Check user doesnt already have an active ticket
            QueueTicket existing = _dal.GetActiveTicketByUser(userId);
            if (existing != null)
                throw new Exception("You already have an active ticket");

            int ticketNumber = _dal.GetNextTicketNumber(branchId, serviceId);

            QueueTicket ticket = new QueueTicket
            {
                TicketNumber = ticketNumber,
                UserId = userId,
                ServiceId = serviceId,
                BranchId = branchId,
                Status = "Waiting"
            };

            ticket.TicketId = _dal.AddTicket(ticket);
            return ticket;
        }

        public int GetPositionInQueue(int ticketId, int branchId, int serviceId)
        {
            List<QueueTicket> waiting = _dal.GetWaitingTickets(branchId, serviceId);
            for (int i = 0; i < waiting.Count; i++)
            {
                if (waiting[i].TicketId == ticketId)
                    return i + 1;
            }
            return 0;
        }

        public void CallNextTicket(int branchId, int serviceId, int adminId, string notes = "")
        {
            List<QueueTicket> waiting = _dal.GetWaitingTickets(branchId, serviceId);
            if (waiting.Count == 0)
                throw new Exception("No waiting tickets");

            QueueTicket next = waiting[0];
            _dal.UpdateStatus(next.TicketId, "Called");

            // Log the call
            CallLogDAL logDal = new CallLogDAL();
            logDal.AddLog(new CallLog
            {
                TicketId = next.TicketId,
                CalledBy = adminId,
                Notes = notes
            });
        }

        public void SkipTicket(int ticketId, int adminId)
        {
            _dal.UpdateStatus(ticketId, "Skipped");
            CallLogDAL logDal = new CallLogDAL();
            logDal.AddLog(new CallLog
            {
                TicketId = ticketId,
                CalledBy = adminId,
                Notes = "Skipped"
            });
        }

        public void CloseTicket(int ticketId, int adminId)
        {
            _dal.UpdateStatus(ticketId, "Done");
            CallLogDAL logDal = new CallLogDAL();
            logDal.AddLog(new CallLog
            {
                TicketId = ticketId,
                CalledBy = adminId,
                Notes = "Done"
            });
        }
    }
}