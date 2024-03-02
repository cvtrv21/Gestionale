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
using System.Windows.Shapes;

namespace BiciclottiWpf.Pages
{
    /// <summary>
    /// Logica di interazione per UpdateBicycleForm.xaml
    /// </summary>
    public partial class UpdateBicycleForm : Window
    {
        private Bicycle bicycleToUpdate;

        public UpdateBicycleForm(Bicycle selectedBicycle)
        {
            InitializeComponent();
            bicycleToUpdate = selectedBicycle;

            PrezzoAcquistoTextBox.Text = bicycleToUpdate.PrezzoAcquisto.ToString();
            PrezzoVendita.Text = bicycleToUpdate.PrezzoVendita.ToString();
        }

        private void Conferma_Click(object sender, RoutedEventArgs e)
        {
            // Ottieni i nuovi prezzi dalla TextBox
            float prezzoAcquisto;
            float prezzoVendita;

            if (float.TryParse(PrezzoAcquistoTextBox.Text, out prezzoAcquisto) && float.TryParse(PrezzoVendita.Text, out prezzoVendita))
            {
                // Conversione riuscita, modifica la quantità dell'elemento selezionato
                if (bicycleToUpdate != null)
                {
                    try
                    {
                        // Aggiorna i prezzi dell'elemento selezionato
                        bicycleToUpdate.PrezzoAcquisto = prezzoAcquisto;
                        bicycleToUpdate.PrezzoVendita = prezzoVendita;

                        // Esegui l'aggiornamento nel database o nella tua logica di business
                        QueueUnitOfWorks.BiciclottiRepository.UpdateBicycle(bicycleToUpdate);


                        // Chiudi la finestra di modifica
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Errore durante la modifica dei prezzi: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Nessun elemento selezionato per la modifica.");
                }
            }
            else
            {
                // La conversione non è riuscita, gestisci l'errore (ad esempio, mostra un messaggio all'utente)
                MessageBox.Show("Inserisci un valore valido per i prezzi.");
            }
        }

        private void Annulla_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
