using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bicytal.Models
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        
        [MaxLength(20)]
        public string GName { get; set; }
        
        [MaxLength(20)]
        public string SName { get; set; }
        
        [MaxLength(40)]
        public string Address { get; set; }
        
        [MaxLength(20)]
        public string State { get; set; }
        
        [MaxLength(4)]
        public string PostCode { get; set; }
        
        [MaxLength(10)]
        public string Mobile { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}