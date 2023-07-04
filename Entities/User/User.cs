using Entities.Common;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : IdentityUser<int>, IEntity
    {
     

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        public string NatinalCode { get; set; }

        public virtual ICollection<Address> MyAddresss { get; set; }

    }


}
