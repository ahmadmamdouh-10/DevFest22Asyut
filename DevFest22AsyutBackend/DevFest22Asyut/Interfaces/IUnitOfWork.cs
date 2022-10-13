namespace DevFest22Asyut.Services
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}
