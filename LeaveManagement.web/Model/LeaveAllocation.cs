using LeaveManagement.web.Data.Attributes;

namespace LeaveManagement.web.Model
{
    [AuditTable]
    public class LeaveAllocation
    {
        public int Id { get; set; }
        public int NumberDays { get; set; }
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public string EmployeeId { get; set; }
    }
}
