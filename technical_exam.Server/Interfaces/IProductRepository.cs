using technical_exam.Server.Models;

namespace technical_exam.Server.Interfaces
{
    public interface IProductRepository
    {
        Task<ReturnGetCartList> GetCartList(string userId);
        Task<ReturnGenericStatus> RemoveCart(ParamRemoveCartItem value);
        Task<ReturnGenericStatus> AddToCart(ParamAddCartItem value);        
    }
}
