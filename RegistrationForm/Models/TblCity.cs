using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationForm.Models
{
    public class TblCity
    {
        public int Id { get; set; }
        public int? StateId { get; set; }
        public string CityName { get; set; }
    }
}
