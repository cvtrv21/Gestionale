using Biciclotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic.Logics
{
    public class ClienteLogic
    {
        public void AddClient(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public void DeleteClient(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bicycle> GetAllClients()
        {
            List<Bicycle> bicycles = new List<Bicycle>();
            Console.WriteLine("Chiamato il metodo GetAllClients");

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

        public void UpdateClient(Bicycle bicycle)
        {
            throw new NotImplementedException();
        }

       /* public void GetOrderCountByClient(int id)
        {
            throw new NotImplementedException();
        }*/
    }
}
