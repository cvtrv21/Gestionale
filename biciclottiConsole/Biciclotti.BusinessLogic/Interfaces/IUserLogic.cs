using Biciclotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        // Metodo per ottenere tutti gli User nel database
        List<User> GetAllUsers();       

        // Metodo per aggiungere una nuova bicicletta
        void AddUser(User user);

        // Metodo per aggiornare una bicicletta esistente
        void UpdateUser(User user);

        // Metodo per eliminare una bicicletta
        void DeleteUser(int id);
    }
}
