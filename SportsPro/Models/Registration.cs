using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SportsPro.Models
{
    public class Registration 
    {
        
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public string ActiveCustomer { get; set; } 

        public int? ProductID { get; set; }
        

        public int? CustomerID { get; set; }
                                                        
    }
}
