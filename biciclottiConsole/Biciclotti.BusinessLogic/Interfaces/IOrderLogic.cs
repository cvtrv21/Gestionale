using Biciclotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic
{
    public interface IOrderLogic
    {
        List<Order> GetAllOrders();

        Order GetOrdersByCode(string code);

        void AddOrders(Order orders);

        void UpdateOrders(Order orders);

        void DeleteOrders(int id);
    }
}
