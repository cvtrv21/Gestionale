using Biciclotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic.Interfaces
{
    public interface IClienteLogic
    {
        // Metodo per ottenere tutte le biciclette nel database
        List<Cliente> GetAllClients();

        // Metodo per ottenere una bicicletta per ID
        //Bicycle GetBicycleById(int id);

        // Metodo per aggiungere una nuova bicicletta
        void AddClient(Cliente cliente);

        // Metodo per aggiornare una bicicletta esistente
        void UpdateClient(Cliente cliente);

        // Metodo per eliminare una bicicletta
        void DeleteClient(int id);

        // Metodo che restituisce il numero di ordini effettuati da un cliente
        //void GetOrderCountByClient(int id);
    }
}
