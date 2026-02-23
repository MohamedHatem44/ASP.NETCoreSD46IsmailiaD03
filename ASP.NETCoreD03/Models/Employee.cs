using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreD03.Models
{
    // DB Model
    // Domain Model
    // Arc 
    // DAL BL PL
    public class Employee
    {
        /*------------------------------------------------------------------*/
        public int Id { get; set; }
        //[MinLength] // Later MVC // Validation VM
        public required string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        /*------------------------------------------------------------------*/
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        /*------------------------------------------------------------------*/
    }
}
