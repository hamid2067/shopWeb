using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Address: BaseEntity
    {
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Location { get; set; }
        public string PostalCode { get; set; }
        public string PhoneReciver { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User Person { get; set; }

    }
}
