using System;
using System.Collections.Generic;

namespace TaskManager.Infrastructure.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public short Funcao { get; set; }
    }
}
