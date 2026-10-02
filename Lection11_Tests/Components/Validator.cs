using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Components
{
    public static class Validator
    {
        public static bool IsValid(string email)
        {
            //проверяем не пустая ли строка и нет ли в ней только пробелов
            if(string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            //нет ли в емейле пробелов
            if(email.Contains(" "))
            {
                return false;
            }
            //находим индекс символа @
            int dogIndex = email.IndexOf('@');
            if(dogIndex <=0 || dogIndex == email.Length -1)
            {
                return false;
            }
            //находим индекс точки после @
            int dotIndex = email.IndexOf('.', dogIndex);
            //проверяем что точка стоит после @, не раньше и не позже определенного места
            //и что она не на последней позиции
            return dotIndex > dogIndex + 1 && dotIndex < email.Length - 1;
        }
    }
}
