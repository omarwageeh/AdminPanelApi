using AmazonClone.Data.Context;
using AmazonClone.Repository.Interface;

namespace AmazonClone.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private bool disposedValue;

        public IUserRepository UserRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IAdminRepository AdminRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IOrderDetailsRepository OrderDetailsRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public UnitOfWork(DataContext context, IUserRepository userRepository, ICustomerRepository customerRepository, IAdminRepository adminRepository, ICategoryRepository categoryRepository, IProductRepository productRepository, IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            _context = context;
            UserRepository = userRepository;
            CustomerRepository = customerRepository;
            AdminRepository = adminRepository;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
            OrderDetailsRepository = orderDetailsRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        { 
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}