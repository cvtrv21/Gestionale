
using Biciclotti.Models.Utilities;
using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using BiciclottiWpf.Models.Utilities;
using BiciclottiWpf.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Word = Microsoft.Office.Interop.Word;
using ZXing;
using System.Windows.Media.Imaging;
using System.IO;
using ZXing.QrCode;
using System.Windows.Interop;
using System.Drawing;
using System.Windows.Media;
using ClosedXML.Excel;
using System.Windows.Data;
using System.Windows.Threading;


namespace BiciclottiWpf
{

    public partial class OrdersWindow : Window
    {
        
        List<OrderRow> orderList = new List<OrderRow>();

        private DispatcherTimer clockTimer;

        public OrdersWindow()
        {
            InitializeComponent();
            GetOrders();
            Loaded += Window_Loaded;

            // Inizializza il timer per l'orologio
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(0.4); // Aggiorna ogni secondo
            clockTimer.Tick += ClockTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            clockTimer.Start(); // Avvia il timer

            // Chiamata al metodo per inizializzare l'orologio
            UpdateClock();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //OrdiniListView.ItemsSource = orderList;

            orderList.Reverse();
            OrdiniListView.ItemsSource = orderList;

            StatoComboBox.SelectedIndex = 1;
            FiltraOrdini_Click(null, null);
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
                orderList = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks();
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

        #region Filtri

        private void FiltraOrdini_Click(object sender, RoutedEventArgs e)
        {

            string statoFiltrato = ((ComboBoxItem)StatoComboBox.SelectedItem).Content.ToString();

            List<OrderRow> ordiniFiltrati = new List<OrderRow>();

            if (statoFiltrato == "Tutti")
            {
                // Se "Tutti" è selezionato, visualizza tutti gli ordini
                ordiniFiltrati = orderList;
            }
            else
            {
                // Filtra gli ordini in base allo stato selezionato
                ordiniFiltrati = orderList.Where(order => order.order.StatoOrdine == statoFiltrato).ToList();
            }
            // Aggiorna la vista con gli ordini filtrati
            OrdiniListView.ItemsSource = null;
            OrdiniListView.ItemsSource = ordiniFiltrati;
        }

        private void FiltraPerData_Click(object sender, RoutedEventArgs e)
        {
            if (DataConsegnaDatePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = DataConsegnaDatePicker.SelectedDate.Value;
                string selectedDateString = selectedDate.ToString("dd/MM/yy");

                // Filtra gli ordini per la data selezionata
                List<OrderRow> ordiniFiltrati = orderList.Where(order => order.order.DataConsegna == selectedDateString).ToList();

                // Aggiorna la ListView con gli ordini filtrati
                OrdiniListView.ItemsSource = ordiniFiltrati;
            }
        }

        private void RimuoviFiltro_Click(object sender, RoutedEventArgs e)
        {
            // Rimuovi il filtro per la data
            DataConsegnaDatePicker.SelectedDate = null;

            // Applica il filtro per lo StatoOrdine attuale
            FiltraOrdini_Click(sender, e);
        }

        private void RimuoviFiltroStato_Click(object sender, RoutedEventArgs e)
        {
            // Reimposta la selezione dello StatoComboBox su "Tutti"
            StatoComboBox.SelectedIndex = 0;

            // Applica il filtro per lo StatoOrdine attuale
            FiltraOrdini_Click(sender, e);
        }
        #endregion

        #region Button Aggiungi-Modifica-Elimina
        private void NuovoOrdine_Click(object sender, RoutedEventArgs e)
        {
            // Crea e mostra la finestra di aggiunta
            AddOrders addOrderWindow = new AddOrders();
            addOrderWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addOrderWindow.ShowDialog();

            GetOrders();
            OrdiniListView.ItemsSource = null;
            orderList.Reverse();
            OrdiniListView.ItemsSource = orderList;
        }

        private void ModificaStato_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;

            // Assicurati che sia stata selezionata una riga nella vista OrdiniListView
            if (OrdiniListView.SelectedItem != null)
            {
                // Recupera l'elemento selezionato dalla lista
                OrderRow selectedOrder = (OrderRow)OrdiniListView.SelectedItem;

                if (selectedOrder.order.StatoOrdine == "Evaso")
                {
                    MessageBox.Show("Non è possibile evadere un ordine già Evaso");
                }
                else
                {
                    try
                    {
                        success = false;

                        // Chiama il metodo CambioStatoOrdine passando l'ID dell'ordine
                        success = QueueUnitOfWorks.BiciclottiRepository.CambioStatoOrdine(selectedOrder.CodiceOrdine);

                        if (success)
                        {
                            // Aggiorna la vista degli ordini dopo aver cambiato lo stato solo se l'operazione ha avuto successo
                            Thread.Sleep(200);
                            
                            //OrdiniListView.Items.Refresh();

                            GetOrders();
                            orderList.Reverse();
                            OrdiniListView.ItemsSource = orderList;

                        }
                        else
                        {
                            MessageBox.Show("Impossibile cambiare lo stato dell'ordine. Giacenza in magazzino insufficiente");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore durante la modifica dello stato dell'ordine: " + ex.Message);
                    }
                }
            }
            else
            {
                // Avvisa l'utente di selezionare un ordine
                MessageBox.Show("Seleziona un ordine dalla lista prima di modificarne lo stato.");
            }
        }

        private void EliminaOrdine_Click(object sender, RoutedEventArgs e)
        {
            //Recupero l'elemento selezionato dalla lista
            OrderRow selectedOrder = (OrderRow)OrdiniListView.SelectedItem;

            if (selectedOrder != null)
            {
                try
                {
                    QueueUnitOfWorks.BiciclottiRepository.DeleteOrderAndOrderRow(selectedOrder.CodiceOrdine);


                    //Rimuovo l'ordine dalla lista
                    orderList.Remove(selectedOrder);

                    //aggiorno la view
                    OrdiniListView.Items.Refresh();
                }

                catch { }
            }
        }
        #endregion

        #region Creazione Etichetta
        private void CreaEtichetta_Click(object sender, RoutedEventArgs e)
        {
            // Assicurati che sia stato selezionato un ordine nella vista OrdiniListView
            if (OrdiniListView.SelectedItem != null)
            {
                // Recupera l'elemento selezionato dalla lista
                OrderRow selectedOrder = (OrderRow)OrdiniListView.SelectedItem;

                // Controlla se l'etichetta è già stata creata
                if (!selectedOrder.IsLabelCreated)
                {
                    // Aggiungi qui la tua logica specifica per creare l'etichetta
                    CreareEtichetta(selectedOrder);

                    // Imposta la proprietà IsLabelCreated su true per indicare che l'etichetta è stata creata
                    selectedOrder.IsLabelCreated = true;

                    MessageBox.Show($"Etichetta creata per l'ordine {selectedOrder.CodiceOrdine}");
                }
                else
                {
                    MessageBox.Show("L'etichetta è già stata creata per questo ordine.");
                }
            }
            else
            {
                // Avvisa l'utente di selezionare un ordine
                MessageBox.Show("Seleziona un ordine dalla lista prima di creare l'etichetta.");
            }
        }

        private void CreareEtichetta(OrderRow orderRow)
        {
            // Creazione di un'applicazione Word
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = true;

            // Creazione di un nuovo documento Word
            Word.Document doc = wordApp.Documents.Add();

            // Aggiungi il nome dell'azienda in grassetto all'inizio del documento
            AggiungiTestoGrassetto(doc, "Nome dell'Azienda");

            // Aggiungi il campo per indicare il giorno di creazione dell'etichetta
            AggiungiParagrafo(doc, $"Etichettato: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");

            // Aggiungi le informazioni dell'ordine al documento
            AggiungiParagrafo(doc, $"Codice Ordine: {orderRow.CodiceOrdine}");
            AggiungiParagrafo(doc, $"ID Bicicletta: {orderRow.BicycleId}");
            AggiungiParagrafo(doc, $"Taglia: {orderRow.Taglia}");
            AggiungiParagrafo(doc, $"Data di Consegna: {orderRow.order.DataConsegna}");
            AggiungiParagrafo(doc, $"Quantità: {orderRow.Quantita}");
            AggiungiParagrafo(doc, $"Nome Cliente: {orderRow.order.NomeCliente}");
            // Genera il codice QR come immagine BitmapSource
            BitmapSource qrBitmap = GeneraCodiceQR(orderRow);
            // Aggiungi il codice QR al documento
            AggiungiImmagine(doc, qrBitmap);

            AggiungiParagrafo(doc, "-----------------------------------------------------------------------------------------------------------------------------------------------");

            

            // Salva il documento in un percorso specifico o lascia che l'utente lo salvi
            // doc.SaveAs("percorso/del/file.docx");

            // Rilascio delle risorse
            Marshal.ReleaseComObject(doc);
            Marshal.ReleaseComObject(wordApp);
        }

        private void AggiungiParagrafo(Word.Document doc, string testo)
        {
            Word.Paragraph paragrafo = doc.Paragraphs.Add();
            paragrafo.Range.Text = testo;
            paragrafo.Range.InsertParagraphAfter();
        }

        private void AggiungiLineaVuota(Word.Document doc)
        {
            Word.Paragraph paragrafo = doc.Paragraphs.Add();
            paragrafo.Range.Text = string.Empty;
            paragrafo.Range.InsertParagraphAfter();
        }

        private void AggiungiTestoGrassetto(Word.Document doc, string testo)
        {
            Word.Paragraph paragrafo = doc.Paragraphs.Add();
            Word.Range range = paragrafo.Range;
            range.Bold = 1; // Imposta il testo in grassetto
            range.Text = testo;
            paragrafo.Range.InsertParagraphAfter();
        }

        private BitmapSource GeneraCodiceQR(OrderRow orderRow)
        {
            // Estrai tutte le informazioni necessarie dall'oggetto OrderRow
            string qrCodeData = $"{orderRow.CodiceOrdine}\n" +
                                $"ID Bicicletta: {orderRow.BicycleId}\n" +
                                $"Taglia: {orderRow.Taglia}\n" +
                                $"Data di Consegna: {orderRow.order.DataConsegna}\n" +
                                $"Quantità: {orderRow.Quantita}\n" +
                                $"Nome Cliente: {orderRow.order.NomeCliente}";

            // Creazione di un'istanza del generatore di codici QR
            BarcodeWriter<ZXing.QrCode.QrCodeEncodingOptions> barcodeWriter = new BarcodeWriter<ZXing.QrCode.QrCodeEncodingOptions>();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            // Configurazione delle opzioni del codice QR
            barcodeWriter.Options = new ZXing.QrCode.QrCodeEncodingOptions
            {
                Width = 150,
                Height = 150
            };

            // Generazione del codice QR come BitMatrix
            ZXing.Common.BitMatrix bitMatrix = barcodeWriter.Encode(qrCodeData);

            // Converti BitMatrix in BitmapSource
            BitmapSource bitmapSource = ConvertBitMatrixToBitmapSource(bitMatrix);

            // Restituisci il BitmapSource generato
            return bitmapSource;
        }

        private BitmapSource ConvertBitMatrixToBitmapSource(ZXing.Common.BitMatrix bitMatrix)
        {
            int width = bitMatrix.Width;
            int height = bitMatrix.Height;

            WriteableBitmap bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Gray8, null);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bitmap.WritePixels(new Int32Rect(x, y, 1, 1),
                                       bitMatrix[x, y] ? new byte[] { 0xFF } : new byte[] { 0 },
                                       1, 0);
                }
            }

