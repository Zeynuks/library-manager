namespace Domain.Foundation
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}