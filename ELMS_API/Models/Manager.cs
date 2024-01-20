using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ELMS_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ELMS_API.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }
        public int EmployeeId { get; set; }

        // Navigation property for Employee (One-to-One)
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
    }
}
