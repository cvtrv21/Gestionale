using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using FrameQueues.Interfaces;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BiciclottiWpf.Pages
{
    /// <summary>
    /// Logica di interazione per UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {

        private ListViewItem lastSelectedItem = null;

        List<User> users = new List<User>();

        private DispatcherTimer clockTimer;

        public UserForm()
        {
            InitializeComponent();
            dataGridClients.ItemsSource = users;
            // Chiamare il metodo per alternare i colori delle righe
            GetAllUsers();
            // Questo gestirà l'evento Window_Loaded quando la finestra viene caricata.
            Loaded += Window_Loaded;

            // Inizializza il timer per l'orologio
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(0.2); // Aggiorna ogni secondo
            clockTimer.Tick += ClockTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            clockTimer.Start(); // Avvia il timer

            // Chiamata al metodo per inizializzare l'orologio
            UpdateClock();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridClients.ItemsSource = users;
        }

        private void UpdateClock()
        {
            // Ottieni l'orario corrente
            DateTime now = DateTime.Now;

            // Aggiorna il testo dell'OrologioTextBlock
            OrologioTextBlock.Text = now.ToString("HH:mm:ss");
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Aggiorna l'orologio ogni secondo
            UpdateClock();
        }

        private void GetAllUsers()
        {
            try
            {
                users = QueueUnitOfWorks.BiciclottiRepository.GetAllUsers()
                    .Select(user => new User
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        License = user.License,
                    })
                    .ToList();

                dataGridClients.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AggiungiUser_Click(object sender, RoutedEventArgs e)
        {
            string nome = NomeTextBox.Text;
            string password = PasswordTextBox.Text;
            string email = EmailTextBox.Text;
            string license = LicenseTextBox.Text;


            // Conversione riuscita, ora puoi utilizzare il valore di codiceFornitore
            // Crea una nuova bicicletta
            User nuovoUser = new User
            {
                Name = nome,
                Password = password,
                Email = email,
                License = license,
            };

            try
            {
                QueueUnitOfWorks.BiciclottiRepository.AddUser(nuovoUser);
                // Svuoto le TextBox con i valori corrispondenti
                NomeTextBox.Text = "";
                PasswordTextBox.Text = "";
                EmailTextBox.Text = "";
                LicenseTextBox.Text = "";
            }
            catch
            { }



            // Dopo che la finestra di aggiunta viene chiusa, aggiorna la lista di biciclette nella DataGrid
            GetAllUsers();
            dataGridClients.ItemsSource = null;
            dataGridClients.ItemsSource = users;
        }

        private void ModificaUser_Click(object sender, RoutedEventArgs e)
        {
            // Raccogli i dati dal form
            string nome = NomeTextBox.Text;
            string password = PasswordTextBox.Text;
            string email = EmailTextBox.Text;
            string license = LicenseTextBox.Text;

            ModificaUtente(nome, password, email, license);
        }

        private void ModificaUtente(string nome, string password, string email, string license)
        {
            if (dataGridClients.SelectedItem != null)
            {
                User selectedUsers = (User)dataGridClients.SelectedItem;

                try
                {
                    // Aggiorna i prezzi dell'elemento selezionato
                    selectedUsers.Name = nome;
                    selectedUsers.Password = password;
                    selectedUsers.Email = email;
                    selectedUsers.License = license;

                    // Esegui l'aggiornamento nel database o nella tua logica di business
                    QueueUnitOfWorks.BiciclottiRepository.UpdateUser(selectedUsers);

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

        private void EliminaUser_Click(object sender, RoutedEventArgs e)
        {
            //Ottengo l'elemento selezionato nella tabella
            User? selectedUser = dataGridClients.SelectedItem as User;

            if (selectedUser != null)
            {
                try
                {
                    QueueUnitOfWorks.BiciclottiRepository.DeleteUser(selectedUser.Id);

                    //Rimuovo la bici dalla lista locale
                    users.Remove(selectedUser);

                    //Aggiorniamo la view
                    dataGridClients.Items.Refresh();
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante l'eliminazione dell'Utente: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Seleziona un 'Utente' da eliminare.");
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

        private void dataGridClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica se è stata selezionata una riga
            if (dataGridClients.SelectedItem != null)
            {
                // Ottieni la riga selezionata
                User selectedUser = (User)dataGridClients.SelectedItem;

                // Riempi le TextBox con i valori corrispondenti
                NomeTextBox.Text = selectedUser.Name;
                PasswordTextBox.Text = selectedUser.Password;
                EmailTextBox.Text = selectedUser.Email;
                LicenseTextBox.Text = selectedUser.License;


                // Abilita solo le TextBox Interessate
                NomeTextBox.IsEnabled = true;
                PasswordTextBox.IsEnabled = true;
                EmailTextBox.IsEnabled = true;
                LicenseTextBox.IsEnabled = true;

            }
            else
            {
                // Nessuna riga selezionata, quindi tutte le TextBox sono abilitate
                NomeTextBox.IsEnabled = true;
                PasswordTextBox.IsEnabled = true;
                EmailTextBox.IsEnabled = true;
                LicenseTextBox.IsEnabled = true;

                // Svuoto le TextBox con i valori corrispondenti
                NomeTextBox.Text = "";
                PasswordTextBox.Text = "";
                EmailTextBox.Text = "";
                LicenseTextBox.Text = "";
                //isRowSelected = false;
            }
        }

    }
}
