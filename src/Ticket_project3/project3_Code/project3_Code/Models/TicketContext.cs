using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project3_Code.Models
{
    public class TicketContext : DbContext
    {
        public TicketContext() : base("name=TicketContext")
        {
        }

        public System.Data.Entity.DbSet<project3_Code.Models.User> Users { get; set; }
        public System.Data.Entity.DbSet<project3_Code.Models.Permit> Permits { get; set; }
        public System.Data.Entity.DbSet<project3_Code.Models.Purchase> Purchases { get; set; }
    }
}
