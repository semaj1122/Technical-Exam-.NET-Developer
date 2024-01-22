using technical_exam.Server.Interfaces;
using technical_exam.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace technical_exam.Server.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IConfiguration config, IUnitOfWork unitOfWork, ILogger<ProductController> logger)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("get-cart-list")]
        [Consumes("application/json")]
        public async Task<ReturnGetCartList> GetCartList(string userId)
        {
            var results = await _unitOfWork.Product.GetCartList(userId);

            return results;
        }

        [HttpPost("remove-cart")]
        [Consumes("application/json")]
        public async Task<ReturnGenericStatus> RemoveCart(ParamRemoveCartItem value)
        {
            var results = await _unitOfWork.Product.RemoveCart(value);

            return results;
        }

        [HttpPost("add-to-cart")]
        [Consumes("application/json")]
        public async Task<ReturnGenericStatus> AddToCart(ParamAddCartItem value)
        {
            var results = await _unitOfWork.Product.AddToCart(value);

            return results;
        }   

    }
}
