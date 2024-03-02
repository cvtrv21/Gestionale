using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using BiciclottiWpf.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BiciclottiWpf.Pages
{
    /// <summary>
    /// Logica di interazione per AddOrders.xaml
    /// </summary>
    public partial class AddOrders : Window
    {
        List<Stock> stockModel = new List<Stock>();

        Stock? selectedStock = null;

        private void dataGridBicycles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStock = (Stock)dataGridBicycles.SelectedItem;
        }

        public AddOrders()
        {
            InitializeComponent();
            GetStocks();
            // Questo gestirà l'evento Window_Loaded quando la finestra viene caricata.
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridBicycles.ItemsSource = stockModel;
        }

        private void GetStocks()
        {
            try
            {
                stockModel = QueueUnitOfWorks.BiciclottiRepository.GetAllBicycleStocks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AggiungiOrdine_Click(object sender, RoutedEventArgs e)
        {
            Order temp = new Order();

            string dataConsegna = DataConsegnaDatePicker.SelectedDate?.ToString("dd/MM/yy");

            // Raccogli i dati dal form            
            //string dataConsegna = DataTextBox.Text;
            string nomeCliente = ClienteTextBox.Text;
            int quantita;

            if (int.TryParse(QuantitaTextBox.Text, out quantita))
            {
                // Conversione riuscita
                // Crea un nuovo stock
                Order nuovoOrdine = new Order
                {
                    DataConsegna = dataConsegna,
                    NomeCliente = nomeCliente,
                    StatoOrdine = "Nuovo",
                };

               temp = QueueUnitOfWorks.BiciclottiRepository.AddOrder(nuovoOrdine);

                // Recupera l'ID dell'ordine appena creato
                int codiceOrdine = temp.CodiceOrdine;

                // Crea una nuova riga d'ordine (OrderRow)
                OrderRow nuovaOrderRow = new OrderRow
                {
                    BicycleId = selectedStock.BicycleId,
                    Taglia = selectedStock.Taglia,
                    Quantita = quantita,
                    CodiceOrdine = codiceOrdine,
                };

                // Aggiungi la nuova riga d'ordine al database
                QueueUnitOfWorks.BiciclottiRepository.AddOrderRow(nuovaOrderRow);

                // Chiudi la finestra di aggiunta
                this.Close();
            }
            else
            {
                // La conversione non è riuscita, gestisci l'errore (ad esempio, mostra un messaggio all'utente)
                MessageBox.Show("Inserisci un valore valido per la quantità (numero intero).");
            }
        }
        private void AnnullaButton_Click(object sender, RoutedEventArgs e)
        {
            // Chiudi la finestra di aggiunta senza aggiungere la bicicletta
            this.Close();
        }

    }
}

