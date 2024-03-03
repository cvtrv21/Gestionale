using Biciclotti.Models.Utilities;
using BiciclottiWpf.Models;
using BiciclottiWpf.Models.Utilities;
using FrameQueues.Attributes;

namespace BiciclottiWpf.Data
{
    public interface IBiciclottiRepository
    {

        #region Bicycle

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista di tutte le bici.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Bicycle"/></returns>
        List<Bicycle> GetAllBicycles();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere una bici al database.
        /// </summary>
        /// <param name="bicycle"></param>
        /// <returns>L'oggetto <seealso cref="Bicycle"/> appena aggiunto.</returns>
        Bicycle AddBicycle(Bicycle bicycle);

        /// <summary>
        /// Sfrutta FrameQueue per ricavare una bici partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Bicycle"/> trovato.</returns>
        Bicycle GetBicycle(int id);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare una bici sul database.
        /// </summary>
        /// <param name="bicycle"></param>
        /// <returns>L'oggetto <see cref="Bicycle"/> aggiornato.</returns>
        Bicycle UpdateBicycle(Bicycle bicycle);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare una bici partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        void DeleteBicycle(int id);

        #endregion

        #region Stock

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli stock.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Stock"/></returns>
        List<Stock> GetAllStocks();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere uno stock al database.
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>L'oggetto <seealso cref="Stock"/> appena aggiunto.</returns>
        Stock AddStock(Stock stock);

        /// <summary>
        /// Sfrutta FrameQueue per ricavare uno stock partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Stock"/> trovato.</returns>
        Stock GetStock(int id);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare uno stock sul database.
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>L'oggetto <see cref="Stock"/> aggiornato.</returns>
        Stock UpdateStock(Stock stock);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare uno stock partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        void DeleteStock(int id);

        #endregion

        #region OrderRow

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli orderrow.
        /// </summary>
        /// <returns>Una lista di <seealso cref="OrderRow"/></returns>
        List<OrderRow> GetAllOrderRows();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere un orderrow al database.
        /// </summary>
        /// <param name="orderRow"></param>
        /// <returns>L'oggetto <seealso cref="OrderRow"/> appena aggiunto.</returns>
        OrderRow AddOrderRow(OrderRow orderRow);

        /// <summary>
        /// Sfrutta FrameQueue per ricavare un orderrow partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="OrderRow"/> trovato.</returns>
        public List<OrderRow> GetOrderRow(int id);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare un orderrow sul database.
        /// </summary>
        /// <param name="orderRow"></param>
        /// <returns>L'oggetto <see cref="OrderRow"/> aggiornato.</returns>
        OrderRow UpdateOrderRow(OrderRow orderRow);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare un orderrow partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        void DeleteOrderRow(int id);

        #endregion

        #region Order

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli order.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Order"/></returns>
        List<Order> GetAllOrders();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere un order al database.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>L'oggetto <seealso cref="Order"/> appena aggiunto.</returns>
        Order AddOrder(Order order);

        /// <summary>
        /// Sfrutta FrameQueue per ricavare un order partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Order"/> trovato.</returns>
        Order GetOrder(int id);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare un order sul database.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>L'oggetto <see cref="Order"/> aggiornato.</returns>
        Order UpdateOrder(Order order);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare uno order partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        void DeleteOrder(int id);

        #endregion

        #region Cliente

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli order.
        /// </summary>
        /// <returns>Una lista di <seealso cref="Cliente"/></returns>
        List<Cliente> GetAllClients();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere un order al database.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>L'oggetto <seealso cref="Cliente"/> appena aggiunto.</returns>
        Cliente AddClient(Cliente client);

        /// <summary>
        /// Sfrutta FrameQueue per ricavare un order partendo dall'id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>L'oggetto <see cref="Cliente"/> trovato.</returns>
        //Order GetOrder(int id);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare un order sul database.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>L'oggetto <see cref="Cliente"/> aggiornato.</returns>
        Cliente UpdateClient(Cliente client);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare uno order partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        void DeleteClient(int id);

        int GetOrderCountByClient(string clientName);

        #endregion

        #region User

        /// <summary>
        /// Sfrutta FrameQueue per prendere la lista degli user.
        /// </summary>
        /// <returns>Una lista di <seealso cref="User"/></returns>
        List<User> GetAllUsers();

        /// <summary>
        /// Sfrutta FrameQueue per aggiungere un User al database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>L'oggetto <seealso cref="User"/> appena aggiunto.</returns>
        User AddUser(User user);

        /// <summary>
        /// Sfrutta FrameQueue per aggiornare un User sul database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>L'oggetto <see cref="Cliente"/> aggiornato.</returns>
        User UpdateUser(User user);

        /// <summary>
        /// Sfrutta FrameQueue per eliminare uno order partendo dall'id
        /// </summary>
        /// <param name="id"></param>
        void DeleteUser(int id);
        #endregion

        #region Custom Methods
        List<Stock> GetAllBicycleStocks();

        List<OrderRow> GetAllOrderStocks();

        public bool CambioStatoOrdine(int codiceOrdine);

        public void AddOrUpdateStock(Stock newStock);
     
        public int GetBicycleIdFromCodiceFornitore(int codiceFornitore);

        public void DeleteOrderAndOrderRow(int id);

        #endregion
    }
}

