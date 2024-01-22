using technical_exam.Server.Interfaces;
using technical_exam.Server.Models;
using technical_exam.Server.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Dapper;

namespace technical_exam.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISQLDataAccess _db;
        private readonly IConfiguration _config;
        public ProductRepository(ISQLDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        
        public async Task<ReturnGetCartList> GetCartList(string userId)
        {
            var obj = new ReturnGetCartList();
            var param = new DynamicParameters();

            try
            {
                param.Add("@userId", userId);

                var results = await _db.LoadData<ReturnGetCartListData, dynamic>("get_cart_list", param);
                obj = new ReturnGetCartList
                {
                    statusCode = results.First().statusCode,
                    statusMessage = results.First().statusMessage,
                    data = results.ToList()
                };
            }
            catch (Exception e)
            {
                obj = new ReturnGetCartList { statusCode = "01", statusMessage = e.Message.ToString() };
            }
            return obj;

        }

        public async Task<ReturnGenericStatus> RemoveCart(ParamRemoveCartItem value)
        {
            var obj = new ReturnGenericStatus();
            var param = new DynamicParameters();

            try
            {
                param.Add("@userId", value.userId);
                param.Add("@cartId", value.cartId);

                var results = await _db.LoadData<ReturnGenericStatus, dynamic>("remove_cart_id", param);
                obj = new ReturnGenericStatus
                {
                    statusCode = results.First().statusCode,
                    statusMessage = results.First().statusMessage
                };
            }
            catch (Exception e)
            {
                obj = new ReturnGenericStatus { statusCode = "01", statusMessage = e.Message.ToString() };
            }
            return obj;

        }

        public async Task<ReturnGenericStatus> AddToCart(ParamAddCartItem value)
        {
            var obj = new ReturnGenericStatus();
            var param = new DynamicParameters();

            try
            {
                param.Add("@userId", value.userId);
                param.Add("@cartId", value.cartId);
                param.Add("@productId", value.productId);
                param.Add("@quantity", value.quantity);

                var results = await _db.LoadData<ReturnGenericStatus, dynamic>("add_to_cart", param);
                obj = new ReturnGenericStatus
                {
                    statusCode = results.First().statusCode,
                    statusMessage = results.First().statusMessage
                };
            }
            catch (Exception e)
            {
                obj = new ReturnGenericStatus { statusCode = "01", statusMessage = e.Message.ToString() };
            }
            return obj;

        }

    }
}
