

namespace Biciclotti.BusinessLogic
{
    public interface IStartUpLogic
    {
        void OnClosing(params string[] codes);
        void OnStarting(params string[] codes);
        void Stop();
    }
}