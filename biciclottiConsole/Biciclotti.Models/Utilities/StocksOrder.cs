

namespace Biciclotti.Models.Utilities
{
    public class StocksOrder
    {
        public int CodiceOrdine { get; set; }
        public string DataConsegna { get; set; } = "";
        public string? NomeCliente { get; set; }
        public string StatoOrdine { get; set; } = "";
        public int BicycleId { get; set; }
        public int Taglia { get; set; }
        public int Quantita { get; set; }
        public StocksOrder() { }
        public StocksOrder(int CodiceOrdine, string DataConsegna, string NomeCliente, string StatoOrdine, int BicycleID, int Taglia, int quantita)
        {
            this.CodiceOrdine = CodiceOrdine;
            this.DataConsegna = DataConsegna;
            this.NomeCliente = NomeCliente;
            this.StatoOrdine = StatoOrdine;
            this.BicycleId = BicycleID;
            this.Taglia = Taglia;
            this.Quantita = quantita;
        }
    }
}
