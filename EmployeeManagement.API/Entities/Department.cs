namespace EmployeeManagement.API.Entities
{
    /// <summary>
    /// Khai báo thuộc tính phòng ban
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Khóa chính 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>  
        public string Code { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string Name { get; set; }
    }
}
