using TaskManager.Core.Enums;

namespace TaskManager.Core.Services.Tarefa
{
    public class ValidaPutDelete
    {
        public static bool Validacao(int usuarioLogado, UserFuncaoEnum funcao, int usuarioTarefa)
        {
            if (usuarioLogado == usuarioTarefa)
                return true;
            else if (funcao == UserFuncaoEnum.Admin)
                return true;
            else
                return false;
        }
    }
}
