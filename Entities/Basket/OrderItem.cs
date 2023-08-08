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
    public class OrderItem : BaseEntity
    {

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? order { get; set; }

        public int productId { get; set; }

       // [ForeignKey(nameof(productId))]
       // public Product? product { get; set; }


        public float Number { get; set; }

        public int Price { get; set; }
        public int sizeId { get; set; }

        [ForeignKey(nameof(sizeId))]
        public ProductSize? size { get; set; }


        public int colorId { get; set; }
        [ForeignKey(nameof(colorId))]
        public ProductColor? color { get; set; }

        public int userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User? user { get; set; }



    }
}
