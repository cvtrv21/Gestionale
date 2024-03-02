using Biciclotti.Models.Utilities;
using BiciclottiWpf.Models;
using BiciclottiWpf.Models.Utilities;
using FrameQueues.Attributes;
using FrameQueues.Models;

namespace BiciclottiWpf.Data
{
    public sealed class BiciclottiRepository : BaseRepository, IBiciclottiRepository
    {

        #region Bicycle

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista di tutte le bici.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Bicycle"/></returns>
        public List<Bicycle> GetAllBicycles()
        {
            List<Bicycle> bicycles = new List<Bicycle>();
            try
            {
                bicycles = Client.TryCallSync(server => server.GetAllBicycles(), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return bicycles;
        }

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere una bici al database.
        /// </summary>
        /// <param name="bicycle"></param>
        /// <returns>L'oggetto <seealso cref="Bicycle"/> appena aggiunto.</returns>
        public Bicycle AddBicycle(Bicycle bicycle)
        {
            Bicycle bicycles = new Bicycle();
            try
            {
                bicycles = Client.TryCallSync(server => server.AddBicycle(bicycle), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return bicycles;
        }

        /// <summary>
        /// Sfrutta FrameQueue per ricavare una bici partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Bicycle"/> trovato.</returns>
        public Bicycle GetBicycle(int id)
        {
            Bicycle bicycle = null;
            try
            {
                bicycle = Client.TryCallSync(server => server.GetBicycle(id), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return bicycle;
        }

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare una bici sul database.
        /// </summary>
        /// <param name="bicycle"></param>
        /// <returns>L'oggetto <see cref="Bicycle"/> aggiornato.</returns>
        public Bicycle UpdateBicycle(Bicycle bicycle)
        {
            try
            {
                Client.TryCallSync(server => server.UpdateBicycle(bicycle), ExceptionManagements.RethrowException);
            }
            catch { }

            return bicycle;
        }
        
        /// <summary>
        /// Sfrutta FrameQueue per eliminare una bici partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBicycle(int id)
        {            
            try
            {
                Client.TryCallSync(server => server.DeleteBicycle(id), ExceptionManagements.RethrowException);
            }
            catch 
            {                
            }
        }

        #endregion

        #region Stock

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli stock.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Stock"/></returns>
        public List<Stock> GetAllStocks()
        {
            List<Stock> stocks = new List<Stock>();
            try
            {
                stocks = Client.TryCallSync(server => server.GetAllStocks(), ExceptionManagements.RethrowException).Data;
            }
            catch
            {

            }
            return stocks;
        }

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere uno stock al database.
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>L'oggetto <seealso cref="Stock"/> appena aggiunto.</returns>
        public Stock AddStock(Stock stock)
        {
            Stock stocks = new Stock();
            try
            {
                stocks = Client.TryCallSync(server => server.AddStock(stock), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return stocks;
        }

        /// <summary>
        /// Sfrutta FrameQueue per ricavare uno stock partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Stock"/> trovato.</returns>
        public Stock GetStock(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare uno stock sul database.
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>L'oggetto <see cref="Stock"/> aggiornato.</returns>
        public Stock UpdateStock(Stock stock)
        {
            try
            {
                Client.TryCallSync(server => server.UpdateStock(stock), ExceptionManagements.RethrowException);
            }
            catch { }

            return stock;
        }

        /// <summary>
        /// Sfrutta FrameQueue per eliminare uno stock partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteStock(int id)
        {
            try
            {
                Client.TryCallSync(server => server.DeleteStock(id), ExceptionManagements.RethrowException);
            }
            catch { }
        }

        #endregion

        #region OrderRow

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli orderrow.
        /// </summary>
        /// <returns>Una lista di <seealso cref="OrderRow"/></returns>
        public List<OrderRow> GetAllOrderRows()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere un orderrow al database.
        /// </summary>
        /// <param name="orderRow"></param>
        /// <returns>L'oggetto <seealso cref="OrderRow"/> appena aggiunto.</returns>
        public OrderRow AddOrderRow(OrderRow orderRow)
        {
            OrderRow orders = new OrderRow();
            try
            {
                orders = Client.TryCallSync(server => server.AddOrderRow(orderRow), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return orders;
        }

        /// <summary>
        /// Sfrutta FrameQueue per ricavare un orderrow partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="OrderRow"/> trovato.</returns>
        public List<OrderRow> GetOrderRow(int id)
        {
            List<OrderRow> orderRows = null;
            try
            {
                orderRows = Client.TryCallSync(server => server.GetOrderRow(id), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return orderRows;
        }


        /// <summary>
        /// Sfrutta FrameQueue per aggiornare un orderrow sul database.
        /// </summary>
        /// <param name="orderRow"></param>
        /// <returns>L'oggetto <see cref="OrderRow"/> aggiornato.</returns>
        public OrderRow UpdateOrderRow(OrderRow orderRow)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sfrutta FrameQueue per eliminare un orderrow partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrderRow(int id)
        {
            try
            {
                Client.TryCallSync(server => server.DeleteOrderRow(id), ExceptionManagements.RethrowException);
            }
            catch { }
        }

        #endregion

        #region Order

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli order.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Order"/></returns>
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            try
            {
                orders = Client.TryCallSync(server => server.GetAllOrders(), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return orders;
        }

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere un order al database.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>L'oggetto <seealso cref="Order"/> appena aggiunto.</returns>
        public Order AddOrder(Order order)
        {
            Order orders = new Order();
            try
            {
                orders = Client.TryCallSync(server => server.AddOrder(order), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return orders;
        }

        /// <summary>
        /// Sfrutta FrameQueue per ricavare un order partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Order"/> trovato.</returns>
        public Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare un order sul database.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>L'oggetto <see cref="Order"/> aggiornato.</returns>
        public Order UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sfrutta FrameQueue per eliminare uno order partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrder(int id)
        {
            try
            {
                Client.TryCallSync(server => server.DeleteOrder(id), ExceptionManagements.RethrowException);
            }
            catch
            {
            }
        }

        #endregion

        #region Clienti

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista di tutti i clienti.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Bicycle"/></returns>
        public List<Cliente> GetAllClients()
        {
            List<Cliente> cliente = new List<Cliente>();
            try
            {
                cliente = Client.TryCallSync(server => server.GetAllClients(), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return cliente;
        }

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere un cliente al database.
        /// </summary>
        /// <param name="bicycle"></param>
        /// <returns>L'oggetto <seealso cref="Bicycle"/> appena aggiunto.</returns>
        public Cliente AddClient(Cliente cliente)
        {
            Cliente clienti = new Cliente();
            try
            {
                clienti = Client.TryCallSync(server => server.AddClient(cliente), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return clienti;
        }

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare un cliente sul database.
        /// </summary>
        /// <param name="bicycle"></param>
        /// <returns>L'oggetto <see cref="Bicycle"/> aggiornato.</returns>
        public Cliente UpdateClient(Cliente cliente)
        {
            try
            {
                Client.TryCallSync(server => server.UpdateClient(cliente), ExceptionManagements.RethrowException);
            }
            catch { }

            return cliente;
        }

        /// <summary>
        /// Sfrutta FrameQueue per eliminare un cliente partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteClient(int id)
        {
            try
            {
                Client.TryCallSync(server => server.DeleteClient(id), ExceptionManagements.RethrowException);
            }
            catch
            {
            }
        }

        #endregion



        #region Metodi Custom
        public List<Stock> GetAllBicycleStocks()
        {
            List<Stock> stocksBicycle = new List<Stock>();
            try
            {
                stocksBicycle = Client.TryCallSync(server => server.GetAllBicycleStocks(), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return stocksBicycle;
        }        

        public List<OrderRow> GetAllOrderStocks()
        {
            List<OrderRow> orderStock = new List<OrderRow>();
            try
            {
                orderStock = Client.TryCallSync(server => server.GetAllOrderStocks(), ExceptionManagements.RethrowException).Data;
            }
            catch { }

            return orderStock;
        }

        public bool CambioStatoOrdine(int codiceOrdine)
        {
            bool risposta = true;

            try
            {
                risposta = Client.TryCallSync(server => server.CambioStatoOrdine(codiceOrdine), ExceptionManagements.RethrowException).Data;                
                
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Errore durante la chiamata remota per cambiare lo stato dell'ordine: " + ex.Message);        
            }

            return risposta;

        }

        public void AddOrUpdateStock(Stock newStock)
        {
            try
            {
                Client.TryCallSync(server => server.AddOrUpdateStock(newStock), ExceptionManagements.RethrowException);
            }
            catch { }
        }

        public int GetBicycleIdFromCodiceFornitore(int codiceFornitore)
        {
            try
            {
                int bicycleId; // Inizializza con un valore di default (0 o -1, a seconda della convenzione).

                // Esegui la chiamata al server per ottenere il BicycleID dal CodiceFornitore
                bicycleId = Client.TryCallSync(server => server.GetBicycleIdFromCodiceFornitore(codiceFornitore), ExceptionManagements.RethrowException).Data;
            
                return bicycleId;
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
                Client.TryCallSync(server => server.DeleteOrderAndOrderRow(id), ExceptionManagements.RethrowException);
            }
            catch
            {
            }
        }









        #endregion

    }

}
