using Biciclotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic
{
    public interface IOrderRowLogic
    {
        List<OrderRow> GetOrderRowsByOrderId(int orderId);

        OrderRow GetOrderRowById(int id);

        void AddOrderRow(OrderRow orderRow);

        void UpdateOrderRow(OrderRow orderRow);

        void DeleteOrderRow(int id);

    }
}
