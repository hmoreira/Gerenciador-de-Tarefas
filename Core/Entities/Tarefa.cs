using TaskManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Entities
{
    public class Tarefa 
    {
        public Tarefa(string titulo, string descricao, DateTime dataVencimento, TarefaStatusEnum status, int usuarioResponsavelId, int usuarioCriadorId)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataVencimento = dataVencimento;
            Status = status;
            UsuarioResponsavelId = usuarioResponsavelId;
            UsuarioCriadorId = usuarioCriadorId;
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; } = null!;
        [Required]
        public string Descricao { get; set; } = null!;
        [Required]        
        public DateTime DataVencimento { get; set; }
        [Required]
        public TarefaStatusEnum Status { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int UsuarioResponsavelId { get; set; }
        public Usuario UsuarioResponsavel { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue)]
        public int UsuarioCriadorId { get; set; }
        public Usuario UsuarioCriador { get; set; } = null!;
    }
}
