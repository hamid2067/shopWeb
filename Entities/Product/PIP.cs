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
    public class PIP : BaseEntity
    {
        [Required]
        [Display(Name ="موجودی")]
        public double invoice { get; set; }


        [Required]
        public int sizeId { get; set; }


        [Required]
        public int colorId { get; set; }



        [Required]
        public int ProductId { get; set; }


        [ForeignKey(nameof(sizeId))]
        public ProductSize ProductSize { get; set; }


        [ForeignKey(nameof(colorId))]
        public ProductColor ProductColor { get; set; }
    }
}