            return bitmap;
        }

        private void AggiungiImmagine(Word.Document doc, BitmapSource image)
        {
            // Salva l'immagine in un file temporaneo
            string tempImagePath = Path.GetTempFileName().Replace(".tmp", ".png");

            using (var fileStream = new FileStream(tempImagePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }

            // Aggiungi una nuova riga vuota
            AggiungiLineaVuota(doc);

            // Aggiungi l'immagine come un nuovo paragrafo
            AggiungiParagrafo(doc, ""); // Aggiungi una riga vuota
            Word.Paragraph paragrafo = doc.Paragraphs.Add();
            Word.Range range = paragrafo.Range;
            range.InlineShapes.AddPicture(tempImagePath);

            // Elimina il file temporaneo dopo aver aggiunto l'immagine al documento
            File.Delete(tempImagePath);
        }
        #endregion

        #region Metodi Salvataggio Excel
        private void EsportaSuExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EsportaDatiExcel();
                MessageBox.Show($"Esportazione Completata");
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni durante l'esportazione
                MessageBox.Show($"Si è verificato un errore durante l'esportazione su Excel: {ex.Message}", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EsportaDatiExcel()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Ordini");

            // Aggiungi intestazioni delle colonne
            GridView gridView = OrdiniListView.View as GridView;
            if (gridView != null)
            {
                int colonnaAttuale = 1;

                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    var column = gridView.Columns[i];
                    var header = column.Header?.ToString();

                    // Verifica se l'header è nullo
                    if (!string.IsNullOrEmpty(header) && !string.Equals(header, "HeaderMancante", StringComparison.OrdinalIgnoreCase))
                    {
                        var cell = worksheet.Cell(1, colonnaAttuale);
                        cell.Value = header;
                        cell.Style.Fill.BackgroundColor = XLColor.LightGreen; // Imposta il colore di sfondo dell'header
                        worksheet.Column(i + 1).Width = column.ActualWidth / 5;
                        colonnaAttuale++;
                    }
                }
            }

            // Popola i dati dalla ListView al foglio di lavoro
            for (int i = 0; i < OrdiniListView.Items.Count; i++)
            {
                var item = OrdiniListView.Items[i] as OrderRow;

                if (item != null)
                {
                    int colonnaAttuale = 1;

                    for (int j = 0; j < gridView.Columns.Count; j++)
                    {
                        var column = gridView.Columns[j];
                        var header = column.Header?.ToString();
                        var valuePath = (column.DisplayMemberBinding as Binding)?.Path.Path;

                        // Verifica se l'header è nullo
                        if (!string.IsNullOrEmpty(header) && !string.Equals(header, "HeaderMancante", StringComparison.OrdinalIgnoreCase))
                        {
                            object cellValue;

                            if (valuePath == "order.DataConsegna")
                            {
                                cellValue = typeof(Order).GetProperty("DataConsegna")?.GetValue(item.order, null);
                            }
                            else if (valuePath == "order.NomeCliente")
                            {
                                cellValue = typeof(Order).GetProperty("NomeCliente")?.GetValue(item.order, null);
                            }
                            else if (valuePath == "order.StatoOrdine")
                            {
                                cellValue = typeof(Order).GetProperty("StatoOrdine")?.GetValue(item.order, null);
                            }
                            else
                            {
                                cellValue = typeof(OrderRow).GetProperty(valuePath)?.GetValue(item, null);
                            }

                            var cell = worksheet.Cell(i + 2, colonnaAttuale);
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
        #endregion

    }

}


