using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ELMS_API.Models
{
    public class TeamMember
    {
        [Key]
        public int TeamMemberId { get; set; }
        public int ManagerId { get; set; }
        public int EmployeeId { get; set; }

        // Navigation properties for Manager (Many-to-One) and Employee (Many-to-One)
        [ForeignKey("ManagerId")]
        public Manager Manager { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
