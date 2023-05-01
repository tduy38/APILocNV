using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using EmployeeManagement.API.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// cac api lien quan den nhan vien
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// api lay danh sach nhan vien theo dieu kien loc va phan trang
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm(Mã NV,tên,sdt)</param>
        /// <param name="departmentID">id phòng ban</param>
        /// <param name="jobPositionID">id vi tri</param>
        /// <param name="limit">so ban ghi muon lay</param>
        /// <param name="offset">vi tri ban ghi bat dau lay</param>
        /// <returns>tra ve mot doi tuong pagingresult bao gom danh sach nhan viên
        /// trên một trang va tổng số bản ghi thỏa mãn điều kiện
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
                        jobPositionID= Guid.NewGuid(),
                        DepartmentID= Guid.NewGuid(),
                        Salary="1800000",
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
                        jobPositionID= Guid.NewGuid(),
                        DepartmentID= Guid.NewGuid(),
                        Salary="1800000",
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
                        jobPositionID= Guid.NewGuid(),
                        DepartmentID= Guid.NewGuid(),
                        Salary="1800000",
                        WorkStatus=WorkStatus.TrialJob,
                        JoiningDate=DateTime.Now
                    }
                },
                TotalRecords = 3

            };
        }

    }
}
