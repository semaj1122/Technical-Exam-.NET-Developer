using technical_exam.Server.Interfaces;

namespace technical_exam.Server.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
             IProductRepository product
         )
        {
            Product = product;
        }
        public IProductRepository Product { get; }
    }
}
