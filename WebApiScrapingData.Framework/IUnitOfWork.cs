namespace WebApiScrapingData.Framework
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
