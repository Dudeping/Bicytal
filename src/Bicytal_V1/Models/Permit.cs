using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bicytal.Models
{
    public class Permit
    {
        [Key]
        [MaxLength(10)]
        public string Type { get; set; }
        
        [Required()]
        [MaxLength(200)]
        public string Description { get; set; }
        
        [Required()]
        public float Quarter_price { get; set; }
        
        [Required()]
        public float HalfYear_price { get; set; }
        
        [Required()]
        public float Year_price { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}