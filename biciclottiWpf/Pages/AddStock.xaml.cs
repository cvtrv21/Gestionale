using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace BiciclottiWpf.Pages
{
    /// <summary>
    /// Logica di interazione per AddStock.xaml
    /// </summary>
    public partial class AddStock : Window
    {
        List<Stock> stocks = new List<Stock>();
        List<Bicycle> bicycles = new List<Bicycle>();

        Bicycle? selectedStock = null;

        private void dataGridBicycles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStock = (Bicycle)dataGridBicycles.SelectedItem;
        }

        public AddStock()
        {
            InitializeComponent();
            GetAllBicycle();
            // Questo gestirà l'evento Window_Loaded quando la finestra viene caricata.
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridBicycles.ItemsSource = bicycles;
        }

        private void GetAllBicycle()
        {
            try
            {
                bicycles = QueueUnitOfWorks.BiciclottiRepository.GetAllBicycles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AggiungiStock_Click(object sender, RoutedEventArgs e)
        {            
            // Raccogli i dati dal form            
            int taglia;
            int quantita;
            int bikeId = selectedStock.BicycleId;

            if (int.TryParse(TagliaTextBox.Text, out taglia) && int.TryParse(QuantitaTextBox.Text, out quantita))
            {
                // Conversione riuscita
                // Crea un nuovo stock
                Stock nuovoStock = new Stock
                {
                    BicycleId = bikeId,
                    Taglia = taglia,
                    Quantita = quantita
                };

                try
                {
                    QueueUnitOfWorks.BiciclottiRepository.AddStock(nuovoStock);
                }
                catch {}

                // Chiudi la finestra di aggiunta
                this.Close();
            }
            else
            {
                // La conversione non è riuscita, gestisci l'errore (ad esempio, mostra un messaggio all'utente)
                MessageBox.Show("Inserisci un valore valido per la taglia o per la quantità (numero intero).");
            }
        }
              

        private void AnnullaButton_Click(object sender, RoutedEventArgs e)
        {
            // Chiudi la finestra di aggiunta senza aggiungere la bicicletta
            this.Close();
        }

       
    }
}

