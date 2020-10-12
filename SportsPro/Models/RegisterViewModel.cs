using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SportsPro.Models
{
    public class RegisterViewModel
    {

        public List<Customer> Customers { get; set; }

        public List<Registration> Registrations { get; set; }
        public List<Product> Products { get; set; }
        public Product CurrentProduct { get; set; }
        public string ActiveCustomer { get; set; }
        public Customer CurrentCustomer { get; set; }

        public int? CustomerID { get; set; }

        public int? ProductID { get; set; }







    }
}
