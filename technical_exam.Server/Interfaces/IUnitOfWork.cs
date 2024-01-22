namespace technical_exam.Server.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
    }
}
