using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using BiciclottiWpf.Models.Utilities;
using BiciclottiWpf.Pages;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Threading;

namespace BiciclottiWpf
{
    /// <summary>
    /// Logica di interazione per StocksWindow.xaml
    /// </summary>
    public partial class StocksWindow : Window
    {
        List<Stock> stocksBikeList = new List<Stock>();

        private DispatcherTimer clockTimer;

        private List<Stock> stockFiltratiTaglia; // Variabile per memorizzare i risultati del filtro per marca
        private List<Stock> stockFiltratiMarca; // Variabile per memorizzare i risultati del filtro per marca
        private List<Stock> stockFiltratiModello; // Variabile per memorizzare i risultati del filtro per marca


        public StocksWindow()
        {
            InitializeComponent();
            GetAllStocks();

            // Questo gestirà l'evento Window_Loaded quando la finestra viene caricata.
            Loaded += Window_Loaded;

            Loaded += StocksWindow_Loaded;

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
            dataGridStocks.ItemsSource = stocksBikeList;
        }

        // Gestore per l'evento Loaded della finestra
        private void StocksWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Inizialmente, calcola e visualizza la somma dei valori
            CalcolaEVisualizzaSommaValori();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Aggiorna l'orologio ogni secondo
            UpdateClock();
        }

        // Metodo per calcolare la somma dei valori
        private double CalcolaSommaValori()
        {
            double sommaValori = 0.0;

            foreach (Stock stock in stocksBikeList)
            {
                sommaValori += stock.ValoreTotale;
            }

            return sommaValori;
        }

        // Metodo per calcolare e visualizzare la somma dei valori
        private void CalcolaEVisualizzaSommaValori()
        {
            double sommaValori = CalcolaSommaValori();
            SumTextBox.Text = sommaValori.ToString("N") + " € "; // Supponendo che SommaTextBox sia il nome del tuo TextBox
        }

