using CustomerVet.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerVet.API.Data
{
    public class CustomerContext: DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options){ }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<ContactNumber> ContactNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        { 
        
        }
    }
}
