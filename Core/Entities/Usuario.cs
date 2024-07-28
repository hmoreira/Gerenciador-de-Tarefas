using TaskManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Entities
{
    public class Usuario 
    {
        public Usuario(string nome, UserFuncaoEnum funcao, string username, string senha)
        {
            Nome = nome;
            Funcao = funcao;
            Senha = senha;
            Username = username;
        }

        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = null!;
        [Required]
        public UserFuncaoEnum Funcao { get; set; }        
        [MaxLength(50)]
        public string Username { get; set; } = null!;
        public string Senha { get; set; } = null!;        
    }
}
