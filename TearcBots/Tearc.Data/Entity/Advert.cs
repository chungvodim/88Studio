using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tearc.Data.Entity
{
    //[Table("Adverts")]
    public class Advert : BaseEntity
    {
        public string ExternalID { get; set; } = Guid.NewGuid().ToString();
        public Source Source { get; set; } = new Source();
        public string URL { get; set; } = "https://vozforums.com/forumdisplay.php?f=68";
        public string Title { get; set; } = "Unknown Title";
        public Author Author { get; set; } = new Author();
        public string Content { get; set; } = "Unknown Content";
        public decimal Price { get; set; } = 1000000;
        public string Region { get; set; } = "TQ";
        public string Address { get; set; } = "82 Yet Kieu";
        public List<string> ImageLinks { get; set; } = new List<string>();
        public ProductType ProductType { get; set; } = new ProductType();
        public int HashCode { get; set; }
        public int Rating { get; set; }
    }
}
