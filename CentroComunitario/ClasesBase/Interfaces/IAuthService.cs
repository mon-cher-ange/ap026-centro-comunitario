using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase.Interfaces
{
    public interface IAuthService
    {
        bool Login(string username, string password);
    }
}
