using Biciclotti.BusinessLogic.Interfaces;

namespace Biciclotti.BusinessLogic
{
    public class LogicFactory : ILogicFactory
    {
        public static LogicFactory Instance { get; private set; }
        public IBicycleLogic Bicycle { get; set; }
        public IOrderRowLogic OrderRow { get; set; }
        public IOrderLogic Order { get; set; }
        public IStockLogic Stock { get; set; }
        public IClienteLogic Cliente { get; set; }
        public IUserLogic User { get; set; }

        public IStartUpLogic StartUp { get; set; }
        public bool IsInitialized { get; set; }

        #region Public methods

        public void Initialize()
        {
            if (Instance != null)
                throw new InvalidOperationException();
            Instance = this;
            IsInitialized = true;
        }

        #endregion
    }

    public sealed class LogicFactoriesCreator
    {
        #region Constructor

        public LogicFactoriesCreator(IEnumerable<ILogicFactory> env)
        {
            if (env.Any(x => x.IsInitialized == false))
                throw new InvalidOperationException();
        }

        #endregion
    }
}