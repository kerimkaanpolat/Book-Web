using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Book_Web.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;  
        public string Address { get; set; } = string.Empty;       
        public string City { get; set; } = string.Empty;          
        public string PostalCode { get; set; } = string.Empty;  
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>(); 
    }

}
