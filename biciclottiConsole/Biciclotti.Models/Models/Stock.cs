using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.Models
{
    public class Stock
    {

        [Key]
        public int StockId { get; set; }

        [Required]
        public int BicycleId { get; set; }

        [Required]
        public int Taglia { get; set; }

        [Required]
        public int Quantita { get; set; }

        public float ValoreTotale
        {
            get
            {
                if (Bicycle != null && Quantita >= 0)
                {
                    return Bicycle.PrezzoVendita * Quantita;
                }
                else
                {
                    // Gestire il caso in cui Bicycle è null o Quantita è negativo
                    return 0; // o un altro valore predefinito appropriato
                }
            }
        }

        [ForeignKey("BicycleId")]
        public virtual Bicycle Bicycle { get; set; }

        public Stock() { }

        public Stock(int bicicleId, int taglia, int quantita)
        {
            BicycleId = bicicleId;
            Taglia = taglia;
            Quantita = quantita;
        }
    }
}