using Biciclotti.Data;
using Biciclotti.Models;
using Biciclotti.Models.Utilities;
using BiciclottiWpf.Models.Utilities;
using FrameQueues.Interfaces;
using FrameQueues.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biciclotti.Service;

public class BiciclottiHandlers : IBiciclottiServerContracts
{
    DbEntities context = new DbEntities();

    #region Bicycle
    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e prende la lista di tutte le bici.
    /// </summary>
    /// <returns>Una lista di <seealso cref="Bicycle"/></returns>
    public List<Bicycle> GetAllBicycles()
    {
        List<Bicycle> bicycles = new List<Bicycle>();

        try
        {
            bicycles = context.Bicycles.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return bicycles;
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiunge una bici al database.
    /// </summary>
    /// <param name="bicycle"></param>
    /// <returns>L'oggetto <seealso cref="Bicycle"/> appena aggiunto.</returns>
    public Bicycle AddBicycle(Bicycle bicycle)
    {
        bool successo = false;

        try
        {
            // Verifico che l'elemento che aggiungiamo non sia già presente 
            var control = context.Bicycles.FirstOrDefault(c => c.Marca == bicycle.Marca && c.Modello == bicycle.Modello);

            if (control == null)
            {
                context.Bicycles.Add(bicycle);
                context.SaveChanges();
                return bicycle; // Restituisci la bicicletta aggiunta
                
            }
            else
            {
                Console.WriteLine("Elemeno già esistente, errore nell'aggiunta");
                return null;                
            }
           
        }
        catch (Exception ex)
        {
            // Gestisci l'eccezione o registra il messaggio di errore, ad esempio:
            Console.WriteLine("Errore durante l'aggiunta della bicicletta al database: " + ex.Message);
            throw; // Rilancia l'eccezione per la gestione superiore
        }

    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e cerca una bici partendo dall'id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>L'oggetto <see cref="Bicycle"/> trovato.</returns>
    public Bicycle GetBicycle(int id)
    {
        try
        {
            
            Bicycle bicycle = context.Bicycles.FirstOrDefault(b => b.BicycleId == id);

            if (bicycle != null)
            {
                return bicycle; // Restituisci la bicicletta trovata
            }
            else
            {
                throw new Exception($"Bicicletta con ID {id} non trovata.");
            }
        }
        catch (Exception ex)
        {
            // Gestisci l'eccezione in base alle tue esigenze, ad esempio registrandola o lanciando un'altra eccezione.
            throw new Exception($"Errore durante il recupero della bicicletta: {ex.Message}");
        }
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiorna una bici sul database.
    /// </summary>
    /// <param name="bicycle"></param>
    /// <returns>L'oggetto <see cref="Bicycle"/> aggiornato.</returns>
    public Bicycle UpdateBicycle(Bicycle bicycle)
    {
        try
        {
            // Ottieni la Bike dal database in base all'ID
            var existingBike = context.Bicycles.Single(x => x.BicycleId == bicycle.BicycleId);

            // Aggiorna la quantità con il nuovo valore
            existingBike.PrezzoAcquisto = bicycle.PrezzoAcquisto;
            existingBike.PrezzoVendita = bicycle.PrezzoVendita;

            // Salva le modifiche nel database
            context.SaveChanges();

            Console.WriteLine("Prezzi aggiornati con successo");

            // Restituisci l'oggetto aggiornato
            return existingBike;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante l'aggiornamento dei prezzi: " + ex.Message);
            throw; // Puoi gestire l'eccezione in modo diverso se necessario
        }
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e elimina una bici partendo dall'id
    /// </summary>
    /// <param name="id"></param>
    public void DeleteBicycle(int id)
    {
        List<Bicycle> bicycles = new List<Bicycle>();

        try
        {
            context.Bicycles.Remove(context.Bicycles.Single(x => x.BicycleId == id));
            context.SaveChanges();
            Console.WriteLine("Bicicletta rimossa con successo");
        }
        catch
        {
            throw;
        }
    }
    #endregion

    #region Stock

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e prende la lista degli stock.
    /// </summary>
    /// <returns>Una lista di <seealso cref="Stock"/></returns>
    public List<Stock> GetAllStocks()
    {
        List<Stock> stocks = new List<Stock>();
        try
        {
            stocks = context.Stocks.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return stocks;
    }


    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiunge uno stock al database.
    /// </summary>
    /// <param name="stock"></param>
    /// <returns>L'oggetto <seealso cref="Stock"/> appena aggiunto.</returns>
    public Stock AddStock(Stock stock)
    {
        try
        {
            // Verifico che l'elemento che aggiungiamo non sia già presente 
            var control = context.Stocks.FirstOrDefault(c => c.Taglia == stock.Taglia && c.Quantita == stock.Quantita);

            if (control == null)
            {
                context.Stocks.Add(stock);
                context.SaveChanges();
                Console.WriteLine("Elemento Stock aggiunto");
                return stock; // Restituisci la bicicletta aggiunta
            }
            else
            {
                Console.WriteLine("Elemeno già esistente, errore nell'aggiunta");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Gestisci l'eccezione o registra il messaggio di errore, ad esempio:
            Console.WriteLine("Errore durante l'aggiunta dello stock al database: " + ex.Message);
            throw; // Rilancia l'eccezione per la gestione superiore
        }
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e trova uno stock partendo dall'id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>L'oggetto <see cref="Stock"/> trovato.</returns>
    public Stock GetStock(int id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiorna uno stock sul database.
    /// </summary>
    /// <param name="stock"></param>
    /// <returns>L'oggetto <see cref="Stock"/> aggiornato.</returns>
    public Stock UpdateStock(Stock stock)
    {
        try
        {
            // Ottieni lo stock dal database in base all'ID
            var existingStock = context.Stocks.Single(x => x.StockId == stock.StockId);

            // Aggiorna la quantità con il nuovo valore
            existingStock.Quantita = stock.Quantita;

            // Salva le modifiche nel database
            context.SaveChanges();

            Console.WriteLine("Stock aggiornato con successo");

            // Restituisci l'oggetto aggiornato
            return existingStock;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante l'aggiornamento dello stock: " + ex.Message);
            throw; // Puoi gestire l'eccezione in modo diverso se necessario
        }
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e elimina uno stock partendo dall'id
    /// </summary>
    /// <param name="id"></param>
    public void DeleteStock(int id)
    {
        try
        {
            var stock = context.Stocks.Single(x => x.StockId == id);
            context.Stocks.Remove(stock);
            context.SaveChanges();
            Console.WriteLine("Stock rimosso con successo");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore rimozione bici: ", ex.Message);
        }
    }   


    #endregion

    #region OrderRow

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e prende la lista degli orderrow.
    /// </summary>
    /// <returns>Una lista di <seealso cref="OrderRow"/></returns>
    public List<OrderRow> GetAllOrderRows()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiunge un orderrow al database.
    /// </summary>
    /// <param name="orderRow"></param>
    /// <returns>L'oggetto <seealso cref="OrderRow"/> appena aggiunto.</returns
    public OrderRow AddOrderRow(OrderRow orderRow)
    {
        try
        {
            // Verifico che l'elemento che aggiungiamo non sia già presente 
            var control = context.OrderRows.FirstOrDefault(c => c.OrderRowId == orderRow.OrderRowId);

            if (control == null)
            {
                context.OrderRows.Add(orderRow);
                context.SaveChanges();
                Console.WriteLine("Elemento OrderRow aggiunto");
                return orderRow; // Restituisci la bicicletta aggiunta
            }
            else
            {
                Console.WriteLine("Elemeno già esistente, errore nell'aggiunta");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Gestisci l'eccezione o registra il messaggio di errore, ad esempio:
            Console.WriteLine("Errore durante l'aggiunta dello stock al database: " + ex.Message);
            throw; // Rilancia l'eccezione per la gestione superiore
        }
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e trova un orderrow partendo dall'id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>L'oggetto <see cref="OrderRow"/> trovato.</returns
    public List<OrderRow> GetOrderRow(int id)
    {
        try
        {
            using (var context3 = new DbEntities()) // Sostituisci "DbEntities" con il tuo contesto del database
            {
                var query = context3.OrderRows.Where(or => or.CodiceOrdine == id).ToList();

                if (query != null)
                {
                    return query; // Restituisci la lista di OrderRow trovati
                }
                else
                {
                    throw new Exception($"OrderRow con ID {id} non trovato.");
                }
            }
        }
        catch (Exception ex)
        {
            // Gestisci l'eccezione in base alle tue esigenze, ad esempio registrandola o lanciando un'altra eccezione.
            throw new Exception($"Errore durante il recupero di OrderRow: {ex.Message}");
        }
    }



    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiorna un orderrow sul database.
    /// </summary>
    /// <param name="orderRow"></param>
    /// <returns>L'oggetto <see cref="OrderRow"/> aggiornato.</returns>
    public OrderRow UpdateOrderRow(OrderRow orderRow)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e elimina un orderrow partendo dall'id
    /// </summary>
    /// <param name="id"></param>
    public void DeleteOrderRow(int id)
    {
        try
        {
            var order = context.OrderRows.Single(x => x.CodiceOrdine == id);
            context.OrderRows.Remove(order);
            context.SaveChanges();
            Console.WriteLine("OrderRow rimosso con successo");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore rimozione orderRow: ", ex.Message);
        }
    }

    #endregion

    #region Order

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e prende la lista degli order.
    /// </summary>
    /// <returns>Una lista di <seealso cref="Order"/></returns>
    public List<Order> GetAllOrders()
    {
        List<Order> orders = new List<Order>();
        try
        {
            orders = context.Orders.ToList();
            Console.WriteLine("GetAllOrders eseguito");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return orders;
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiunge un order al database.
    /// </summary>
    /// <param name="order"></param>
    /// <returns>L'oggetto <seealso cref="Order"/> appena aggiunto.</returns>
    public Order AddOrder(Order order)
    {
        try
        {
            // Verifico che l'elemento che aggiungiamo non sia già presente 
            var control = context.Orders.FirstOrDefault(c => c.CodiceOrdine == order.CodiceOrdine);

            if (control == null)
            {
                context.Orders.Add(order);
                context.SaveChanges();
                Console.WriteLine("Elemento Orders aggiunto");
                return order; // Restituisci la bicicletta aggiunta
            }
            else
            {
                Console.WriteLine("Elemeno già esistente, errore nell'aggiunta");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Gestisci l'eccezione o registra il messaggio di errore, ad esempio:
            Console.WriteLine("Errore durante l'aggiunta dello stock al database: " + ex.Message);
            throw; // Rilancia l'eccezione per la gestione superiore
        }
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e trova un order partendo dall'id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>L'oggetto <see cref="Order"/> trovato.</returns>
    public Order GetOrder(int id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiorna un order sul database.
    /// </summary>
    /// <param name="order"></param>
    /// <returns>L'oggetto <see cref="Order"/> aggiornato.</returns>
    public Order UpdateOrder(Order order)
    {
        try
        {

            // Ottieni lo stock dal database in base all'ID
            var existingStock = context.Orders.Single(x => x.CodiceOrdine == order.CodiceOrdine);

            // Aggiorna la quantità con il nuovo valore
            existingStock.StatoOrdine = order.StatoOrdine;


            // Salva le modifiche nel database
            context.SaveChanges();

            Console.WriteLine("Order aggiornato con successo");

            // Restituisci l'oggetto aggiornato
            return existingStock;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante l'aggiornamento dell'Ordine: " + ex.Message);
            throw; // Puoi gestire l'eccezione in modo diverso se necessario
        }
    }

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e elimina uno order partendo dall'id
    /// </summary>
    /// <param name="id"></param>
    public void DeleteOrder(int id)
    {
        try
        {
            var order = context.Orders.Single(x => x.CodiceOrdine == id);
            context.Orders.Remove(order);
            context.SaveChanges();
            Console.WriteLine("Order rimosso con successo");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore rimozione order: ", ex.Message);
        }
    }

    #endregion

    #region Clienti
    public List<Cliente> GetAllClients()
    {
        List<Cliente> client = new List<Cliente>();

        try
        {
            client = context.Clienti.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
       

        return client;
    }

    // Metodo per aggiungere un nuovo cliente
    public Cliente AddClient(Cliente cliente)
    {
        bool successo = false;

        try
        {
            // Verifico che l'elemento che aggiungiamo non sia già presente 
            var control = context.Clienti.FirstOrDefault(c => c.IdCliente == cliente.IdCliente);

            if (control == null)
            {
                context.Clienti.Add(cliente);
                context.SaveChanges();
                return cliente; // Restituisci il cliente aggiunto

            }
            else
            {
                Console.WriteLine("Elemeno già esistente, errore nell'aggiunta");
                return null;
            }

        }
        catch (Exception ex)
        {
            // Gestisci l'eccezione o registra il messaggio di errore, ad esempio:
            Console.WriteLine("Errore durante l'aggiunta del cliente al database: " + ex.Message);
            throw; // Rilancia l'eccezione per la gestione superiore
        }
    }

    // Metodo per aggiornare una bicicletta esistente
    public Cliente UpdateClient(Cliente cliente)
    {


        try
        {
            // Ottieni la Bike dal database in base all'ID
            var existingClient = context.Clienti.Single(x => x.IdCliente == cliente.IdCliente);

            existingClient.NomeCliente = cliente.NomeCliente;
            existingClient.Indirizzo = cliente.Indirizzo;
            existingClient.Telefono = cliente.Telefono;
            existingClient.Email = cliente.Email;            

            // Salva le modifiche nel database
            context.SaveChanges();

            Console.WriteLine("Cliente aggiornato con successo");

            // Restituisci l'oggetto aggiornato
            return existingClient;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante l'aggiornamento del cliente: " + ex.Message);
            throw; // Puoi gestire l'eccezione in modo diverso se necessario
        }
    }

    // Metodo per eliminare una bicicletta
    public void DeleteClient(int id)
    {
        List<Cliente> cliente = new List<Cliente>();

        try
        {
            context.Clienti.Remove(context.Clienti.Single(x => x.IdCliente == id));
            context.SaveChanges();
            Console.WriteLine("Bicicletta rimossa con successo");
        }
        catch
        {
            throw;
        }
    }

    public int GetOrderCountByClient(string clientName)
    {
        try
        {
            int orderCount = context.Orders
                .Join(context.Clienti, order => order.NomeCliente, client => client.NomeCliente, (order, client) => new { order, client })
                .Where(x => x.client.NomeCliente == clientName)
                .Count();

            return orderCount;
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Custom Methods
    public List<Stock> GetAllBicycleStocks()
    {
        try
        {    
            var query = context.Stocks
                .Include(a => a.Bicycle)
                .ToList();            

            Console.WriteLine("GetAllBicycleStocks eseguito");
            return query;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore nel metodo GetAllBicycleStocks", ex.Message);
            return null;
        }        
    }

    public List<OrderRow> GetAllOrderStocks()
    {
        try
        {
            var query = context.OrderRows
                .Include(a => a.order)
                .ToList();

            Console.WriteLine("GetAllOrderStocks");

            return query;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"$\"Errore nel metodo GetAllOrderStocks", ex.Message);
            return null;
        }
        
    }

    public bool CambioStatoOrdine(int codiceOrdine)
    {
        bool successo = false; // Inizializza la variabile di successo a false

        try
        {
            using (var context2 = new DbEntities())
            {
                // Recupera l'ordine specifico dalla tabella Orders utilizzando il codiceOrdine
                var ordine = context.Orders.FirstOrDefault(o => o.CodiceOrdine == codiceOrdine);

                if (ordine != null)
                {
                    // Verifica che lo stato sia attualmente "Nuovo"
                    if (ordine.StatoOrdine == "Nuovo")
                    {
                        var righeOrdine = context.OrderRows.Where(or => or.CodiceOrdine == codiceOrdine).ToList();

                        bool righeSuccesso = true; // Inizializza una variabile per le righe dell'ordine

                        foreach (var rigaOrdine in righeOrdine)
                        {
                            // Recupera l'elemento corrispondente dalla tabella Stocks
                            var stock = context.Stocks.FirstOrDefault(s => s.BicycleId == rigaOrdine.BicycleId && s.Taglia == rigaOrdine.Taglia);

                            if (stock != null)
                            {
                                // Verifica se c'è abbastanza quantità in magazzino per soddisfare l'ordine
                                if (stock.Quantita >= rigaOrdine.Quantita)
                                {
                                    // Sottrai la quantità dalla quantità in stock
                                    stock.Quantita -= rigaOrdine.Quantita;
                                    Console.WriteLine("Quantità sottratta dallo stock con successo.");
                                }
                                else
                                {
                                    // Se la quantità dello stock non è sufficiente, imposta la variabile delle righe su false
                                    righeSuccesso = false;
                                    Console.WriteLine("Stock non trovato per questa bicicletta e taglia o quantità insufficiente.");
                                    break; // Esci dal ciclo foreach
                                }
                            }
                            else
                            {
                                Console.WriteLine("Stock non trovato per questa bicicletta e taglia.");
                                righeSuccesso = false;
                                break; // Esci dal ciclo foreach
                            }
                        }

                        if (righeSuccesso)
                        {
                            // Se tutte le righe sono state elaborate con successo, cambia lo stato in "Evaso"
                            ordine.StatoOrdine = "Evaso";
                            Console.WriteLine("Stato dell'ordine cambiato con successo.");
                            context2.SaveChanges();
                            context.SaveChanges();
                            successo = true; // Imposta la variabile di successo su true solo se tutte le righe sono state elaborate con successo
                        }
                    }
                    else
                    {
                        Console.WriteLine("Impossibile cambiare lo stato dell'ordine. Lo stato attuale non è 'Nuovo'.");
                    }
                }
                else
                {
                    Console.WriteLine("Ordine non trovato.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante il cambio di stato dell'ordine: " + ex.Message);
            throw;
        }

        return successo; // Restituisci la variabile di successo alla fine del metodo
    }

    public void AddOrUpdateStock(Stock newStock)
    {
        try
        {
            // Verifica se esiste già una giacenza con lo stesso BicycleId nel database
            Stock existingStock = context.Stocks.FirstOrDefault(s => s.Bicycle.BicycleId == newStock.BicycleId && s.Taglia == newStock.Taglia);
            

            if (existingStock != null && existingStock.Taglia == newStock.Taglia)
            {
                // Già esistente: aggiorna la quantità invece di aggiungere una nuova riga
                existingStock.Quantita += newStock.Quantita;
            }

            else if(existingStock != null && existingStock.Taglia != newStock.Taglia)
            {
                Stock stock = new Stock
                {
                    BicycleId = newStock.BicycleId,
                    Taglia = newStock.Taglia,
                    Quantita = newStock.Quantita,
                };

                context.Stocks.Add(stock);
            }
            else
            {
                context.Stocks.Add(newStock);
            }

            // Salva le modifiche nel database
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Accedi all'eccezione interna, se presente
            Exception innerException = ex.InnerException;

            if (innerException != null)
            {
                // Puoi stampare o registrare il messaggio dell'eccezione interna
                Console.WriteLine("Eccezione interna: " + innerException.Message);
            }

            // Gestisci l'eccezione esterna
            throw new Exception("Errore durante l'aggiunta della giacenza: " + ex.Message);
        }
    }

    public int GetBicycleIdFromCodiceFornitore(int codiceFornitore)
    {
        try
        {
            // In alternativa, se hai una lista di biciclette in memoria, puoi cercare il BicycleID in base al CodiceFornitore.
            var bicycle = context.Bicycles.FirstOrDefault(b => b.CodiceFornitore == codiceFornitore);

            if (bicycle != null)
            {
                Console.WriteLine("Lettura da Barcode eseguita con successo");
                return bicycle.BicycleId;
            }
            else
            {
                Console.WriteLine("Lettura Barcode fallita");
                return 0; // Restituisci 0 se il BicycleID non è stato trovato
            }
        }
        catch (Exception ex)
        {
            // Gestisci eventuali eccezioni qui
            throw new Exception("Errore durante la ricerca del BicycleID dal CodiceFornitore: " + ex.Message);
        }
    }

    public void DeleteOrderAndOrderRow(int id)
    {
        try
        {
            var order = context.Orders.SingleOrDefault(x => x.CodiceOrdine == id);
            if (order != null)
            {
                // Cancella gli ordini collegati a questa riga d'ordine
                var orderRows = context.OrderRows.Where(x => x.CodiceOrdine == id);
                context.OrderRows.RemoveRange(orderRows);

                context.Orders.Remove(order);
                context.SaveChanges();
                Console.WriteLine("Order e relative OrderRow rimosse con successo");
            }
            else
            {
                Console.WriteLine("Ordine non trovato.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore nella rimozione dell'ordine e delle relative OrderRow: " + ex.Message);
        }
    }



    #endregion
}

