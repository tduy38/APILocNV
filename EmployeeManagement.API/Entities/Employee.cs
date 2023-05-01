using EmployeeManagement.API.Enums;

namespace EmployeeManagement.API.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Code { get; set; }    
        public string Fullname { get; set; }
        public Gender Gender { get; set; }  
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
       public Guid DepartmentID { get; set; }
        public Guid jobPositionID { get; set; }
        public WorkStatus WorkStatus { get; set; }
        public string Salary { get; set; }
        public DateTime JoiningDate { get; set; }      
    }
}
