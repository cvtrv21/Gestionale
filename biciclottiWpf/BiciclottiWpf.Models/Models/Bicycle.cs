using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiciclottiWpf.Models
{
    [Table(name: "Bicycle")]
    public class Bicycle
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int BicycleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Marca { get; set; } = "";

        [Required]
        public string Modello { get; set; } = "";

        [Required]
        public int CodiceFornitore { get; set; }

        public float PrezzoAcquisto { get; set; }

        public float PrezzoVendita { get; set; }

        public Bicycle() { }

        public virtual ICollection<Stock>? Stocks { get; set; }

    }

}