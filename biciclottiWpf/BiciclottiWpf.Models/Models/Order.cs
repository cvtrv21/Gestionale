using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiciclottiWpf.Models
{
    public class Order
    {
      
        [Key]
        public int CodiceOrdine { get; set; }

        [Required]
        public string DataConsegna { get; set; } = "";

       
        [Required]
        public string NomeCliente { get; set; } = "";

        
        [Required]
        public string StatoOrdine { get; set; } = "";

        public Order() { }

        public Order(string dataConsegna, string nomeCliente, string statoOrdine)
        {
            DataConsegna = dataConsegna;
            NomeCliente = nomeCliente;
            StatoOrdine = statoOrdine;
        }

        public virtual ICollection<OrderRow>? OrderRow { get; set; }
    }
}
