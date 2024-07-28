namespace TaskManager.Infrastructure.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public DateTime DataVencimento { get; set; }
        public int UsuarioResponsavelId { get; set; }
        public int UsuarioCriadorId { get; set; }
        public short Status { get; set; }

        public virtual Usuario UsuarioCriador { get; set; } = null!;
        public virtual Usuario UsuarioResponsavel { get; set; } = null!;
    }
}
