using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project3_Code.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "UserName must field")]
        [MaxLength(30, ErrorMessage = "UserName can't exceed 30 bits in length")]
        public string UserName { get; set; }

        [MaxLength(20, ErrorMessage = "GName can't exceed 20 bits in length")]
        public string GName { get; set; }

        [MaxLength(20, ErrorMessage = "SName can't exceed 20 bits in length")]
        public string SName { get; set; }

        [MaxLength(40, ErrorMessage = "Address can't exceed 40 bits in length")]
        public string Address { get; set; }

        [MaxLength(20, ErrorMessage = "State can't exceed 20 bits in length")]
        public string State { get; set; }

        [MaxLength(4, ErrorMessage = "PostCode can't exceed 4 bits in length")]
        public string PostCode { get; set; }

        [MaxLength(10, ErrorMessage = "Mobile can't exceed 10 bits in length")]
        public string Mobile { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}