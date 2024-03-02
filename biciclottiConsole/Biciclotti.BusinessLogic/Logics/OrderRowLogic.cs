using Biciclotti.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic
{
    public class OrderRowLogic: IOrderRowLogic
    {
        public List<OrderRow> GetOrderRowsByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }

        public OrderRow GetOrderRowById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddOrderRow(OrderRow orderRow)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderRow(OrderRow orderRow)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderRow(int id)
        {
            throw new NotImplementedException();
        }
    }
}
