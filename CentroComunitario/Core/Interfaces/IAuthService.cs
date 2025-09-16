using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        bool Login(string username, string password);
    }
}
