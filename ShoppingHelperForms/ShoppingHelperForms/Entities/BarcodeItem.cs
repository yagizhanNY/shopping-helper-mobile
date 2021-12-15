using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingHelperForms.Entities
{
    public class BarcodeItem
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
