using Entities.Common;
using Entities.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities.weblog 
{
    public class Weblog : BaseEntity
    {
        [Required]
        [Display(Name = "خلاصه وبلاگ")]
        public string websummery { get; set; }

        [Required]
        [Display(Name = "متن وبلاگ")]
        public string webdescription { get; set; }

        [Required]
        [Display(Name = "آدرس تصویر")]
        public string urlProduct { get; set; }

        [Required]
        [Display(Name = "دسته بندی محصول")]
        public int categoryId { get; set; }




        [ForeignKey(nameof(categoryId))]
        public ProductCategory? productCategory { get; set; }



        [NotMapped]
        public IFormFile UploadFiles { get; set; }


        //public virtual ICollection<ProductCategory>? productCategories { get; set; }

    }
}
