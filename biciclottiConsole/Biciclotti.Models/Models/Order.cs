using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Biciclotti.Models
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

        public Order(int codiceOrdine, string dataConsegna, string nomeCliente, string statoOrdine)
        {
            CodiceOrdine = codiceOrdine;
            DataConsegna = dataConsegna;
            NomeCliente = nomeCliente;
            StatoOrdine = statoOrdine;
        }

        public virtual ICollection<OrderRow>? OrderRow { get; set; }
    }
}
