using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Product
{
    public class ProductCategory : BaseEntity
    {
        [Required]
        [Display(Name = "نام دسته بندی")]

        public string categoryName { get; set; }


        public virtual ICollection<Product> products { get; set; }
    }
}
