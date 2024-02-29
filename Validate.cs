using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    internal class Validate
    {
        public static bool IsEmailCorrect(string email)
        {
            if (email.Contains('@') && email.Contains('.') && email.Length > 5)
            {
                byte at = (byte)email.IndexOf('@');
                byte dot = (byte)email.IndexOf('.');

                if (at > 0 && dot > at + 1 && dot < email.Length - 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsEmailUnique(string email, List<User> users)
        {
            foreach (User user in users)
            {
                if (user.Login == email)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsCreate
    }
}