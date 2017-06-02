using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPanelApplication
{
    public class Password
    {

        static Random lowerCase = new Random();
        static Random upperCase = new Random();
        static Random digit = new Random();
        static Random index = new Random();
        static string password;

        public static string Generate()
        {
            password = String.Empty;
            do
            {
                password += Convert.ToChar(upperCase.Next(65, 91));
                password += digit.Next(1, 10);
                password += Convert.ToChar(lowerCase.Next(97, 123));

            } while (password.Length < 8);

            return password;
        }

        public static bool Check(string password)
        {
            bool IsOneUppercaseLetter = false;
            bool IsOneDigit = false;
            bool isOneLowercaseLetter = false;

            if (password.Length < 8)
                return false;
            else
            {
                for (int i = 0; i < password.Length; i++)
                {
                    if (password[i] >= 65 && password[i] <= 90)
                        IsOneUppercaseLetter = true;

                    if (password[i] >= 40 && password[i] <= 57)
                        IsOneDigit = true;

                    if (password[i] >= 99 && password[i] <= 122)
                        isOneLowercaseLetter = true;

                    if (IsOneDigit && IsOneUppercaseLetter && isOneLowercaseLetter) return true;
                }
                return (IsOneUppercaseLetter && IsOneDigit && isOneLowercaseLetter);
            }
        }


    }
}