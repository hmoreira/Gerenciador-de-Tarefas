namespace TaskManager.Infrastructure.Models
{
    public class Usuario
    {
        public Usuario()
        {
            TarefaUsuarioCriadores = new HashSet<Tarefa>();
            TarefaUsuarioResponsaveis = new HashSet<Tarefa>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Username { get; set; } = null!;
        public short Funcao { get; set; }
        public byte[] Senha { get; set; } = null!;

        public virtual ICollection<Tarefa> TarefaUsuarioCriadores { get; set; }
        public virtual ICollection<Tarefa> TarefaUsuarioResponsaveis { get; set; }
    }
}
