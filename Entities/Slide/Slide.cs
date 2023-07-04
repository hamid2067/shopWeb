using Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities.Slide
{
    public class Slide: BaseEntity
    {
        public string? subTitle { get; set; }

        [Required]
        [Display(Name = "عنوان")]
        public string title { get; set; }

        public string description { get; set; }

        public string? url { get; set; }

        public string imageUrl { get; set; }

        [NotMapped]
        public IFormFile UploadFiles { get; set; }

    }

}
