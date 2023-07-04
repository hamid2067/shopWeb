using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class Post:BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

       

    }

   
}
