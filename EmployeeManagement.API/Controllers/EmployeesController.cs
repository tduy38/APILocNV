using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using EmployeeManagement.API.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Các api liên quan đến nhân viên
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// api lấy danh sách nhân viên theo điều kiện lọc và phân trang
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm(Mã NV,tên,sdt)</param>
        /// <param name="departmentID">id phòng ban</param>
        /// <param name="jobPositionID">id vị trí</param>
        /// <param name="limit">số bản ghi muốn lấy</param>
        /// <param name="offset">vị trí bản ghi bắt đàu lấy </param>
        /// <returns>trả về một đối tượng PagingResult bao gồm danh sách nhân viên
        /// trên một trang và tổng số bản ghi thỏa mãn điều kiện
        /// </returns>
        [HttpGet]
        public PagingResult GetPaging([FromQuery]string? keyword,
            [FromQuery] Guid? departmentID,
            [FromQuery] Guid? jobPositionID,
            [FromQuery] int limit=10,
            [FromQuery] int offset = 0)
        {
            return new PagingResult
            {
                Data = new List<Employee>
                {
                    new Employee
                    {
                        Id= Guid.NewGuid(),
                        Code= "NV001",
                        Fullname="Pham Tuan Duy",
                        Gender= Enums.Gender.Male,
                        DateOfBirth= DateTime.Now,
                        PhoneNumber="0852225538",
                        Email= "ex@gmail.com",
                        JobPositionId= Guid.NewGuid(),
                        DepartmentId= Guid.NewGuid(),
                        Salary =1800000,
                        WorkStatus=WorkStatus.TrialJob,
                        JoiningDate=DateTime.Now
                    },
                    new Employee
                    {
                        Id= Guid.NewGuid(),
                        Code= "NV002",
                        Fullname="Hoang Quoc Tuan",
                        Gender= Enums.Gender.Male,
                        DateOfBirth= DateTime.Now,
                        PhoneNumber="0852225538",
                        Email= "ex@gmail.com",
                       JobPositionId= Guid.NewGuid(),
                        DepartmentId= Guid.NewGuid(),
                        Salary =1800000,
                        WorkStatus=WorkStatus.TrialJob,
                        JoiningDate=DateTime.Now
                    },
                    new Employee
                    {
                        Id= Guid.NewGuid(),
                        Code= "NV003",
                        Fullname="Phung Van Duc",
                        Gender= Enums.Gender.Female,
                        DateOfBirth= DateTime.Now,
                        PhoneNumber="0852225538",
                        Email= "ex@gmail.com",
                        JobPositionId= Guid.NewGuid(),
                        DepartmentId= Guid.NewGuid(),
                        Salary =1800000,
                        WorkStatus=WorkStatus.TrialJob,
                        JoiningDate=DateTime.Now
                    }
                },
                TotalRecords = 3

            };
        }

    }
}
