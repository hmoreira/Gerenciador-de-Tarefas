using TaskManager.Core.Enums;

namespace TaskManager.API.DTO
{
    public class TarefaBase
    {        
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public DateTime DataVencimento { get; set; }
        public TarefaStatusEnum Status { get; set; }
        public int UsuarioResponsavelId { get; set; }
        public int UsuarioCriadorId { get; set; }
    }
}
