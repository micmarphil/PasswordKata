using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKata
{
    public interface IRule
    {
        bool Validate(string password);
        string FailureMessage();
    }
}
