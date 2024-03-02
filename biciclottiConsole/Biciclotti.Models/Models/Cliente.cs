using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        public string NomeCliente { get; set; } = "";

        public string Indirizzo { get; set; } = "";

        public string Telefono { get; set; } = "";

        public string Email { get; set; } = "";
        
    }
}
