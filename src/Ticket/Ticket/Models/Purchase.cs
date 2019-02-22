using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ticket.Models
{
    public class Purchase
    {
        [Key]
        public int PId { get; set; }

        [Required]
        public virtual Permit Type { get; set; }

        [Required]
        public virtual User UserName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public float Cost { get; set; }

        [Required]
        public DateTime PTime { get; set; }
    }
}