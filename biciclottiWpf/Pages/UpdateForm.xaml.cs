using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using BiciclottiWpf.Models.Utilities;
using System;
using System.Windows;


namespace BiciclottiWpf.Pages
{
    /// <summary>
    /// Logica di interazione per UpdateForm.xaml
    /// </summary>
    public partial class UpdateForm : Window
    {
        private Stock stockToUpdate;
        public UpdateForm(Stock selectedStock)
        {
            InitializeComponent();
            stockToUpdate = selectedStock;

            // Imposta i controlli della finestra con i valori dell'oggetto stockToUpdate
            // Ad esempio, puoi impostare il valore della quantità in un TextBox.
            QuantitaTextBox.Text = stockToUpdate.Quantita.ToString();
        }

        private void Conferma_Click(object sender, RoutedEventArgs e)
        {
            // Ottieni la nuova quantità dalla TextBox
            int nuovaQuantita;

            if (int.TryParse(QuantitaTextBox.Text, out nuovaQuantita))
            {
                // Conversione riuscita, modifica la quantità dell'elemento selezionato
                if (stockToUpdate != null)
                {
                    try
                    {
                        // Aggiorna la quantità dell'elemento selezionato
                        stockToUpdate.Quantita = nuovaQuantita;

                        // Esegui l'aggiornamento nel database o nella tua logica di business
                        QueueUnitOfWorks.BiciclottiRepository.UpdateStock(stockToUpdate);
                        

                        // Chiudi la finestra di modifica
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Errore durante la modifica della quantità: {ex.Message}");
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
                MessageBox.Show("Inserisci un valore valido per la quantità (numero intero).");
            }
        }

        private void Annulla_Click(object sender, RoutedEventArgs e)
        {
            // Chiudi la finestra di aggiunta senza aggiungere la bicicletta
            this.Close();
        }
    }
}