        private void GetAllStocks()
        {
            try
            {
                stocksBikeList = QueueUnitOfWorks.BiciclottiRepository.GetAllBicycleStocks();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Button Aggiungi-Elimina-Modifica

        private void EliminaGiacenza_Click(object sender, RoutedEventArgs e)
        {
            //Ottengo l'elemento selezionato nella tabella
            Stock selectedStock = dataGridStocks.SelectedItem as Stock;

            if (selectedStock != null)
            {
                try
                {
                    QueueUnitOfWorks.BiciclottiRepository.DeleteStock(selectedStock.StockId);
                    //Rimuovo la bici dalla lista locale
                    stocksBikeList.Remove(selectedStock);                    

                    //Aggiorniamo la view
                    dataGridStocks.Items.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante l'eliminazione dello Stock: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Seleziona uno Stock da eliminare.");
            }
        }       

        private void AggiungiGiacenza_Click(object sender, RoutedEventArgs e)
        {
            // Crea e mostra la finestra di aggiunta
            AddStock addStockWindow = new AddStock();
            addStockWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addStockWindow.ShowDialog();

            // Dopo che la finestra di aggiunta viene chiusa, aggiorna la lista di biciclette nella DataGrid
            GetAllStocks();
            dataGridStocks.ItemsSource = null;
            dataGridStocks.ItemsSource = stocksBikeList;

            // Calcola e visualizza nuovamente la somma dei valori
            CalcolaEVisualizzaSommaValori();
        }        

        private void ModificaGiacenza_Click(object sender, RoutedEventArgs e)
        {
            // Ottieni l'elemento selezionato nella tabella
            Stock selectedBicycleStock = dataGridStocks.SelectedItem as Stock;

            if (selectedBicycleStock != null)
            {
                // Crea e mostra la finestra di modifica, passando l'elemento selezionato
                UpdateForm updateFormWindow = new UpdateForm(selectedBicycleStock);
                updateFormWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                updateFormWindow.ShowDialog();

                // Dopo che la finestra di aggiunta viene chiusa, aggiorna la lista di biciclette nella DataGrid
                GetAllStocks();
                dataGridStocks.ItemsSource = null;
                dataGridStocks.ItemsSource = stocksBikeList;
            }
            else
            {
                MessageBox.Show("Seleziona uno Stock da modificare.");
            }
        }
        #endregion

        #region Filtri
        private void FiltraPerTaglia_Click(object sender, RoutedEventArgs e)
        {
            //Ottengo il nome della marca inserito dall'utente nel TextBox
            string tagliaDaFiltrare = TagliaTextBox.Text;

            if (!string.IsNullOrEmpty(tagliaDaFiltrare))
            {
                TagliaTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x80, 0xE4, 0xAB)); // Imposta il colore di sfondo a #FF80E4AB

                stockFiltratiTaglia = stocksBikeList
            .Where(stock => stock.Taglia.ToString().Equals(tagliaDaFiltrare, StringComparison.OrdinalIgnoreCase))
            .ToList();

                dataGridStocks.ItemsSource = stockFiltratiTaglia;
            }
            else
            {
                dataGridStocks.ItemsSource = stocksBikeList;
            }
        }

        private void RimuoviFiltroTaglia_Click(object sender, RoutedEventArgs e)
        {
            //Reimposto la ListView con tutti i miei Stock
            dataGridStocks.ItemsSource = stocksBikeList;
            TagliaTextBox.Text = "";
            TagliaTextBox.Background = Brushes.White;
        }

        private void FiltraPerMarca_Click(object sender, RoutedEventArgs e)
        {
            //Ottengo il nome della marca inserito dall'utente nel TextBox
            string marcaDaFiltrare = MarcaTextBox.Text;

            if(!string.IsNullOrEmpty(marcaDaFiltrare) )
            {
                MarcaTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x80, 0xE4, 0xAB)); // Imposta il colore di sfondo a #FF80E4AB

                stockFiltratiMarca = stocksBikeList
                    .Where(stock => stock.Bicycle.Marca.Equals(marcaDaFiltrare, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                dataGridStocks.ItemsSource = stockFiltratiMarca;
            }
            else
            {
                dataGridStocks.ItemsSource = stocksBikeList;
            }
        }

        private void RimuoviFiltroMarca_Click(object sender, RoutedEventArgs e)
        {
            //Reimposto la ListView con tutti i miei Stock
            dataGridStocks.ItemsSource = stocksBikeList;
            MarcaTextBox.Text = "";
            MarcaTextBox.Background = Brushes.White;
            /*
            // Reimposto la ListView con i risultati del filtro per marca se esistono, altrimenti con l'elenco completo
            if (stockFiltratiMarca != null && stockFiltratiMarca.Any())
            {
                dataGridStocks.ItemsSource = stockFiltratiMarca;
            }
            else
            {
                dataGridStocks.ItemsSource = stocksBikeList;
            }
            MarcaTextBox.Text = "";
            */
        }

        private void FiltraPerModello_Click(object sender, RoutedEventArgs e)
        {
            string modelloDaFiltrare = ModelloTextBox.Text;

            if(!string.IsNullOrEmpty (modelloDaFiltrare) ) 
            {
                MarcaTextBox.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x80, 0xE4, 0xAB)); // Imposta il colore di sfondo a #FF80E4AB

                stockFiltratiModello = stocksBikeList
                    .Where(stock => stock.Bicycle.Modello.Equals(modelloDaFiltrare, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                dataGridStocks.ItemsSource = stockFiltratiModello ?? stocksBikeList;
            }
            else
            {
                dataGridStocks.ItemsSource = stocksBikeList;
            }
        }

        private void RimuoviFiltroModello_Click(object sender, RoutedEventArgs e)
        {
            //Reimposto la ListView con tutti i miei Stock
            dataGridStocks.ItemsSource = stocksBikeList;
            ModelloTextBox.Text = "";
            MarcaTextBox.Background = Brushes.White;
        }
        #endregion

        private void EsportaStockExcelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EsportaDatiExcelStock();
                MessageBox.Show($"Esportazione Completata");
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni durante l'esportazione
                MessageBox.Show($"Si è verificato un errore durante l'esportazione su Excel: {ex.Message}", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EsportaDatiExcelStock()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Stocks");

            // Aggiungi intestazioni delle colonne escludendo "Valore Stock"
            GridView gridView = dataGridStocks.View as GridView;
            if (gridView != null)
            {
                int colonnaAttuale = 1;

                foreach (var column in gridView.Columns)
                {
                    var header = column.Header?.ToString();

                    // Verifica se l'header è nullo e non è "Valore Stock"
                    if (!string.IsNullOrEmpty(header) && !string.Equals(header, "HeaderMancante", StringComparison.OrdinalIgnoreCase)
                        && !string.Equals(header, "Valore Stock", StringComparison.OrdinalIgnoreCase))
                    {
                        var cell = worksheet.Cell(1, colonnaAttuale);
                        cell.Value = header;
                        cell.Style.Fill.BackgroundColor = XLColor.LightGreen; // Imposta il colore di sfondo dell'header
                        worksheet.Column(colonnaAttuale).Width = column.ActualWidth / 5;
                        colonnaAttuale++;
                    }
                }
            }

            // Popola i dati dalla ListView al foglio di lavoro
            for (int i = 0; i < dataGridStocks.Items.Count; i++)
            {
                var item = dataGridStocks.Items[i] as Stock;

                if (item != null)
                {
                    int colonnaAttuale = 1;

                    foreach (var column in gridView.Columns)
                    {
                        var header = column.Header?.ToString();
                        var valuePath = (column.DisplayMemberBinding as Binding)?.Path.Path;

                        // Verifica se l'header è nullo e non è "Valore Stock"
                        if (!string.IsNullOrEmpty(header) && !string.Equals(header, "HeaderMancante", StringComparison.OrdinalIgnoreCase)
                            && !string.Equals(header, "Valore Stock", StringComparison.OrdinalIgnoreCase))
                        {
                            var cell = worksheet.Cell(i + 2, colonnaAttuale);
                            object cellValue = null;

                            if (!string.IsNullOrEmpty(valuePath))
                            {
                                // Gestisci il percorso della proprietà in base al tuo modello
                                if (valuePath.StartsWith("Bicycle."))
                                {
                                    // Se la proprietà appartiene a Bicycle, gestisci la navigazione delle proprietà
                                    var bicycleProperty = typeof(Bicycle).GetProperty(valuePath.Substring(8));
                                    cellValue = bicycleProperty?.GetValue(item.Bicycle, null);
                                }
                                else
                                {
                                    // Altrimenti, considera la proprietà direttamente dal modello Stock
                                    cellValue = typeof(Stock).GetProperty(valuePath)?.GetValue(item, null);
                                }
                            }

                            // Verifica se il valore è nullo prima di assegnarlo alla cella
                            cell.Value = cellValue?.ToString() ?? string.Empty;
                            colonnaAttuale++;
                        }
                    }
                }
            }

            // Salva il file Excel
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                workbook.SaveAs(saveFileDialog.FileName);
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
