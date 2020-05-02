using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.Data.Domain
{
    public interface IUserManager
    {
        Task CreateManagerUser(string username, string password);
    }
}
