using Biciclotti.Models;
using Biciclotti.Models.Utilities;
using BiciclottiWpf.Models.Utilities;
using FrameQueues.Attributes;

namespace Biciclotti.Service;

public interface IBiciclottiServerContracts
{

    #region Bicycle

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e prende la lista di tutte le bici.
    /// </summary>
    /// <returns>Una lista di <seealso cref="Bicycle"/></returns>
    [ServerContract("Console", "global.bicycles.all", 1)]
    List<Bicycle> GetAllBicycles();

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiunge una bici al database.
    /// </summary>
    /// <param name="bicycle"></param>
    /// <returns>L'oggetto <seealso cref="Bicycle"/> appena aggiunto.</returns>
    [ServerContract("Console", "global.bicycles.add", 1)]
    Bicycle AddBicycle(Bicycle bicycle);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e cerca una bici partendo dall'id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>L'oggetto <see cref="Bicycle"/> trovato.</returns>
    [ServerContract("Console", "global.bicycles.getbyid", 1)]
    Bicycle GetBicycle(int id);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiorna una bici sul database.
    /// </summary>
    /// <param name="bicycle"></param>
    /// <returns>L'oggetto <see cref="Bicycle"/> aggiornato.</returns>
    [ServerContract("Console", "global.bicycles.update", 1)]
    Bicycle UpdateBicycle(Bicycle bicycle);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e elimina una bici partendo dall'id
    /// </summary>
    /// <param name="id"></param>
    [ServerContract("Console", "global.bicycles.delete", 1)]
    void DeleteBicycle(int id);

    #endregion

    #region Stock

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e prende la lista degli stock.
    /// </summary>
    /// <returns>Una lista di <seealso cref="Stock"/></returns>
    [ServerContract("Console", "global.stock.all", 1)]
    List<Stock> GetAllStocks();

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiunge uno stock al database.
    /// </summary>
    /// <param name="stock"></param>
    /// <returns>L'oggetto <seealso cref="Stock"/> appena aggiunto.</returns>
    [ServerContract("Console", "global.stock.add", 1)]
    Stock AddStock(Stock stock);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e trova uno stock partendo dall'id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>L'oggetto <see cref="Stock"/> trovato.</returns>
    [ServerContract("Console", "global.stock.getbyid", 1)]
    Stock GetStock(int id);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiorna uno stock sul database.
    /// </summary>
    /// <param name="stock"></param>
    /// <returns>L'oggetto <see cref="Stock"/> aggiornato.</returns>
    [ServerContract("Console", "global.stock.update", 1)]
    Stock UpdateStock(Stock stock);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e elimina uno stock partendo dall'id
    /// </summary>
    /// <param name="id"></param>
    [ServerContract("Console", "global.stock.delete", 1)]
    void DeleteStock(int id);

    #endregion

    #region OrderRow

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e prende la lista degli orderrow.
    /// </summary>
    /// <returns>Una lista di <seealso cref="OrderRow"/></returns>
    [ServerContract("Console", "global.orderrow.all", 1)]
    List<OrderRow> GetAllOrderRows();

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiunge un orderrow al database.
    /// </summary>
    /// <param name="orderRow"></param>
    /// <returns>L'oggetto <seealso cref="OrderRow"/> appena aggiunto.</returns>
    [ServerContract("Console", "global.orderrow.add", 1)]
    OrderRow AddOrderRow(OrderRow orderRow);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e trova un orderrow partendo dall'id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>L'oggetto <see cref="OrderRow"/> trovato.</returns>
    [ServerContract("Console", "global.orderrow.getbyid", 1)]
    public List<OrderRow> GetOrderRow(int id);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiorna un orderrow sul database.
    /// </summary>
    /// <param name="orderRow"></param>
    /// <returns>L'oggetto <see cref="OrderRow"/> aggiornato.</returns>
    [ServerContract("Console", "global.orderrow.update", 1)]
    OrderRow UpdateOrderRow(OrderRow orderRow);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e elimina un orderrow partendo dall'id
    /// </summary>
    /// <param name="id"></param>
    [ServerContract("Console", "global.orderrow.delete", 1)]
    void DeleteOrderRow(int id);

    #endregion

    #region Order

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e prende la lista degli order.
    /// </summary>
    /// <returns>Una lista di <seealso cref="Order"/></returns>
    [ServerContract("Console", "global.order.all", 1)]
    List<Order> GetAllOrders();

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiunge un order al database.
    /// </summary>
    /// <param name="order"></param>
    /// <returns>L'oggetto <seealso cref="Order"/> appena aggiunto.</returns>
    [ServerContract("Console", "global.order.add", 1)]
    Order AddOrder(Order order);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e trova un order partendo dall'id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>L'oggetto <see cref="Order"/> trovato.</returns>
    [ServerContract("Console", "global.order.getbyid", 1)]
    Order GetOrder(int id);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiorna un order sul database.
    /// </summary>
    /// <param name="order"></param>
    /// <returns>L'oggetto <see cref="Order"/> aggiornato.</returns>
    [ServerContract("Console", "global.order.update", 1)]
    Order UpdateOrder(Order order);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e elimina uno order partendo dall'id
    /// </summary>
    /// <param name="id"></param>
    [ServerContract("Console", "global.order.delete", 1)]
    void DeleteOrder(int id);


    #endregion

    #region Clienti
    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e prende la lista dei clienti.
    /// </summary>
    /// <returns>Una lista di <seealso cref="Cliente"/></returns>
    [ServerContract("Console", "global.client.all", 1)]
    List<Cliente> GetAllClients();

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e aggiunge un cliente al database.
    /// </summary>
    /// <param name="client"></param>
    /// <returns>L'oggetto <seealso cref="Cliente"/> appena aggiunto.</returns>
    [ServerContract("Console", "global.client.add", 1)]
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
    [ServerContract("Console", "global.client.update", 1)]
    Cliente UpdateClient(Cliente cliente);

    /// <summary>
    /// Riceve la chiamata dal TryCall() di Wpf e elimina uno cliente partendo dall'id
    /// </summary>
    /// <param name="id"></param>
    [ServerContract("Console", "global.client.delete", 1)]
    void DeleteClient(int id);
    #endregion

    #region Custom Methods
    [ServerContract("Console", "global.customStock.all", 1)]
    List<Stock> GetAllBicycleStocks();

    [ServerContract("Console", "global.customStock.stockOrder", 1)]
    List<OrderRow> GetAllOrderStocks();

    [ServerContract("Console", "global.customStock.cambioSato", 1)]
    public bool CambioStatoOrdine(int codiceOrdine);

    [ServerContract("Console", "global.customStock.addOrUpdate", 1)]
    public void AddOrUpdateStock(Stock newStock);

    [ServerContract("Console", "global.customStock.getFromCO", 1)]
    public int GetBicycleIdFromCodiceFornitore(int codiceFornitore);

    [ServerContract("Console", "global.customStock.deleteOrderandOrderRow", 1)]
    public void DeleteOrderAndOrderRow(int id);

    #endregion

}