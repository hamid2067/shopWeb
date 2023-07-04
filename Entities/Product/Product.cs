using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Product
{
    public class Product: BaseEntity
    {
        [Required]
        [Display(Name ="نام محصول")]
        public string productName { get; set; }


        [Display(Name = "توضیحات محصول")]
        public string? productDescription { get; set; }


        [Required]
        [Display(Name = "خلاصه توضیحات محصول")]
        public string productSummery { get; set; }

       
        [Display(Name = "محصول ویژه")]
        public string IsSpecial { get; set; }

        public virtual ICollection<imageProduct> Images { get; set; }

    }
}
