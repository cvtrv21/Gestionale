using Biciclotti.Models.Utilities;
using BiciclottiWpf.Data;
using BiciclottiWpf.Models;
using BiciclottiWpf.Models.Utilities;
using BiciclottiWpf.Pages;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;


namespace BiciclottiWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        private DispatcherTimer miniTabellaTimer;

        private DispatcherTimer chartUpdateTimer;

        private DispatcherTimer ordiniDaEvadereTimer;

        private DispatcherTimer clockTimer;

        private DispatcherTimer clienteTopTimer;

        private int _numeroOrdiniDaEvadere;

        public int NumeroOrdiniDaEvadere
        {
            get { return _numeroOrdiniDaEvadere; }
            set
            {
                _numeroOrdiniDaEvadere = value;
                // Aggiorna l'interfaccia utente o esegui altre azioni necessarie quando il valore cambia
                // Ad esempio, puoi aggiornare un TextBlock o eseguire altre operazioni in base al valore
            }
        }

        private int _numeroOrdiniScaduti;

        public int NumeroOrdiniScaduti
        {
            get { return _numeroOrdiniScaduti; }
            set
            {
                _numeroOrdiniScaduti = value;
                // Aggiorna l'interfaccia utente o esegui altre azioni necessarie quando il valore cambia
                // Ad esempio, puoi aggiornare un TextBlock o eseguire altre operazioni in base al valore
            }
        }

        public ChartValues<float> QuantitaValues { get; set; } 
        public ChartValues<float> ProfittoValues { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Inizializza le ChartValues per le colonne Quantità e Profitto
            QuantitaValues = new ChartValues<float>();
            ProfittoValues = new ChartValues<float>();

            #region Timer Dati RealTime
            // Inizializza il timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5); // Imposta l'intervallo desiderato (ad esempio, 5 secondi)
            timer.Tick += Timer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            timer.Start(); // Avvia il timer

            // Esegui la prima verifica delle notifiche
            CheckNotifications();

             // Esegui la prima verifica delle giacenze all'avvio dell'applicazione
            CheckOrderStocks();
            #endregion

            #region Timer Tabella Ordini Evasi
            // Inizializza il timer per l'aggiornamento della mini tabella
            miniTabellaTimer = new DispatcherTimer();
            miniTabellaTimer.Interval = TimeSpan.FromSeconds(3); // Imposta l'intervallo desiderato (ad esempio, 2 secondi)
            miniTabellaTimer.Tick += MiniTabellaTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            miniTabellaTimer.Start(); // Avvia il timer
            #endregion

            #region Timer Grafico
            // Inizializza il timer per l'aggiornamento del grafico
            chartUpdateTimer = new DispatcherTimer();
            chartUpdateTimer.Interval = TimeSpan.FromSeconds(5); // Imposta l'intervallo desiderato (ad esempio, 10 secondi)
            chartUpdateTimer.Tick += ChartUpdateTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            chartUpdateTimer.Start(); // Avvia il timer
            #endregion

            #region Timer Ordini Da Evadere
            // Inizializza il timer per l'aggiornamento del grafico
            ordiniDaEvadereTimer = new DispatcherTimer();
            ordiniDaEvadereTimer.Interval = TimeSpan.FromSeconds(4); // Imposta l'intervallo desiderato (ad esempio, 10 secondi)
            ordiniDaEvadereTimer.Tick += OrdiniDaEvadereTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            ordiniDaEvadereTimer.Start(); // Avvia il timer
            // Chiamata a un metodo per aggiornare i dati del grafico
            ConfigureChart();
            #endregion

            #region Timer Orologio
            // Inizializza il timer per l'orologio
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1); // Aggiorna ogni secondo
            clockTimer.Tick += ClockTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            clockTimer.Start(); // Avvia il timer

            // Chiamata al metodo per inizializzare l'orologio
            UpdateClock();
            #endregion

            #region Timer Cliente TOP
            // Inizializza il timer per l'orologio
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(3); // Aggiorna ogni secondo
            clockTimer.Tick += ClienteTopTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            clockTimer.Start(); // Avvia il timer

            AggiornaTopCliente();
            #endregion

            #region Ordini Scaduti
            // Inizializza il timer per l'orologio
            clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(3); // Aggiorna ogni secondo
            clockTimer.Tick += OrdiniScadutiTimer_Tick; // Aggiungi un gestore per l'evento Tick del timer
            clockTimer.Start(); // Avvia il timer

            AggiornaNumeroOrdiniScaduti();
            #endregion

        }

        #region metodi Timer Tick
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Aggiorna l'orologio ogni secondo
            UpdateClock();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Chiamata alla tua funzione di verifica delle notifiche
            CheckNotifications();
            CheckOrderStocks();           
        }

        private void MiniTabellaTimer_Tick(object sender, EventArgs e)
        {
            // Chiamata al metodo per aggiornare la mini tabella
            AggiornaMiniTabella();
        }

        private void ChartUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Chiamata al metodo per aggiornare i dati del grafico
            UpdateChartData();
        }

        private void OrdiniDaEvadereTimer_Tick(object sender, EventArgs e)
        {
            AggiornaNumeroOrdiniDaEvadere();
        }

        private void ClienteTopTimer_Tick(object sender, EventArgs e)
        {
            AggiornaTopCliente();
        }

        private void OrdiniScadutiTimer_Tick(object sender, EventArgs e)
        {
            AggiornaNumeroOrdiniScaduti();
        }
        #endregion

        private void UpdateClock()
        {
            // Ottieni l'orario corrente
            DateTime now = DateTime.Now;

            // Aggiorna il testo dell'OrologioTextBlock
            OrologioTextBlock.Text = now.ToString("HH:mm:ss");
        }

        #region Terminale Messaggi Tempo Reale
        private void CheckNotifications()
        {
            List<Stock> stockList = new List<Stock>();

            // Ottengo tutte le giacenze
            stockList = QueueUnitOfWorks.BiciclottiRepository.GetAllStocks();

            //Filtriamo le giacenze con quantità inferiore a 2
            List<Stock> lowStock = new List<Stock>();
            foreach (var stock in stockList)
            {
                if (stock.Quantita <= 2)
                {
                    lowStock.Add(stock);
                }
            }

            // Ottieni la data e l'ora correnti
            string currentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            // Utilizza il Dispatcher per aggiornare l'interfaccia utente nel thread UI principale
            Application.Current.Dispatcher.Invoke(() =>
            {
                RealTimeMemo.Document.Blocks.Clear();

                // Aggiungi la data e l'ora corrente
                Paragraph dateTimeParagraph = new Paragraph(new Run("------------------------------------------------------------------------------------------------------------->> "+ currentTime));
                dateTimeParagraph.Foreground = Brushes.White;
                dateTimeParagraph.Margin = new Thickness(0,0,0,1);
                RealTimeMemo.Document.Blocks.Add(dateTimeParagraph);

                for (int i = 0; i < lowStock.Count; i++)
                {
                    var stock = lowStock[i];

                    // Ottieni il nome della bicicletta associata a questa giacenza
                    var bicycle = QueueUnitOfWorks.BiciclottiRepository.GetBicycle(stock.BicycleId);

                    // Aggiungi il messaggio con l'indice all'inizio
                    Paragraph paragraph = new Paragraph(new Run($"{i + 1}. ATTENZIONE! --->  {bicycle.Marca} - {bicycle.Modello} ha una giacenza minore o uguale a 2"));
                    paragraph.Foreground = Brushes.Green;

                    paragraph.Margin = new Thickness(0, 0, 0, 1);

                    RealTimeMemo.Document.Blocks.Add(paragraph);
                }

                // Scorri in basso la RichTextBox per visualizzare il testo appena aggiunto
                RealTimeMemo.ScrollToEnd();
            });
        }

        private void CheckOrderStocks()
        {
            List<OrderRow> newOrders = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks();
            List<Stock> stockList = QueueUnitOfWorks.BiciclottiRepository.GetAllStocks();
            bool orderSatisfied = true;

            int unsatisfiedOrderCount = 0; // Variabile per tenere traccia del numero di ordini non soddisfatti

            foreach (var order in newOrders)
            {
                // Verifica se l'ordine è in stato "Nuovo"
                if (order.order.StatoOrdine == "Nuovo")
                {
                    var orderRows = QueueUnitOfWorks.BiciclottiRepository.GetOrderRow(order.CodiceOrdine);

                    foreach (var row in orderRows)
                    {
                        var stock = stockList.Find(s => s.BicycleId == row.BicycleId);
                        if (stock == null || stock.Quantita < row.Quantita)
                        {
                            orderSatisfied = false;
                            break;
                        }
                    }

                    if (!orderSatisfied)
                    {
                        foreach (var row in orderRows)
                        {
                            unsatisfiedOrderCount++; // Incrementa il contatore

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Paragraph paragraph = new Paragraph(new Run($"{unsatisfiedOrderCount}. ATTENZIONE! ---> L'ordine {order.CodiceOrdine} non è soddisfatto a causa di giacenze insufficienti"));
                                paragraph.Foreground = Brushes.Orange;

                                paragraph.Margin = new Thickness(0, 0, 0, 1);

                                RealTimeMemo.Document.Blocks.Add(paragraph);
                            });
                        }
                    }
                }
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!orderSatisfied && unsatisfiedOrderCount > 0)
                {
                    // Scorri in basso la RichTextBox per visualizzare il testo appena aggiunto
                    RealTimeMemo.ScrollToEnd();
                }
            });
        }
        #endregion

        #region Metodi navigazione Pagine
        private void OpenBicycleWindow_Click(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            BicycleWindow bicycleWindow = new BicycleWindow();

            bicycleWindow.Show(); //Mostra la finestra in modalità modale      
        }

        private void OpenStocksWindow_Click(object sender, RoutedEventArgs e)
        {
            StocksWindow stocksWindow = new StocksWindow();

            stocksWindow.Show();
        }

        private void OpenOrdersWindow_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow ordersWindow = new OrdersWindow();

            ordersWindow.Show();
        }

        private void OpenClientWindow_Click(object sender, RoutedEventArgs e)
        {
            clientWindow clientW = new clientWindow();

            clientW.Show();
        }

        private void OpenStoricoOrdiniWindow_Click(object sender, RoutedEventArgs e)
        {
            StoricoOrdiniWindow storicoOrdiniWindow = new StoricoOrdiniWindow();

            storicoOrdiniWindow.Show();

        }
        #endregion

        #region Barcode
        private void SimulateBarcodeRead_Click(object sender, RoutedEventArgs e)
        {
            // Ottenere il valore della TextBox come simulazione del barcode
            string barcodeValue = BarcodeTextBox.Text;

            // Eseguire le operazioni desiderate utilizzando il valore simulato del barcode
            HandleBarcodeOperations(barcodeValue);           
        }

        private void HandleBarcodeOperations(string barcodeValue)
        {
            // Dividi il valore del barcode in base al separatore "||"
            string[] barcodeParts = barcodeValue.Split(new string[] { "||" }, StringSplitOptions.None);

            if (barcodeParts.Length == 3)
            {
                int taglia;
                int quantita;

                if (int.TryParse(barcodeParts[1], out taglia) &&
                    int.TryParse(barcodeParts[2], out quantita))
                {
                    try
                    {
                        // Crea una nuova giacenza con il BicycleID (sostituire con la tua logica per ottenere il BicycleID)
                        int bicycleId = QueueUnitOfWorks.BiciclottiRepository.GetBicycleIdFromCodiceFornitore(Convert.ToInt32(barcodeParts[0]));
                        Thread.Sleep(200);

                        if (bicycleId != 0)
                        {
                            Stock newStock = new Stock
                            {
                                BicycleId = bicycleId,
                                Taglia = taglia,
                                Quantita = quantita
                            };
                       
                            // Chiama il metodo per aggiungere la nuova giacenza
                            QueueUnitOfWorks.BiciclottiRepository.AddOrUpdateStock(newStock);                            

                            // Facoltativamente, puoi mostrare una conferma o eseguire altre azioni necessarie
                            MessageBox.Show("Giacenza aggiornata o aggiunta con successo.");

                            BarcodeTextBox.Text = "";
                        }
                        else
                        {
                            //MessageBox.Show("BicycleID non trovato per il CodiceFornitore specificato.");
                            MessageBoxResult result = MessageBox.Show("\"BicycleID non trovato per il CodiceFornitore specificato. \n Vuoi aggiungere una nuova Bicicletta nell'anagrafica?","Conferma", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                AddForm addBikeWindows = new AddForm();
                                addBikeWindows.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                addBikeWindows.ShowDialog();
                            }
                            else
                            {
                                BarcodeTextBox.Text = "";
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore durante l'aggiornamento o l'aggiunta della giacenza: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Dati non validi nel barcode.");
                }
            }
            else
            {
                MessageBox.Show("Formato del barcode non valido.");
            }
        }

        #endregion

        #region Tabella Ordini Evasi

        private void AggiornaMiniTabella()
        {
            // Ottieni solo gli ordini evasi
            List<OrderRow> ordiniEvasi = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks();

            // Filtra gli ordini evasi con stato "Evaso"
            var ordiniEvasiFiltrati = ordiniEvasi.Where(order => order.order.StatoOrdine == "Evaso").ToList();

            // Creare una lista di oggetti anonimi per la mini tabella
            var miniTabellaData = ordiniEvasiFiltrati
                 .Where(order => order.Bicycle != null)  // Assicurati che Bicycle non sia null
                 .Select(order => new
                 {
                     Cliente = order.order.NomeCliente,
                     CO = order.CodiceOrdine,
                     BikeID = order.BicycleId,
                     Marca = order.Bicycle.Marca,
                     Modello = order.Bicycle.Modello,
                     Taglia = order.Taglia,
                     Qta = order.Quantita,
                     Profitto = CalcolaProfitto(order),
                 })
                 .Reverse()
                 .ToList();

            // Assegna la lista alla sorgente dati del DataGrid
            MiniTable.ItemsSource = miniTabellaData;

            // Imposta la quantità totale di ordini evasi nel TextBlock
            QuantitaOrdiniEvasiRun.Text = $"{ordiniEvasiFiltrati.Count}";

            // Imposta la somma totale dei profitti nel TextBlock
            SommaProfittiRun.Text = $"{CalcolaSommaProfitti()} €";
        }

        private float CalcolaProfitto(OrderRow order)
        {
            // Ottieni tutte le biciclette
            var tutteLeBiciclette = QueueUnitOfWorks.BiciclottiRepository.GetAllBicycles();

            // Cerca la bicicletta corrispondente
            var biciclettaCorrispondente = tutteLeBiciclette.FirstOrDefault(b => b.BicycleId == order.BicycleId);

            // Se la bicicletta è trovata, calcola il profitto
            if (biciclettaCorrispondente != null)
            {
                float profitto = biciclettaCorrispondente.PrezzoVendita * order.Quantita;
                return profitto;
            }

            // Restituisci 0 se la bicicletta non è trovata
            return 0;
        }

        private float CalcolaSommaProfitti()
        {
            // Ottieni solo gli ordini evasi
            List<OrderRow> ordiniEvasi = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks();

            // Filtra gli ordini evasi con stato "Evaso"
            var ordiniEvasiFiltrati = ordiniEvasi.Where(order => order.order.StatoOrdine == "Evaso").ToList();

            // Calcola la somma totale dei profitti
            float sommaProfitti = ordiniEvasiFiltrati.Sum(order => CalcolaProfitto(order));

            return sommaProfitti;
        }

        #endregion

        #region Grafico

        private void ConfigureChart()
        {
            // Aggiungi le Serie al grafico
            var mySeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Quantità",
                        Values = QuantitaValues
                    },
                    new ColumnSeries
                    {
                        Title = "Profitto",
                        Values = ProfittoValues
                    }
                    // Aggiungi altre serie se necessario
                };

                        // Imposta il DataContext del tuo grafico
                        MyChart.DataContext = this;

                        // Configura gli assi per ottenere un effetto di zoom
                        MyChart.AxisX = new AxesCollection
                {
                    new Axis
                    {
                        //Title = "Produzioni",
                        //LabelsRotation = 15, // Rotazione delle etichette sull'asse X
                        Separator = new Separator { Step = 1 } // Imposta il passo tra le etichette
                    }
                };

                        MyChart.AxisY = new AxesCollection
                {
                    new Axis
                    {
                        Title = "Produzioni",
                        LabelFormatter = value => value.ToString("N0") // Formattazione delle etichette sull'asse Y
                    }
                };

            // Chiamata a un metodo per aggiornare i dati del grafico
            UpdateChartData();
        }


        private void UpdateChartData()
        {
            // Ottieni i dati degli ordini evasi (puoi sostituire questa logica con i tuoi dati effettivi)
            List<OrderRow> ordiniEvasi = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks();

            // Filtra gli ordini evasi con stato "Evaso"
            var ordiniEvasiFiltrati = ordiniEvasi.Where(order => order.order.StatoOrdine == "Evaso").ToList();

            // Pulisci i dati esistenti nelle ChartValues
            QuantitaValues.Clear();
            ProfittoValues.Clear();

            // Itera sugli ordini evasi e popola le ChartValues
            foreach (var order in ordiniEvasiFiltrati)
            {
                // Aggiungi i dati delle quantità
                QuantitaValues.Add((float)order.Quantita);

                // Calcola e aggiungi i dati del profitto
                float profitto = CalcolaProfitto(order);
                ProfittoValues.Add(profitto);
            }

            // Rendi il grafico più zoommato
            //MyChart.AxisX[0].MaxValue = QuantitaValues.Count + 1;
            //MyChart.AxisY[0].MaxValue = Math.Ceiling(ProfittoValues.DefaultIfEmpty(0).Max());
        }

        #endregion
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #region Viste Dashboard
        private void AggiornaNumeroOrdiniDaEvadere()
        {
            // Ottieni la data di oggi
            DateTime oggi = DateTime.Today;

            // Ottieni gli ordini non ancora evasi in stato "Nuovo" dei prossimi 7 giorni
            List<OrderRow> ordiniNonEvasi = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks()
                .Where(order => order.order.StatoOrdine == "Nuovo" && DateTime.Parse(order.order.DataConsegna) >= oggi && DateTime.Parse(order.order.DataConsegna) <= oggi.AddDays(7))
                .ToList();

            // Conta il numero di ordini non ancora evasi
            int numeroOrdiniNonEvasi = ordiniNonEvasi.Count;

            // Aggiorna la proprietà NumeroOrdiniDaEvadere
            NumeroOrdiniDaEvadere = numeroOrdiniNonEvasi;

            NumeroOrdiniDaEvadereTextBlock.Text = numeroOrdiniNonEvasi.ToString();
        }

        private void AggiornaNumeroOrdiniScaduti()
        {
            // Ottieni la data di oggi
            DateTime oggi = DateTime.Today;

            // Filtra gli ordini in base alla data di consegna e allo stato "Nuovo" per i giorni precedenti
            int numeroOrdiniFiltrati = QueueUnitOfWorks.BiciclottiRepository.GetAllOrderStocks()
                .Count(order => order.order.StatoOrdine == "Nuovo" && Convert.ToDateTime(order.order.DataConsegna) < oggi);

            // Aggiorna la proprietà NumeroOrdiniDaEvadere
            NumeroOrdiniScaduti = numeroOrdiniFiltrati;

            // Visualizza il numero nella TextBox
            NumeroOrdiniScadutiTextBlock.Text = numeroOrdiniFiltrati.ToString();
        }


        private void AggiornaTopCliente()
        {
            Cliente clienteTop = GetClienteConPiuOrdini();

            TopClienteTextBox.Text = clienteTop.NomeCliente;
        }

        private Cliente GetClienteConPiuOrdini()
        {
            // Ottieni la lista di tutti i clienti
            List<Cliente> clienti = QueueUnitOfWorks.BiciclottiRepository.GetAllClients();

            // Trova il cliente con il massimo numero di ordini
            Cliente clienteConPiuOrdini = clienti.OrderByDescending(cliente => QueueUnitOfWorks.BiciclottiRepository.GetOrderCountByClient(cliente.NomeCliente)).FirstOrDefault();

            return clienteConPiuOrdini;
        }
        #endregion


    }

}
