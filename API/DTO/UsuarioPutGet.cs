namespace TaskManager.API.DTO
{
    public class UsuarioPutGet
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;        
        public short Funcao { get; set; }
    }
}
