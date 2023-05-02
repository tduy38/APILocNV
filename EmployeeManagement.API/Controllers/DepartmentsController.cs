using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// API lấy danh sách phòng ban
        /// </summary>
        /// <param name="keyword">Từ khóa phòng ban muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">Vị trí bản ghi bắt đầu lấy</param>
        /// <returns>Trả về đối tượng PagingResult,và danh sách phòng ban trên một trang & tổng số bản ghi thỏa mãn điều kiện</returns>
        [HttpGet]
        public IActionResult GetPaging(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0)
        {
            return Ok(new PagingResult
            {
                Data = new List<object>
                {
                    new Department
                    {
                        Id = Guid.NewGuid(),
                        Code = "PB0001",
                        Name = "Phòng giám đốc"
                    },
                    new Department
                    {
                        Id = Guid.NewGuid(),
                        Code = "PB0002",
                        Name = "Phòng nhân sự"
                    },
                    new Department
                    {
                        Id = Guid.NewGuid(),
                        Code = "PB0003",
                        Name = "Phòng bảo vệ"
                    }
                },
                TotalRecords = 3
            });
        }
    }
}
