using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tearc.Data.Entity
{
    //[Table("Brands")]
    public class Brand
    {
        public string Name { get; set; }
    }
}
