namespace EmployeeManagement.API.Entities.DTO
{
    /// <summary>
    /// Du liệu trả về cho api phân trang
    /// </summary>
    public class PagingResult
    {
        /// <summary>
        /// Danh sach nhan vien 
        /// </summary>
        public List<Employee> Data { get; set; }
        /// <summary>
        /// tổng số bản ghi thỏa mãn điều kiện
        /// </summary>

        public int TotalRecords { get; set; }
    }
}
