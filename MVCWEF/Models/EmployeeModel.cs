using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCWEF.Models
{
    public class EmployeeModel
    {
        public int emp_ID { get; set; }
        [DisplayName("Employee Name")]
        public string emp_Name { get; set; }
        public string emp_Salary { get; set; }
        public string CreateOn { get; set; }
    }
}