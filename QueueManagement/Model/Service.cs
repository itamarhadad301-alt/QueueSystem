namespace Model
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int AvgDuration { get; set; }
        public int BranchId { get; set; }
        public bool IsActive { get; set; }
    }
}