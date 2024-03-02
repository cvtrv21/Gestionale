using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BiciclottiWpf
{
    /// <summary>
    /// Logica di interazione per clientWindow.xaml
    /// </summary>
    public partial class clientWindow : Window
    {
        private ListViewItem lastSelectedItem = null;

        List<Cliente> clienti = new List<Cliente>();

        private DispatcherTimer clockTimer;

        public clientWindow()
        {
            InitializeComponent();

            dataGridClients.ItemsSource = clienti;
            // Chiamare il metodo per alternare i colori delle righe
            GetAllClients();
            // Questo gestirà l'evento Window_Loaded quando la finestra viene caricata.
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
            dataGridClients.ItemsSource = clienti;
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Aggiorna l'orologio ogni secondo
            UpdateClock();
        }

        private void dataGridClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica se è stata selezionata una riga
            if (dataGridClients.SelectedItem != null)
            {
                // Ottieni la riga selezionata
                Cliente selectedClient = (Cliente)dataGridClients.SelectedItem;

                // Riempi le TextBox con i valori corrispondenti
                NomeTextBox.Text = selectedClient.NomeCliente;
                IndirizzoTextBox.Text = selectedClient.Indirizzo;
                TelefonoTextBox.Text = selectedClient.Telefono;
                EmailTextBox.Text = selectedClient.Email;
                

                // Abilita solo le TextBox Interessate
                NomeTextBox.IsEnabled = true;
                IndirizzoTextBox.IsEnabled = true;
                TelefonoTextBox.IsEnabled = true;
                EmailTextBox.IsEnabled = true;

            }
            else
            {
                // Nessuna riga selezionata, quindi tutte le TextBox sono abilitate
                NomeTextBox.IsEnabled = true;
                IndirizzoTextBox.IsEnabled = true;
                TelefonoTextBox.IsEnabled = true;
                EmailTextBox.IsEnabled = true;

                // Svuoto le TextBox con i valori corrispondenti
                NomeTextBox.Text = "";
                IndirizzoTextBox.Text = "";
                TelefonoTextBox.Text = "";
                EmailTextBox.Text = "";
                //isRowSelected = false;
            }
        }

        private void GetAllClients()
        {
            try
            {
                clienti = QueueUnitOfWorks.BiciclottiRepository.GetAllClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Button Aggiungi-Modifica-Elimina
        private void EliminaCliente_Click(object sender, RoutedEventArgs e)
        {
            //Ottengo l'elemento selezionato nella tabella
            Cliente? selectedClient = dataGridClients.SelectedItem as Cliente;

            if (selectedClient != null)
            {
                try
                {
                    QueueUnitOfWorks.BiciclottiRepository.DeleteClient(selectedClient.IdCliente);

                    //Rimuovo la bici dalla lista locale
                    clienti.Remove(selectedClient);

                    //Aggiorniamo la view
                    dataGridClients.Items.Refresh();
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante l'eliminazione della bicicletta: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Seleziona una bicicletta da eliminare.");
            }
        }

        private void AggiungiCliente_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Crea e mostra la finestra di aggiunta
            AddForm addBicycleWindow = new AddForm();
            // Imposta la finestra al centro dello schermo
            addBicycleWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            addBicycleWindow.ShowDialog();
            */
            // Raccogli i dati dal form
            string nome = NomeTextBox.Text;
            string indirizzo = IndirizzoTextBox.Text;
            string telefono = TelefonoTextBox.Text;
            string email = EmailTextBox.Text;


            // Conversione riuscita, ora puoi utilizzare il valore di codiceFornitore
            // Crea una nuova bicicletta
            Cliente nuovoCliente = new Cliente
                {
                    NomeCliente = nome,
                    Indirizzo = indirizzo,
                    Telefono = telefono,
                    Email = email,                    
                };

                try
                {
                    QueueUnitOfWorks.BiciclottiRepository.AddClient(nuovoCliente);
                    // Svuoto le TextBox con i valori corrispondenti
                    NomeTextBox.Text = "";
                    IndirizzoTextBox.Text = "";
                    TelefonoTextBox.Text = "";
                    EmailTextBox.Text = "";                 
                }
                catch
                { }
            
            

            // Dopo che la finestra di aggiunta viene chiusa, aggiorna la lista di biciclette nella DataGrid
            GetAllClients();
            dataGridClients.ItemsSource = null;
            dataGridClients.ItemsSource = clienti;

        }

        private void ModificaCliente_Click(object sender, RoutedEventArgs e)
        {
            // Raccogli i dati dal form
            string nome = NomeTextBox.Text;
            string indirizzo = IndirizzoTextBox.Text;
            string telefono = TelefonoTextBox.Text;
            string email = EmailTextBox.Text;

            ModificaCliente(nome, indirizzo, telefono, email);
        }
        #endregion
        private void ModificaCliente(string nome, string indirizzo, string telefono, string email)
        {
            if (dataGridClients.SelectedItem != null)
            {
                Cliente selectedClients = (Cliente)dataGridClients.SelectedItem;

                try
                {
                    // Aggiorna i prezzi dell'elemento selezionato
                    selectedClients.NomeCliente = nome;
                    selectedClients.Indirizzo = indirizzo;
                    selectedClients.Telefono = telefono;
                    selectedClients.Email = email;

                    // Esegui l'aggiornamento nel database o nella tua logica di business
                    QueueUnitOfWorks.BiciclottiRepository.UpdateClient(selectedClients);

                    // Aggiorna la visualizzazione della DataGrid
                    dataGridClients.Items.Refresh();

                    // Chiudi la finestra di modifica dei prezzi
                    // In questo punto, potresti anche chiudere la finestra o effettuare altre azioni necessarie
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante la modifica: {ex.Message}");
                }
                MessageBox.Show("Modifica eseguita");
            }
            else
            {
                MessageBox.Show("Nessun elemento selezionato per la modifica");
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
                dataGridClients.SelectedItem = null;
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
