using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Menu
{
    public class Menu: BaseEntity
    {
        public string NameMenu { get; set; }
        public string NameIcon { get; set; }

        public string? PageAddress { get; set; }

        public int ParentId { get; set; }


    }
}
