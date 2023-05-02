using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using EmployeeManagement.API.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Các API liên quan đến nhân viên
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// API lấy danh sách nhân viên theo điều kiện lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm: Mã nhân viên(Code), tên nhân viên(Fullname), số điện thoại(PhoneNumber) </param>
        /// <param name="jobPositionId">Id vị trí</param>
        /// <param name="departmentId">Id phòng ban</param>
        /// <param name="limit">Số bản ghi muốn lấy </param>
        /// <param name="offset">Vị trí bản ghi bắt đầu lấy</param>
        /// <returns>
        /// Trả về đối tượng PagingResult,và danh sách sinh viên trên một trang & tổng số bản ghi thỏa mãn điều kiện
        /// </returns>
        [HttpGet]
        public IActionResult GetPaging(
            [FromQuery] string? keyword,
            [FromQuery] Guid jobPositionId,
            [FromQuery] Guid departmentId,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0)
        {
            return Ok(new PagingResult
            {
                Data = new List<object>
                {
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV001",
                        Fullname = "Nguyên Văn A",
                        Gender = Gender.Male,
                        DateOfBirth = new DateTime(2003,1,2),
                        PhoneNumber = "99999999",
                        Email = "nguyenvana@gmail.com",
                        JobPositionId = Guid.NewGuid(),
                        DepartmentId = Guid.NewGuid(),
                        Salary = 3440,
                        WorkStatus = WorkStatus.TrialJob,
                        IdentityNumber = "88888888",
                        IdentityIssuerDate = new DateTime(2016,3,12),
                        IdentityIssuerPlace = "Hà Nội",
                        TaxCode = "5555",
                        JoiningDate = new DateTime(2019,3,12),

                    },

                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV002",
                        Fullname = "Phạm Tuấn Duy",
                        Gender = Gender.Female,
                        DateOfBirth = new DateTime(2003,3,12),
                        PhoneNumber = "99999999",
                        Email = "tuanduy@gmail.com",
                        JobPositionId = Guid.NewGuid(),
                        DepartmentId = Guid.NewGuid(),
                        Salary = 0,
                        WorkStatus = WorkStatus.TrialJob,
                        IdentityNumber = "88888888",
                        IdentityIssuerDate = new DateTime(2017,3,12),
                        IdentityIssuerPlace = "Thái Bình",
                        TaxCode = "5555",
                        JoiningDate = new DateTime(2019,3,12),

                    },

                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV003",
                        Fullname = "Phùng Văn Đức",
                        Gender = Gender.Male,
                        DateOfBirth =  new DateTime(2002,3,12),
                        PhoneNumber = "99999999",
                        Email = "vanduc@gmail.com",
                        JobPositionId = Guid.NewGuid(),
                        DepartmentId = Guid.NewGuid(),
                        Salary = 0,
                        WorkStatus = WorkStatus.TrialJob,
                        IdentityNumber = "88888888",
                        IdentityIssuerDate = new DateTime(2016,3,12),
                        IdentityIssuerPlace = "Ba vì",
                        TaxCode = "5555",
                        JoiningDate = new DateTime(2019,3,12),
                    }

                },
                TotalRecords = 3


            });
        }

        /// <summary>
        /// API lấy chi tiết một nhân viên
        /// </summary>
        /// <param name="employeeId">Id nhân viên muốn lấy</param>
        /// <returns>Chi tiết đối tượng nhân viên muốn lấy</returns>
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] Guid employeeId)
        {
            return Ok(new Employee
            {
                Id = Guid.NewGuid(),
                Code = "NV001",
                Fullname = "Nguyên Văn A",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2003, 1, 2),
                PhoneNumber = "99999999",
                Email = "nguyenvana@gmail.com",
                JobPositionId = Guid.NewGuid(),
                DepartmentId = Guid.NewGuid(),
                Salary = 3440,
                WorkStatus = WorkStatus.TrialJob,
                IdentityNumber = "88888888",
                IdentityIssuerDate = new DateTime(2016, 3, 12),
                IdentityIssuerPlace = "Hà Nội",
                TaxCode = "5555",
                JoiningDate = new DateTime(2019, 3, 12),

            });
        }

        /// <summary>
        /// API lấy mã nhân viên mới tự động tăng
        /// </summary>
        /// <returns>Mã nhân viên mới tự động tăng</returns>
        [HttpGet("new-code")]
        public IActionResult GetNewEmployeeCode()
        {
            return Ok("NV99999");
        }

        /// <summary>
        /// API thêm mới nhân viên 
        /// </summary>
        /// <param name="newEmployee"> Đối tượng nhân viên muốn thêm mới </param>
        /// <returns> Id của một nhân viên mới thêm thành công </returns>
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee newEmployee)
        {
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
        }

        /// <summary>
        /// API sửa nhân viên
        /// </summary>
        /// <param name="updateEmployee">Đối tượng nhân viên muốn sửa </param>
        /// <param name="employeeId">Id của nhân viên đang muốn sửa</param>
        /// <returns>Id của nhân viên vừa mới sửa</returns>
        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee([FromBody] Employee updateEmployee, [FromRoute] Guid employeeId)
        {
            return Ok(employeeId);
        }

        /// <summary>
        /// API Xóa nhân viên
        /// </summary>
        /// <param name="employeeId">Id của nhân viên muốn xóa</param>
        /// <returns>Id của nhân viên vừa xóa</returns>
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeId)
        {
            return Ok(employeeId);
        }



    }
}
