﻿using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities.Product
{
    public class Product: BaseEntity
    {
        [Required]
        [Display(Name ="نام محصول")]
        public string productName { get; set; }


        [Display(Name = "توضیحات محصول")]
        [AllowHtml]
        public string? productDescription { get; set; }


        [Required]
        [Display(Name = "خلاصه توضیحات محصول")]
        public string? productSummery { get; set; }

       
        [Display(Name = "محصول ویژه")]
        public bool IsSpecial { get; set; }


        [Required]
        [Display(Name = "دسته بندی محصول")]
        public int categoryId { get; set; }




        [ForeignKey(nameof(categoryId))]
        public ProductCategory? productCategory { get; set; }




        public virtual ICollection<imageProduct>? Images { get; set; }

        public virtual ICollection<ProductSize>? sizes { get; set; }

        public virtual ICollection<ProductColor>? colors { get; set; }
        public virtual ICollection<PIP>? pips { get; set; }

        [NotMapped]
        public int selectSize { get; set; }

        [NotMapped]
        public int selectColor { get; set; }

    }
}
