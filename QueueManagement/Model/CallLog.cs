using System;

namespace Model
{
    public class CallLog
    {
        public int LogId { get; set; }
        public int TicketId { get; set; }
        public int CalledBy { get; set; }
        public DateTime CalledAt { get; set; }
        public string Notes { get; set; }
    }
}