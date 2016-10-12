using System.ComponentModel.DataAnnotations;
using TestAssignment.DAL;
using AutoMapper;

namespace TestAssignment.Web.Models
{
    public class EmployeeDataModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        [Required]
        [Range(18, 67, ErrorMessage = "Invalid {0}. Please provide value between {1} and {2}")]
        [Display(Name = "Current Age")]
        public int Age { get; set; }

        [Range(0, 100, ErrorMessage = "Invalid {0}. Please provide value between {1} and {2}")]
        [Display(Name = "Witholding Tax (%)")]
        public decimal TaxPercent { get; set; }

        public static EmployeeDataModel FromEmployee(Employee emp)
        {
            return Mapper.Map<EmployeeDataModel>(emp);
        }

        public Employee ToEmployee()
        {
            return Mapper.Map<Employee>(this);
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}; {nameof(Name)}: {Name}; {nameof(Age)}: {Age}; {nameof(TaxPercent)}: {TaxPercent}";
        }
    }
}
