using Biciclotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic
{
    public interface IBicycleLogic
    {

        // Metodo per ottenere tutte le biciclette nel database
        List<Bicycle> GetAllBicycles();

        // Metodo per ottenere una bicicletta per ID
        //Bicycle GetBicycleById(int id);

        // Metodo per aggiungere una nuova bicicletta
        void AddBicycle(Bicycle bicycle);

        // Metodo per aggiornare una bicicletta esistente
        void UpdateBicycle(Bicycle bicycle);

        // Metodo per eliminare una bicicletta
        void DeleteBicycle(int id);

    }

}
