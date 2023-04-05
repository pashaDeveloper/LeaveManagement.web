using LeaveManagement.web.Data.Attributes;

namespace LeaveManagement.web.Model
{
    [AuditTable]
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
