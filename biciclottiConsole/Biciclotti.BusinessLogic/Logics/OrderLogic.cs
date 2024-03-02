using Biciclotti.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic
{
    public class OrderLogic: IOrderLogic
    {
        
        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order GetOrdersByCode(string codice)
        {
            throw new NotImplementedException();
        }

        public void AddOrders(Order orders)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrders(Order orders)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrders(int id)
        {
            throw new NotImplementedException();
        }
    }
}
