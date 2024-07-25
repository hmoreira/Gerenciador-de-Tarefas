namespace TaskManager.Core.Entidades
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = null!;
        public List<Tarefa> Tarefas { get; set; } = null!;

    }
}
