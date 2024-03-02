using Biciclotti.Data;
using Biciclotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic
{
    public class BicycleLogic : IBicycleLogic
    {

        public void AddBicycle(Bicycle bicycle)
        {
            throw new NotImplementedException();
        }

        public void DeleteBicycle(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bicycle> GetAllBicycles()
        {
            List<Bicycle> bicycles = new List<Bicycle>();
            Console.WriteLine("Chiamato il metodo GetAllBicycles");

            try
            {
                // Effettua la query al database per ottenere tutte le biciclette.
                //bicycles = _dbContext.Bicycles.ToList();
            }
            catch (Exception ex)
            {
                // Registra eventuali eccezioni che si verificano durante l'operazione.
                Console.WriteLine(ex + "Si è verificato un errore durante la chiamata a GetAllBicycles.");
                throw; // Rilancia l'eccezione per la gestione futura.
            }
            return bicycles;
        }

        public void UpdateBicycle(Bicycle bicycle)
        {
            throw new NotImplementedException();
        }
    }
}