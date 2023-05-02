namespace EmployeeManagement.API.Entities.DTO
{
    /// <summary>
    /// Dữ liệu trả về cho api phân trang
    /// </summary>
    public class PagingResult
    {
        /// <summary>
        /// Danh sách nhân viên 
        /// </summary>
        public List<object> Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện
        /// </summary>
        public int TotalRecords { get; set; }
       
    }
}
