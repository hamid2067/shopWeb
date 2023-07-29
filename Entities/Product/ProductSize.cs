﻿using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Product
{
    public class ProductSize : BaseEntity
    {
        [Required]
        [Display(Name ="نام سایز")]
        public string SizeName { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        public virtual ICollection<PIP>? pipes { get; set; }
    }
}
