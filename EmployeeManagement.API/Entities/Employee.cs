using EmployeeManagement.API.Enums;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage ="Mã nhân viên không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã nhân viên không được để quá 20 lý tự")]
        public string Code { get; set; }

        /// <summary>
        ///  Tên nhân viên
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên nhân viên không được để quá 100 lý tự")]
        public string FullName { get; set; }

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
        [Required]
        [MaxLength(25)] 
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Email không được để trống")]
        [MaxLength(50, ErrorMessage = "Email không được để quá 50 lý tự")]
        [EmailAddress(ErrorMessage ="Email chưa đúng định dạng")]// định dạng đúng email
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
        [Required(ErrorMessage = "Căn cước công dân không được để trống")]
        [MaxLength(25, ErrorMessage = "Căn cước công dân không được để quá 25 lý tự")]
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
