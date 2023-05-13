using Dapper;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

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
            try
            {
                // Chuẩn bị tên stored procedure 
                String storedProcedureName = "Proc_department_GetPaging";
                // Chuẩn bị tham số đầu vào cho stored 
                var parameters = new DynamicParameters();
                parameters.Add("p_Keyword", keyword);
                parameters.Add("p_Limit", limit);
                parameters.Add("p_Offset", offset);

                // Khởi tạo kết nối tới Database
                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
                // Thực hiện gọi vào Database để chạy stored procedure
                var multipleResultSets = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var departments = multipleResultSets.Read<object>().ToList();
                int totalRecords = multipleResultSets.Read<int>().FirstOrDefault();

                return Ok(new PagingResult
                {
                    Data = departments,
                    TotalRecords = totalRecords
                });
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier
                });

            }
        }
    }
}




