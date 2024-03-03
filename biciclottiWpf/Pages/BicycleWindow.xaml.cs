using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using BiciclottiWpf.Models.Utilities;
using BiciclottiWpf.Pages;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BiciclottiWpf
{
    /// <summary>
    /// Logica di interazione per BicycleWindow.xaml
    /// </summary>
    public partial class BicycleWindow : Window
    {
        private ListViewItem lastSelectedItem = null;
        List<Bicycle> bicycles = new List<Bicycle>();

        private DispatcherTimer clockTimer;

        public BicycleWindow()
        {
            InitializeComponent();
            GetAllBicycle();
            // Questo gestirà l'evento Window_Loaded quando la finestra viene caricata.
            Loaded += Window_Loaded;

            // Inizializza il timer per l'orologio
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(0.3); // Aggiorna ogni secondo
            clockTimer.Tick += ClockTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            clockTimer.Start(); // Avvia il timer

            // Chiamata al metodo per inizializzare l'orologio
            UpdateClock();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridBicycles.ItemsSource = bicycles;
            // Chiamare il metodo per alternare i colori delle righe
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Aggiorna l'orologio ogni secondo
            UpdateClock();
        }

        private void dataGridBicycles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica se è stata selezionata una riga
            if (dataGridBicycles.SelectedItem != null)
            {
                // Ottieni la riga selezionata
                Bicycle selectedStock = (Bicycle)dataGridBicycles.SelectedItem;

                // Riempi le TextBox con i valori corrispondenti
                MarcaTextBox.Text = selectedStock.Marca;
                ModelloTextBox.Text = selectedStock.Modello;
                CodiceFornitoreTextBox.Text = selectedStock.CodiceFornitore.ToString();
                PrezzoAcquistoTextBox.Text = selectedStock.PrezzoAcquisto.ToString();
                PrezzoVenditaTextBox.Text = selectedStock.PrezzoVendita.ToString();

                // Abilita solo le TextBox di PrezzoAcquisto e PrezzoVendita
                MarcaTextBox.IsEnabled = false;
                ModelloTextBox.IsEnabled = false;
                CodiceFornitoreTextBox.IsEnabled = false;
                PrezzoAcquistoTextBox.IsEnabled = true;
                PrezzoVenditaTextBox.IsEnabled = true;

                //isRowSelected = true;
            }
            else
            {
                // Nessuna riga selezionata, quindi tutte le TextBox sono abilitate
                MarcaTextBox.IsEnabled = true;
                ModelloTextBox.IsEnabled = true;
                CodiceFornitoreTextBox.IsEnabled = true;
                PrezzoAcquistoTextBox.IsEnabled = true;
                PrezzoVenditaTextBox.IsEnabled = true;

                // Svuoto le TextBox con i valori corrispondenti
                MarcaTextBox.Text = "";
                ModelloTextBox.Text = "";
                CodiceFornitoreTextBox.Text = "";
                PrezzoAcquistoTextBox.Text = "";
                PrezzoVenditaTextBox.Text = "";
                //isRowSelected = false;
            }
        }

        private void GetAllBicycle()
        {
            try
            {
                bicycles = QueueUnitOfWorks.BiciclottiRepository.GetAllBicycles();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Button Aggiungi-Modifica-Elimina
        private void EliminaBici_Click(object sender, RoutedEventArgs e)
        {
            //Ottengo l'elemento selezionato nella tabella
            Bicycle? selectedBike = dataGridBicycles.SelectedItem as Bicycle;

            if(selectedBike != null)
            {
                try
                {
                    QueueUnitOfWorks.BiciclottiRepository.DeleteBicycle(selectedBike.BicycleId);

                    //Rimuovo la bici dalla lista locale
                    bicycles.Remove(selectedBike);

                    //Aggiorniamo la view
                    dataGridBicycles.Items.Refresh();
                }

                catch(Exception ex)
                {
                    MessageBox.Show($"Errore durante l'eliminazione della bicicletta: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Seleziona una bicicletta da eliminare.");
            }
        }       

        private void AggiungiBici_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Crea e mostra la finestra di aggiunta
            AddForm addBicycleWindow = new AddForm();
            // Imposta la finestra al centro dello schermo
            addBicycleWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            addBicycleWindow.ShowDialog();
            */
            // Raccogli i dati dal form
            string marca = MarcaTextBox.Text;
            string modello = ModelloTextBox.Text;
            int codiceFornitore;
            float prezzoAcquisto;
            float prezzoVendita;

            if (int.TryParse(CodiceFornitoreTextBox.Text, out codiceFornitore) && float.TryParse(PrezzoAcquistoTextBox.Text, out prezzoAcquisto) && float.TryParse(PrezzoVenditaTextBox.Text, out prezzoVendita))
            {
                // Conversione riuscita, ora puoi utilizzare il valore di codiceFornitore
                // Crea una nuova bicicletta
                Bicycle nuovaBicicletta = new Bicycle
                {
                    Marca = marca,
                    Modello = modello,
                    CodiceFornitore = codiceFornitore,
                    PrezzoAcquisto = prezzoAcquisto,
                    PrezzoVendita = prezzoVendita
                };

                try
                {
                    QueueUnitOfWorks.BiciclottiRepository.AddBicycle(nuovaBicicletta);
                    // Svuoto le TextBox con i valori corrispondenti
                    MarcaTextBox.Text = "";
                    ModelloTextBox.Text = "";
                    CodiceFornitoreTextBox.Text = "";
                    PrezzoAcquistoTextBox.Text = "";
                    PrezzoVenditaTextBox.Text = "";
                }
                catch
                {}
            }
            else
            {
                // La conversione non è riuscita, gestisci l'errore (ad esempio, mostra un messaggio all'utente)
                MessageBox.Show("Inserisci un valore valido per il codice fornitore (numero intero).");
            }

            // Dopo che la finestra di aggiunta viene chiusa, aggiorna la lista di biciclette nella DataGrid
            GetAllBicycle();
            dataGridBicycles.ItemsSource = null;
            dataGridBicycles.ItemsSource = bicycles;

        }

        private void ModificaPrezzi_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Ottieni l'elemento selezionato nella tabella
            //Stock selectedStock = dataGridStocks.SelectedItem as Stock;
            Bicycle selectedBicycle = dataGridBicycles.SelectedItem as Bicycle;

            if (selectedBicycle != null)
            {
                //Ottengo l'oggetto Stock corrispondente

                //Stock selectedStock = GetStockFromSelectedBicycleStock(selectedBicycleStock);

                //Crea e mostra la finestra di modifica, passando l'elemento selezionato
                UpdateBicycleForm updateBikeWindow = new UpdateBicycleForm(selectedBicycle);
                updateBikeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                updateBikeWindow.ShowDialog();

                // Dopo che la finestra di aggiunta viene chiusa, aggiorna la lista di biciclette nella DataGrid
                GetAllBicycle();
                dataGridBicycles.ItemsSource = null;                
                dataGridBicycles.ItemsSource = bicycles;
            }
            else
            {
                MessageBox.Show("Seleziona una Bicicletta da modificare.");
            }
            */
            // Ottieni i nuovi prezzi dalla TextBox
            if (float.TryParse(PrezzoAcquistoTextBox.Text, out float nuovoPrezzoAcquisto) && float.TryParse(PrezzoVenditaTextBox.Text, out float nuovoPrezzoVendita))
            {
                // Chiamata al metodo per la modifica dei prezzi
                ModificaPrezzi(nuovoPrezzoAcquisto, nuovoPrezzoVendita);
            }
            else
            {
                // La conversione non è riuscita, gestisci l'errore (ad esempio, mostra un messaggio all'utente)
                MessageBox.Show("Inserisci un valore valido per i prezzi.");
            }
        }
        #endregion
        private void ModificaPrezzi(float nuovoPrezzoAcquisto, float nuovoPrezzoVendita)
        {
            if (dataGridBicycles.SelectedItem != null)
            {
                Bicycle selectedBicycle = (Bicycle)dataGridBicycles.SelectedItem;

                try
                {
                    // Aggiorna i prezzi dell'elemento selezionato
                    selectedBicycle.PrezzoAcquisto = nuovoPrezzoAcquisto;
                    selectedBicycle.PrezzoVendita = nuovoPrezzoVendita;

                    // Esegui l'aggiornamento nel database o nella tua logica di business
                    QueueUnitOfWorks.BiciclottiRepository.UpdateBicycle(selectedBicycle);

                    // Aggiorna la visualizzazione della DataGrid
                    dataGridBicycles.Items.Refresh();

                    // Chiudi la finestra di modifica dei prezzi
                    // In questo punto, potresti anche chiudere la finestra o effettuare altre azioni necessarie
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante la modifica dei prezzi: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Nessun elemento selezionato per la modifica dei prezzi.");
            }
        }

        private void dataGridBicycles_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Ottieni l'elemento visivo su cui è stato fatto clic
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            // Cerca l'elemento ListViewItem genitore (riga) più vicino
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep is ListViewItem item)
            {
                if (item == lastSelectedItem)
                {
                    // Se l'elemento su cui è stato fatto clic è la stessa riga già selezionata, deseleziona
                    item.IsSelected = false;
                    lastSelectedItem = null;
                }
                else
                {
                    // Se questa è una nuova selezione, deseleziona l'ultima riga selezionata (se presente)
                    if (lastSelectedItem != null)
                    {
                        lastSelectedItem.IsSelected = false;
                    }
                    lastSelectedItem = item;
                }
            }
            else
            {
                // Se è stato fatto clic fuori dalla tabella, deseleziona tutto
                dataGridBicycles.SelectedItem = null;
                lastSelectedItem = null;
            }
        }

        private void UpdateClock()
        {
            // Ottieni l'orario corrente
            DateTime now = DateTime.Now;

            // Aggiorna il testo dell'OrologioTextBlock
            OrologioTextBlock.Text = now.ToString("HH:mm:ss");
        }
    }

}
