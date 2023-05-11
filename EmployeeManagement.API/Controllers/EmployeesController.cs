using Dapper;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using EmployeeManagement.API.Enums;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Reflection.Metadata;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Các API liên quan đến nhân viên
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {



        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeId)
        {
            try
            {
                //Chuẩn bị tên stored procedure
                string storedProcedureName = "Proc_employee_Delete";

                //Chuẩn bị tham số đầu vào cho stored
                var parameters = new DynamicParameters();
                parameters.Add("p_Id", employeeId);

                //Khởi tạo kết nối tới Database	

                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                //Thực hiện gọi vào Database để chạy stored procedure
                int numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về 
                if (numberOfAffectedRows == 1)
                {
                    return Ok(employeeId); // Trả về phản hồi HTTP 200 và id vừa xóa nếu xóa thành công
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.DatabaseFailed,
                    DevMsg = Resource.DevMsg_DatabaseFailed,
                    UserMsg = Resource.UserMsg_DatabaseFailed,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier

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
            [FromQuery] Guid departmentId,
            [FromQuery] Guid jobPositionId,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0)
        {
            try
            {
                string storedProcedureName = "Proc_employee_GetPaging";

                var parameters = new DynamicParameters();
                parameters.Add("p_Keyword", keyword);
                parameters.Add("p_DepartmentId", departmentId);
                parameters.Add("p_JobPositionId", jobPositionId);
                parameters.Add("p_Offset", offset);
                parameters.Add("p_Limit", limit);

                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                var multiResultSets = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                var employees = multiResultSets.Read<object>().ToList();

                int toltalRecords = multiResultSets.Read<int>().FirstOrDefault();

                return Ok(new PagingResult
                {
                    Data = employees,
                    TotalRecords = toltalRecords
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

        /// <summary>
        /// API lấy chi tiết một nhân viên
        /// </summary>
        /// <param name="employeeId">Id nhân viên muốn lấy</param>
        /// <returns>Chi tiết đối tượng nhân viên muốn lấy</returns>
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] Guid employeeId)
        {

            try// ctrl K S
            {
                //Chuẩn bị tên store procedure
                string storedProcedureName = "Proc_employee_GetbyId";

                //Chuẩn bị tham số đầu vào cho store
                var parameters = new DynamicParameters();

                parameters.Add("p_Id", employeeId);

                //Khởi tạo kết nối tới Database
                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                //Thực hiện gọi vào database để chạy stored procedure
                var employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về
                if (employee == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(employee);
                }
            }

            //Try catch để bắt exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.DevMsg_Exception,
                    MoreInfo = "https://errfix.com/1",
                    TraceId = HttpContext.TraceIdentifier

                });
            }


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
            try
            {
                //validate

                //lấy được toàn bộ thuộc tính của class employee             
                var properties = typeof(Employee).GetProperties();

                //Khai báo mảng lỗi
                var validateFailures = new List<string>();

                foreach (var property in properties)
                {
                    string propertyName = property.Name;

                    //Kiểm tra xem thuộc tính đó có attribute  required hay không
                    var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        if (String.IsNullOrEmpty(property.GetValue(newEmployee).ToString()))
                        {
                            validateFailures.Add(requiredAttribute.ErrorMessage);
                        }
                    }
                    //Kiểm tra xem thuộc tính đó có maxLength required hay không
                    var maxlengthAttribute = (MaxLengthAttribute)property.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault();
                    if (maxlengthAttribute != null)
                    {
                        if (property.GetValue(newEmployee).ToString().Length > maxlengthAttribute.Length)
                        {
                            validateFailures.Add(maxlengthAttribute.ErrorMessage);
                        }
                    }
                    //Kiểm tra xem thuộc tính đó có EmailAddress required hay không
                    var emailAddressAttribute = (EmailAddressAttribute)property.GetCustomAttributes(typeof(EmailAddressAttribute), false).FirstOrDefault();
                    if (emailAddressAttribute != null)
                    {
                        if (String.IsNullOrEmpty(property.GetValue(newEmployee)?.ToString()))
                        {
                            validateFailures.Add(emailAddressAttribute.ErrorMessage);
                        }
                    }
                }

                //Kiểm tra mảng lỗi xem có phần tử nào không và return về lỗi
                if (validateFailures.Count > 0)
                {
                    return BadRequest(new ErrorResult
                    {
                        ErrorCode = Enums.ErrorCode.InvalidData,
                        DevMsg = "",
                        UserMsg = "",
                        MoreInfo = validateFailures,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }

                //Chuẩn bị tên store procedure
                string storedProcedureName = "Proc_Employee_Insert";

                //Chuẩn bị tham số đầu vào cho store
                var parameters = new DynamicParameters();
                var newId = Guid.NewGuid();
                parameters.Add("p_Id", newId);
                parameters.Add("p_Code", newEmployee.Code);
                parameters.Add("p_FullName", newEmployee.FullName);
                parameters.Add("p_Gender", newEmployee.Gender);
                parameters.Add("p_DateOfBirth", newEmployee.DateOfBirth);
                parameters.Add("p_PhoneNumber", newEmployee.PhoneNumber);
                parameters.Add("p_Email", newEmployee.Email);
                parameters.Add("p_JobPositionID", newEmployee.JobPositionId);
                parameters.Add("p_DepartmentID", newEmployee.DepartmentId);
                parameters.Add("p_Salary", newEmployee.Salary);
                parameters.Add("p_WorkStatus", newEmployee.WorkStatus);
                parameters.Add("p_IdentityNumber", newEmployee.IdentityNumber);
                parameters.Add("p_IdentityIssuerDate", newEmployee.IdentityIssuerDate);
                parameters.Add("p_IdentityIssuerPlace", newEmployee.IdentityIssuerPlace);
                parameters.Add("p_TaxCode", newEmployee.TaxCode);
                parameters.Add("p_JoiningDate", newEmployee.JoiningDate);

                //Khởi tạo kết nối tới Database
                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                //Thực hiện gọi vào database để chạy stored procedure
                int numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về	
                if (numberOfAffectedRows == 1)
                {
                    return StatusCode(StatusCodes.Status201Created, newId);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.DatabaseFailed,
                    DevMsg = Resource.DevMsg_DatabaseFailed,
                    UserMsg = Resource.DevMsg_DatabaseFailed,
                    MoreInfo = "https://errfix.com/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }

            //Try catch để bắt exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.DevMsg_Exception,
                    MoreInfo = "https://errfix.com/1",
                    TraceId = HttpContext.TraceIdentifier

                });
            }
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

            try
            {

                //Validate đầu vào
                //Lấy danh sách thuộc tính của class Employee
                var properties = typeof(Employee).GetProperties();
                //Khai báo mảng các lỗi
                var validateFailures = new List<string>();
                //Chạy vòng lặp Foreach đi qua các thuộc tính 
                foreach (var property in properties)
                {
                    //Lấy tên thuộc tính
                    string propertyName = property.Name;

                    var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        if (string.IsNullOrEmpty(property.GetValue(updateEmployee).ToString()))
                        {
                            validateFailures.Add(requiredAttribute.ErrorMessage);
                        }
                    }

                    var maxlengthAttribute = (MaxLengthAttribute)property.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault();
                    if (maxlengthAttribute != null)
                    {
                        if (property.GetValue(updateEmployee).ToString().Length > maxlengthAttribute.Length)
                        {
                            validateFailures.Add(maxlengthAttribute.ErrorMessage);
                        }
                    }

                    var emailAddressAttribute = (EmailAddressAttribute)property.GetCustomAttributes(typeof(EmailAddressAttribute), false).FirstOrDefault();
                    if (emailAddressAttribute != null)
                    {
                        if (string.IsNullOrEmpty(property.GetValue(updateEmployee).ToString()))
                        {
                            validateFailures.Add(emailAddressAttribute.ErrorMessage);
                        }
                    }
                }
                if (validateFailures.Count > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = Enums.ErrorCode.InvalidData,
                        DevMsg = "",
                        UserMsg = "",
                        MoreInfo = validateFailures,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }

                // Chuẩn bị tên stored procedure
                string storedProcedureName = "Proc_employee_Update";

                // Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("p_Id", employeeId);
                parameters.Add("p_Code", updateEmployee.Code);
                parameters.Add("p_FullName", updateEmployee.FullName);
                parameters.Add("p_Gender", updateEmployee.Gender);
                parameters.Add("p_DateOfBirth", updateEmployee.DateOfBirth);
                parameters.Add("p_PhoneNumber", updateEmployee.PhoneNumber);
                parameters.Add("p_Email", updateEmployee.Email);
                parameters.Add("p_JobPositionId", updateEmployee.JobPositionId);
                parameters.Add("p_DepartmentId", updateEmployee.DepartmentId);
                parameters.Add("p_Salary", updateEmployee.Salary);
                parameters.Add("p_WorkStatus", updateEmployee.WorkStatus);
                parameters.Add("p_IdentityNumber", updateEmployee.IdentityNumber);
                parameters.Add("p_IdentityIssuerDate", updateEmployee.IdentityIssuerDate);
                parameters.Add("p_IdentityIssuerPlace", updateEmployee.IdentityIssuerPlace);
                parameters.Add("p_TaxCode", updateEmployee.TaxCode);
                parameters.Add("p_JoiningDate", updateEmployee.JoiningDate);

                // Thêm các tham số khác tùy thuộc vào thông tin cần cập nhật

                // Khởi tạo kết nối tới Database	

                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                // Thực hiện gọi vào Database để chạy stored procedure
                var employee = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                if (employeeId != null)
                {
                    return Ok(employeeId); // Trả về phản hồi HTTP 200 No Content nếu sửa thành công
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.DatabaseFailed,
                    DevMsg = Resource.DevMsg_DatabaseFailed,
                    UserMsg = Resource.UserMsg_DatabaseFailed,
                    MoreInfo = "https://google.com",
                    TraceId = HttpContext.TraceIdentifier
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











