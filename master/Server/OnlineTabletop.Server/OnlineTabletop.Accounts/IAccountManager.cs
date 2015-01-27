using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Accounts
{
    public interface IAccountManager<DbAccount>
    {
        DbAccount FindAccountByEmail(string email);
    }
}
