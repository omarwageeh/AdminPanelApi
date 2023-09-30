namespace AmazonClone.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IAdminRepository AdminRepository { get; }
        IProductRepository ProductRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IOrderRepository OrderRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
