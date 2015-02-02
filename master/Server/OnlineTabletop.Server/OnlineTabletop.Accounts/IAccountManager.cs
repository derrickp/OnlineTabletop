using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Accounts
{
    public interface IAccountManager<Account>
    {
        Account FindAccountByEmail(string email);

        Account FindAccountByName(string accountName);

        bool VerifyLogin(string name, string password);

        Account Add(RegisterBindingModel account);
    }
}
