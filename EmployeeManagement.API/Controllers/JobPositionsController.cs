using EmployeeManagement.API.Entities.DTO;
using EmployeeManagement.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobPositionsController : ControllerBase
    {

        /// <summary>
        /// API lấy danh sách vị trí
        /// </summary>
        /// <param name="keyword">Từ khóa vị trí muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">Vị trí bản ghi bắt đầu lấy</param>
        /// <returns>Trả về đối tượng PagingResult,và danh sách vị trí trên một trang & tổng số bản ghi thỏa mãn điều kiện</returns>
        [HttpGet]
        public IActionResult GetPaging(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0
            )
        {
            return Ok(new PagingResult
            {
                
                Data = new List<Object>
            {
                 new JobPosition
                 {
                     Id = Guid.NewGuid(),
                     Code = "VT0001",
                     Name = "Giám đốc"
                 },
                 new JobPosition
                 {
                     Id = Guid.NewGuid(),
                     Code = "VT0002",
                     Name = "Nhân viên"
                 },
                 new JobPosition
                 {
                     Id = Guid.NewGuid(),
                     Code = "VT0003",
                     Name = "Bảo vệ"
                 }

            },
                TotalRecords = 3
            }

            );



        }
    }
}
