using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Models
{
    public interface ICharacterRepository<T>: IRepository<Character>
        where T:Character
    {
        IEnumerable<Character> GetByPlayerId(string playerId);
    }
}
