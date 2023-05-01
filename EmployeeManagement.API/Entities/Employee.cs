using EmployeeManagement.API.Enums;

namespace EmployeeManagement.API.Entities
{
    public class Employee
    {
        /// <summary>
        ///  Khai báo ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  Khai báo mã nhân viên
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///  Tên nhân viên
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// Giới tính: 0 là nam,1 là nữ, 2 là khác
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Ngày tháng năm sinh
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Id Vị trí 
        /// </summary>
        public Guid JobPositionId { get; set; }

        /// <summary>
        /// Trạng thái làm việc: 0 là thử việc, 1 là đang làm, 2 là đã nghỉ việc
        /// </summary>
        public WorkStatus WorkStatus { get; set; }

        /// <summary>
        /// Mức lương
        /// </summary>
        public Decimal Salary { get; set; }

        /// <summary>
        /// Ngày gia nhập công ty
        /// </summary>
        public DateTime JoiningDate { get; set; }      
    }
}
