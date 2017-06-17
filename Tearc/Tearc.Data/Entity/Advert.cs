using System;
using System.Collections.Generic;
using System.Text;

namespace Tearc.Data.Entity
{
    public class Advert : BaseEntity
    {
        public string ExternalID { get; set; }
        public Source Source { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public List<string> ImageLinks { get; set; }
        public ProductType ProductType { get; set; }
        public int HashCode { get; set; }
        public int Rating { get; set; }
    }
}
