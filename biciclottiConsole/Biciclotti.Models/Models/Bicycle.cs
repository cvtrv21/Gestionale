using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biciclotti.Models
{
    [Table(name: "Bicycle")]
    public class Bicycle
    {
      
        [Key]
        public int BicycleId { get; set; }

      
        [Required]
        public string Marca { get; set; } = "";

     
        [Required]
        public string Modello { get; set; } = "";

     
        [Required]
        public int CodiceFornitore { get; set; }


        public float PrezzoAcquisto { get; set; }

        public float PrezzoVendita { get; set; }

        public Bicycle() { }

        public Bicycle(int bicycleId, string marca, string modello, int codiceFornitore)
        {
            BicycleId = bicycleId;
            Marca = marca;
            Modello = modello;
            CodiceFornitore = codiceFornitore;
        }

        public virtual ICollection<Stock>? Stocks { get; set; }
    }

}