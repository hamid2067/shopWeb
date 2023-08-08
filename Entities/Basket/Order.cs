using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Product;
using Microsoft.CodeAnalysis;

namespace Entities.Basket
{
    public class Order : BaseEntity
    {


        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Mobile { get; set; }

        public string OffCode { get; set; }

        //[ForeignKey(nameof(productId))]
        //public Product? product { get; set; }

        public int SumPrice { get; set; }

        public int Off { get; set; }

        public int PayPrice { get; set; }

        public int OrderState { get; set; }



        public int userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User? user { get; set; }



    }
}
