namespace EmployeeManagement.API.Entities
{
    
        /// <summary>
        /// Khai báo thuộc tính vị trí, chức vụ
        /// </summary>
    public class JobPosition
    {
            /// <summary>
            /// Khóa chính
            /// </summary>
            public Guid Id { get; set; }
            /// <summary>
            /// Mã vị trí
            /// </summary>  
            public string Code { get; set; }
            /// <summary>
            /// Tên vị trí
            /// </summary>
           public string Name { get; set; }
    }
  
}
