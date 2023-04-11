using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obchis_3_C
{
    internal class TestClass
    {
        [RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "Name must start with a capital letter and not contain numbers")]
        public string Name { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
    }
}
