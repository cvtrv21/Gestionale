using BiciclottiWpf.Models;
using FrameQueues.Attributes;
using BiciclottiWpf.Models.Utilities;
using Biciclotti.Models.Utilities;

namespace BiciclottiWpf.Data
{
    public interface IBiciclottiClientContracts
    {

        #region Bicycle

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista di tutte le bici.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Bicycle"/></returns>
        [ClientContract("Console", "global.bicycles.all", 1)]
        List<Bicycle> GetAllBicycles();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere una bici al database.
        /// </summary>
        /// <param name="bicycle"></param>
        /// <returns>L'oggetto <seealso cref="Bicycle"/> appena aggiunto.</returns>
        [ClientContract("Console", "global.bicycles.add", 1)]
        Bicycle AddBicycle(Bicycle bicycle);

        /// <summary>
        /// Sfrutta FrameQueue per ricavare una bici partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Bicycle"/> trovato.</returns>
        [ClientContract("Console", "global.bicycles.getbyid", 1)]
        Bicycle GetBicycle(int id);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare una bici sul database.
        /// </summary>
        /// <param name="bicycle"></param>
        /// <returns>L'oggetto <see cref="Bicycle"/> aggiornato.</returns>
        [ClientContract("Console", "global.bicycles.update", 1)]
        Bicycle UpdateBicycle(Bicycle bicycle);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare una bici partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        [ClientContract("Console", "global.bicycles.delete", 1)]
        void DeleteBicycle(int id);

        #endregion

        #region Stock

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli stock.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Stock"/></returns>
        [ClientContract("Console", "global.stock.all", 1)]
        List<Stock> GetAllStocks();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere uno stock al database.
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>L'oggetto <seealso cref="Stock"/> appena aggiunto.</returns>
        [ClientContract("Console", "global.stock.add", 1)]
        Stock AddStock(Stock stock);

        /// <summary>
        /// Sfrutta FrameQueue per ricavare uno stock partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Stock"/> trovato.</returns>
        [ClientContract("Console", "global.stock.getbyid", 1)]
        Stock GetStock(int id);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare uno stock sul database.
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>L'oggetto <see cref="Stock"/> aggiornato.</returns>
        [ClientContract("Console", "global.stock.update", 1)]
        Stock UpdateStock(Stock stock);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare uno stock partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        [ClientContract("Console", "global.stock.delete", 1)]
        void DeleteStock(int id);

        #endregion

        #region OrderRow

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli orderrow.
        /// </summary>
        /// <returns>Una lista di <seealso cref="OrderRow"/></returns>
        [ClientContract("Console", "global.orderrow.all", 1)]
        List<OrderRow> GetAllOrderRows();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere un orderrow al database.
        /// </summary>
        /// <param name="orderRow"></param>
        /// <returns>L'oggetto <seealso cref="OrderRow"/> appena aggiunto.</returns>
        [ClientContract("Console", "global.orderrow.add", 1)]
        OrderRow AddOrderRow(OrderRow orderRow);

        /// <summary>
        /// Sfrutta FrameQueue per ricavare un orderrow partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="OrderRow"/> trovato.</returns>
        [ClientContract("Console", "global.orderrow.getbyid", 1)]
        public List<OrderRow> GetOrderRow(int id);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare un orderrow sul database.
        /// </summary>
        /// <param name="orderRow"></param>
        /// <returns>L'oggetto <see cref="OrderRow"/> aggiornato.</returns>
        [ClientContract("Console", "global.orderrow.update", 1)]
        OrderRow UpdateOrderRow(OrderRow orderRow);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare un orderrow partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        [ClientContract("Console", "global.orderrow.delete", 1)]
        void DeleteOrderRow(int id);

        #endregion

        #region Order

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli order.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Order"/></returns>
        [ClientContract("Console", "global.order.all", 1)]
        List<Order> GetAllOrders();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere un order al database.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>L'oggetto <seealso cref="Order"/> appena aggiunto.</returns>
        [ClientContract("Console", "global.order.add", 1)]
        Order AddOrder(Order order);

        /// <summary>
        /// Sfrutta FrameQueue per ricavare un order partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Order"/> trovato.</returns>
        [ClientContract("Console", "global.order.getbyid", 1)]
        Order GetOrder(int id);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare un order sul database.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>L'oggetto <see cref="Order"/> aggiornato.</returns>
        [ClientContract("Console", "global.order.update", 1)]
        Order UpdateOrder(Order order);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare uno order partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        [ClientContract("Console", "global.order.delete", 1)]
        void DeleteOrder(int id);

        #endregion

        #region Clienti
        /// <summary>
        /// Riceve la chiamata dal TryCall() di Wpf e prende la lista dei clienti.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Cliente"/></returns>
        [ClientContract("Console", "global.client.all", 1)]
        List<Cliente> GetAllClients();

        /// <summary>
        /// Riceve la chiamata dal TryCall() di Wpf e aggiunge un cliente al database.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>L'oggetto <seealso cref="Cliente"/> appena aggiunto.</returns>
        [ClientContract("Console", "global.client.add", 1)]
        Cliente AddClient(Cliente cliente);

        /// <summary>
        /// Riceve la chiamata dal TryCall() di Wpf e trova un cliente partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Cliente"/> trovato.</returns>
        
        //[ServerContract("Console", "global.client.getbyid", 1)]
        //Cliente GetClient(int id);

        /// <summary>
        /// Riceve la chiamata dal TryCall() di Wpf e aggiorna un cliente sul database.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>L'oggetto <see cref="Cliente"/> aggiornato.</returns>
        [ClientContract("Console", "global.client.update", 1)]
        Cliente UpdateClient(Cliente cliente);

        /// <summary>
        /// Riceve la chiamata dal TryCall() di Wpf e elimina uno cliente partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        [ClientContract("Console", "global.client.delete", 1)]
        void DeleteClient(int id);

        [ClientContract("Console", "global.client.getClientCount", 1)]
        public int GetOrderCountByClient(string clientName);
        #endregion

        #region Custom Methods
        [ClientContract("Console", "global.customStock.all", 1)]
        List<Stock> GetAllBicycleStocks();

        [ClientContract("Console", "global.customStock.stockOrder", 1)]
        List<OrderRow> GetAllOrderStocks();

        [ClientContract("Console", "global.customStock.cambioSato", 1)]
        public bool CambioStatoOrdine(int codiceOrdine);

        [ClientContract("Console", "global.customStock.addOrUpdate", 1)]
        public void AddOrUpdateStock(Stock newStock);

        [ClientContract("Console", "global.customStock.getFromCO", 1)]
        int GetBicycleIdFromCodiceFornitore(int codiceFornitore);

        [ClientContract("Console", "global.customStock.deleteOrderandOrderRow", 1)]
        public void DeleteOrderAndOrderRow(int id);

        #endregion




    }
}