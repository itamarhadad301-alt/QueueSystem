using System;

namespace Model
{
    public class QueueTicket
    {
        public int TicketId { get; set; }
        public int TicketNumber { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public int BranchId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CalledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}