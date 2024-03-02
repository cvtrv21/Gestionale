using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiciclottiWpf.Models
{
    public class OrderRow
    {
        [Key]
        public int OrderRowId { get; set; }

        
        [Required]
        public int BicycleId { get; set; }

       
        [Required]
        [MaxLength(50)]
        public int Taglia { get; set; }

      
        [Required]
        public int Quantita { get; set; }

        [ForeignKey("BicycleId")]
        public Bicycle Bicycle { get; set; }

        [Required]
        public int CodiceOrdine { get; set; }

        [ForeignKey("CodiceOrdine")]
        public virtual Order order { get; set; }

        public OrderRow() { }

        public bool IsLabelCreated { get; set; }


    }

}
