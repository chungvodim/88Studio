using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tearc.Data.Entity
{
    //[Table("ProductTypes")]
    public class ProductType
    {
        public string Name { get; set; }
    }
}
