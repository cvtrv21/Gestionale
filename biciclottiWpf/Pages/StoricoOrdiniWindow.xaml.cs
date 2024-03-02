using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using BiciclottiWpf.Pages;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        List<OrderRow> orderList = new List<OrderRow>();

        private DispatcherTimer clockTimer;

        public StoricoOrdiniWindow()
        {
            InitializeComponent();
            GetOrders();
            Loaded += Window_Loaded;

            // Inizializza il timer per l'orologio
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1); // Aggiorna ogni secondo
            clockTimer.Tick += ClockTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            clockTimer.Start(); // Avvia il timer

            // Chiamata al metodo per inizializzare l'orologio
            UpdateClock();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //OrdiniListView.ItemsSource = orderList;

            orderList.Reverse();

            OrdiniEvasiListView.ItemsSource = orderList;

        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Aggiorna l'orologio ogni secondo
            UpdateClock();
        }

        
        private void GetOrders()
        {
            try
            {
                List<OrderRow> temp = new List<OrderRow>();

                temp = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks();

                orderList = temp.Where(order => order.order.StatoOrdine == "Evaso").ToList();

                // Creazione della lista filtrata
                var ListaFiltrata = orderList
                    .Where(order => order.Bicycle != null)
                    .Select(order => new
                    {
                        Cliente = order.order.NomeCliente,
                        CO = order.CodiceOrdine,
                        BikeID = order.BicycleId,
                        Marca = order.Bicycle.Marca,
                        Modello = order.Bicycle.Modello,
                        Taglia = order.Taglia,
                        Qta = order.Quantita,
                        Guadagni = CalcolaProfitto(order),
                    })
                    .Reverse()
                    .ToList();

                // Aggiorna la ListView con la lista filtrata
                OrdiniEvasiListView.ItemsSource = ListaFiltrata;

                

                OrdiniEvasiListView.Items.Refresh();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void UpdateClock()
        {
            // Ottieni l'orario corrente
            DateTime now = DateTime.Now;

            // Aggiorna il testo dell'OrologioTextBlock
            OrologioTextBlock.Text = now.ToString("HH:mm:ss");
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

    }
}
