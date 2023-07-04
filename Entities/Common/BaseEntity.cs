using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Common
{

    public interface IEntity
    {

    }
    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
        public string? CreatedByUserId { get; set; }
        public string? ModifiedByUserId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string? Hash { get; set; }

    }

    public abstract class BaseEntity : BaseEntity<int>
    {

    }
}
