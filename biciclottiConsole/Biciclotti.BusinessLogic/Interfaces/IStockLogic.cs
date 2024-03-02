using Biciclotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic
{
    public interface IStockLogic
    {
        List<Stock> GetAllStocks();

        Stock GetStocksById(int id);

        void AddStocks(Stock stocks);

        void UpdateStocks(Stock stocks);

        void DeleteStocks(int id);
    }
}
