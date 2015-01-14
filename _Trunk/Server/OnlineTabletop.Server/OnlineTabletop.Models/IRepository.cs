using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Models
{
    public interface IRepository<T>: ICollection<T>
        where T:IEntity
    {
        T Get(string id);
    }
}
