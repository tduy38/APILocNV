namespace EmployeeManagement.API.Controllers
{
    internal class MaxlengthAttribute
    {
        public string ErrorMessage { get; internal set; }
        public int Length { get; internal set; }
    }
}