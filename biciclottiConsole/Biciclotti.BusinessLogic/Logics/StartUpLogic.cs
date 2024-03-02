using Biciclotti.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Biciclotti.BusinessLogic
{
    public class StartUpLogic : IStartUpLogic
    {
        public void OnClosing(params string[] codes)
        {
        }

        public void Stop()
        {
            OnClosing();
            Environment.Exit(0);
        }

        public void OnStarting(params string[] codes)
        {
            AutoInitConfiguration();
            DatabaseInit();
            StartOperationThread();

        }
        CancellationTokenSource tmpOperationThread;

        async void StartOperationThread()
        {
            try
            {

                //Passo 1: Creo token univoco per questa macchina, serve per terminare il Thread anticipatamente
                tmpOperationThread = new CancellationTokenSource();

                //Passo 2: Creo Thread, aggiungo i seguenti parametri (Token, e tutte le informazioni)
                var operartionThread = new Thread(() =>
                {
                    //LogicFactory.Instance.Operation.CalculateOperationJobTime(tmpOperationThread);
                    while (!tmpOperationThread.IsCancellationRequested)
                        Thread.Sleep(1);
                });

                //Passo 4: Eseguo lo start del thread
                operartionThread.Start();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void DatabaseInit()
        {
            using (var context = new DbEntities())
            {
                context.Database.Migrate();
                context.SaveChanges(true);
            }
        }

        void AutoInitConfiguration()
        {
            try
            {
                var config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();

                if (config == null)
                {
                    Console.WriteLine("Be Careful!! There are problems with Configuration File: AppSettings");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
