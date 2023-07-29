using Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Product
{
    public class imageProduct : BaseEntity
    {

        public int productId { get; set; }


        [ForeignKey(nameof(productId))]
        public Product? product { get; set; }

   

        [Required]
        [Display(Name ="آدرس تصویر")]
        public string urlProduct { get; set; }



        [Required]
        [Display(Name = "عکس مشخص")]
        public bool IsFirst { get; set; }

        [NotMapped]
        public IFormFile UploadFiles { get; set; }
    }
}
