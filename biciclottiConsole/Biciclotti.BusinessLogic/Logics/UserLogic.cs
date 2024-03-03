using Biciclotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biciclotti.BusinessLogic.Logics
{
    public class UserLogic
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllusers()
        {
            List<User> user = new List<User>();
            Console.WriteLine("GetAllUsers");

            try
            {
                // Effettua la query al database per ottenere tutte le biciclette.
                //bicycles = _dbContext.Bicycles.ToList();
            }
            catch (Exception ex)
            {
                // Registra eventuali eccezioni che si verificano durante l'operazione.
                Console.WriteLine(ex + "Si è verificato un errore durante la chiamata a GetAllUsers.");
                throw; // Rilancia l'eccezione per la gestione futura.
            }
            return user;
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
