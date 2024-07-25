using TaskManager.Core.Enums;

namespace TaskManager.Core.Entidades
{
    public class Tarefa
    {
        public int TarefaId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public DateTime DataVencimento { get; set; }
        public TarefaStatusEnum Status { get; set; }
        public int UsuarioResponsavelId { get; set; }
        public Usuario UsuarioResponsavel { get; set; } = null!;
        public int UsuarioCriadorId { get; set; }
        public Usuario UsuarioCriador { get; set; } = null!;
    }
}
