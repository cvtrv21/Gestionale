using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using BiciclottiWpf.Pages;
using ClosedXML.Excel;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZXing;

namespace BiciclottiWpf
{
    /// <summary>
    /// Logica di interazione per StoricoOrdiniWindow.xaml
    /// </summary>
    public partial class StoricoOrdiniWindow : Window
    {
        private DispatcherTimer clockTimer;

        private DispatcherTimer miniTabellaTimer;

        public StoricoOrdiniWindow()
        {
            InitializeComponent();

            #region Timer Tabella Ordini Evasi
            // Inizializza il timer per l'aggiornamento della mini tabella
            miniTabellaTimer = new DispatcherTimer();
            miniTabellaTimer.Interval = TimeSpan.FromSeconds(0.4); // Imposta l'intervallo desiderato (ad esempio, 2 secondi)
            miniTabellaTimer.Tick += MiniTabellaTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            miniTabellaTimer.Start(); // Avvia il timer

            AggiornaMiniTabella();
            #endregion
            

            #region Timer Orologio
            // Inizializza il timer per l'orologio
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1); // Aggiorna ogni secondo
            clockTimer.Tick += ClockTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            clockTimer.Start(); // Avvia il timer

            // Chiamata al metodo per inizializzare l'orologio
            UpdateClock();
            #endregion

          

        }

        #region metodi Timer Tick
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Aggiorna l'orologio ogni secondo
            UpdateClock();
        }

       

        private void MiniTabellaTimer_Tick(object sender, EventArgs e)
        {
            // Chiamata al metodo per aggiornare la mini tabella
            AggiornaMiniTabella();
        }

      
        #endregion

        private void UpdateClock()
        {
            // Ottieni l'orario corrente
            DateTime now = DateTime.Now;

            // Aggiorna il testo dell'OrologioTextBlock
            OrologioTextBlock.Text = now.ToString("HH:mm:ss");
        }

        #region Tabella Ordini Evasi

        private void AggiornaMiniTabella()
        {
            // Ottieni solo gli ordini evasi
            List<OrderRow> ordiniEvasi = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks();

            // Filtra gli ordini evasi con stato "Evaso"
            var ordiniEvasiFiltrati = ordiniEvasi.Where(order => order.order.StatoOrdine == "Evaso").ToList();

            // Creare una lista di oggetti anonimi per la mini tabella
            var miniTabellaData = ordiniEvasiFiltrati
                 .Where(order => order.Bicycle != null)  // Assicurati che Bicycle non sia null
                 .Select(order => new
                 {
                     Cliente = order.order.NomeCliente,
                     CO = order.CodiceOrdine,
                     BikeID = order.BicycleId,
                     Marca = order.Bicycle.Marca,
                     Modello = order.Bicycle.Modello,
                     Taglia = order.Taglia,
                     Qta = order.Quantita,
                     Profitto = CalcolaProfitto(order),
                     StatoOrdine = order.order.StatoOrdine,
                 })
                 .Reverse()
                 .ToList();

            // Assegna la lista alla sorgente dati del DataGrid
            dataGridStoricoOrder.ItemsSource = miniTabellaData;

           
        }

        private float CalcolaProfitto(OrderRow order)
        {
            // Ottieni tutte le biciclette
            var tutteLeBiciclette = QueueUnitOfWorks.BiciclottiRepository.GetAllBicycles();

            // Cerca la bicicletta corrispondente
            var biciclettaCorrispondente = tutteLeBiciclette.FirstOrDefault(b => b.BicycleId == order.BicycleId);

            // Se la bicicletta è trovata, calcola il profitto
            if (biciclettaCorrispondente != null)
            {
                float profitto = biciclettaCorrispondente.PrezzoVendita * order.Quantita;
                return profitto;
            }

            // Restituisci 0 se la bicicletta non è trovata
            return 0;
        }

        private float CalcolaSommaProfitti()
        {
            // Ottieni solo gli ordini evasi
            List<OrderRow> ordiniEvasi = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks();

            // Filtra gli ordini evasi con stato "Evaso"
            var ordiniEvasiFiltrati = ordiniEvasi.Where(order => order.order.StatoOrdine == "Evaso").ToList();

            // Calcola la somma totale dei profitti
            float sommaProfitti = ordiniEvasiFiltrati.Sum(order => CalcolaProfitto(order));

            return sommaProfitti;
        }

        #endregion
    }
}
