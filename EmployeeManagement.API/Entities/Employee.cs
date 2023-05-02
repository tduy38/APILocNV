using EmployeeManagement.API.Enums;

namespace EmployeeManagement.API.Entities
{
    public class Employee
    {
        /// <summary>
        ///  Khóa chính
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  Mã nhân viên
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
        /// Id phòng ban/khóa ngoại
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Id Vị trí /khóa ngoại
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

        /// <summary>
        /// Số Căn cước công dân
        /// </summary>
        public String IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime IdentityIssuerDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public String IdentityIssuerPlace { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string TaxCode { get; set; }
        
    }  
}
