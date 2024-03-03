using biciclotti.ServiceHost;
using Biciclotti.Data;
using FrameQueues.Interfaces;
using FrameQueues;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Biciclotti.BusinessLogic;
using Biciclotti.Service;

namespace Biciclotti.Console
{
    public class Program
    {
        protected static IServerContract QueueServer { get; set; }

        static bool _exitSystem;

        #region Trap application termination
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            //Logger.Info("Exiting Device due to external CTRL-C, or process kill, or shutdown");

            LogicFactory.Instance.StartUp.OnClosing();

            //allow main to run off
            _exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }
        #endregion

        static void Main(string[] args)
        {
            // Some biolerplate to react to close window event, CTRL-C, kill, etc
            _handler += Handler;

            SetConsoleCtrlHandler(_handler, true);

            if (args.Length == 1 && !bool.TryParse(args[0], out bool isDebug) && isDebug)
            {
                Debugger.Launch();
            }

            AutofacStarter.AutofacInit();

            // Qui inizializziamo la coda lato server per FrameQueue,
            // in maniera tale che il client possa raggiungere i metodi lato server
            QueueServer = new ServerContract<IBiciclottiServerContracts>(new BiciclottiHandlers(), "Console");
            QueueServer.OnConsumingError += (_, _, _, exception) => System.Console.WriteLine(exception);
            QueueServer.Start();

            LogicFactory.Instance.StartUp.OnStarting();
            
            System.Console.WriteLine("Controller Started!");

            //hold the console so it doesn’t run off the end
            while (!_exitSystem)
            {
                Thread.Sleep(500);
            }
        }
    }
}