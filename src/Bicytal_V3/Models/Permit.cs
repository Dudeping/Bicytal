using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bicytal.Models
{
    public class Permit
    {
        [Key]
        [MaxLength(10, ErrorMessage = "Type can't exceed 10 bits in length")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Type format is not correct.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Description must field")]
        [MaxLength(200, ErrorMessage = "Description can't exceed 200 bits in length")]
        [RegularExpression(@"^[a-zA-Z_,. ]+$", ErrorMessage = "Description format is not correct.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Quarter_price must field")]
        [DataType(DataType.Currency, ErrorMessage = "Quarter_price format is not correct.")]
        public float Quarter_price { get; set; }

        [Required(ErrorMessage = "HalfYear_price must field")]
        [DataType(DataType.Currency, ErrorMessage = "HalfYear_price format is not correct.")]
        public float HalfYear_price { get; set; }

        [Required(ErrorMessage = "Year_price must field")]
        [DataType(DataType.Currency, ErrorMessage = "Year_price format is not correct.")]
        public float Year_price { get; set; }

        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}