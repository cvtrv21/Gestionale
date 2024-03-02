using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using System.Windows;


namespace BiciclottiWpf.Pages
{
    /// <summary>
    /// Logica di interazione per AddForm.xaml
    /// </summary>
    public partial class AddForm : Window
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void ConfermaButton_Click(object sender, RoutedEventArgs e)
        {
            // Raccogli i dati dal form
            string marca = MarcaTextBox.Text;
            string modello = ModelloTextBox.Text;
            int codiceFornitore;

            if (int.TryParse(CodiceFornitoreTextBox.Text, out codiceFornitore))
            {
                // Conversione riuscita, ora puoi utilizzare il valore di codiceFornitore
                // Crea una nuova bicicletta
                Bicycle nuovaBicicletta = new Bicycle
                {
                    Marca = marca,
                    Modello = modello,
                    CodiceFornitore = codiceFornitore
                };

                try
                {
                    QueueUnitOfWorks.BiciclottiRepository.AddBicycle(nuovaBicicletta);
                    
                }
                catch
                {
                }

                // Chiudi la finestra di aggiunta
                this.Close();
            }
            else
            {
                // La conversione non è riuscita, gestisci l'errore (ad esempio, mostra un messaggio all'utente)
                MessageBox.Show("Inserisci un valore valido per il codice fornitore (numero intero).");
            }

        }
            private void AnnullaButton_Click(object sender, RoutedEventArgs e)
            {
                // Chiudi la finestra di aggiunta senza aggiungere la bicicletta
                this.Close();
            }

    }
    }

