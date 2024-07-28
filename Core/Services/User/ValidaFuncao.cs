using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Enums;

namespace TaskManager.Core.Services.User
{
    public class ValidaFuncao
    {
        public static bool IsAdmin(UserFuncaoEnum userFuncao) 
        { 
            if (userFuncao == UserFuncaoEnum.Admin)
                return true;
            else 
                return false;
        }
    }
}
