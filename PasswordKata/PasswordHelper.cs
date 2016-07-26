using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKata
{
    public static class PasswordHelper
    {

        public static string ValidPassword()
        {
            return "a1B28Kd38";
        }

        public static string AllAlphaPassword()
        {
            var invalidPassword = ValidPassword();

            for (var i = 0; i < 10; i++)
            {
                invalidPassword = invalidPassword.Replace(i.ToString(), "a");
            }

            return invalidPassword;
        }
    }
}
