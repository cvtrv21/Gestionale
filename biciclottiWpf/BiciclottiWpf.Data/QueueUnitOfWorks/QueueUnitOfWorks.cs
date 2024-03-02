namespace BiciclottiWpf.Data
{
    public class QueueUnitOfWorks
    {
        static IBiciclottiRepository _biciclottiRepository;
        public static IBiciclottiRepository BiciclottiRepository => _biciclottiRepository ??= new BiciclottiRepository();


        public static void Close() => BaseRepository.Close();
    }
}
