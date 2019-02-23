using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bicytal.Models
{
    public class TicketContext : DbContext
    {
        public TicketContext() : base("name=TicketContext")
        {
        }

        public System.Data.Entity.DbSet<Bicytal.Models.User> Users { get; set; }
        public System.Data.Entity.DbSet<Bicytal.Models.Permit> Permits { get; set; }
        public System.Data.Entity.DbSet<Bicytal.Models.Purchase> Purchases { get; set; }
    }
}
