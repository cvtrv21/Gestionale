using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiciclottiWpf.Models.Utilities
{
    public class BicycleStock
    {
        public int BicycleId { get; set; }



        public int StockId { get; set; }



        public string? Marca { get; set; }



        public string? Modello { get; set; }



        public int CodiceFornitore { get; set; }



        public int Taglia { get; set; }



        public int Quantita { get; set; }



        public BicycleStock() { }



        public BicycleStock(int BicycleId, int StockId, string Marca, string Modello, int CodiceFornitore, int Taglia, int quantita)
        {
            this.BicycleId = BicycleId;
            this.StockId = StockId;
            this.Marca = Marca;
            this.Modello = Modello;
            this.CodiceFornitore = CodiceFornitore;
            this.Taglia = Taglia;
            this.Quantita = quantita;
        }
    }
}
