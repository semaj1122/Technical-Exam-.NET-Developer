using System.Text.Json.Serialization;

namespace technical_exam.Server.Models
{
    public class ApiCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ParamRemoveCartItem
    {
        public string userId { get; set; }
        public string cartId { get; set; }
    }

    public class ParamAddCartItem
    {
        public string userId { get; set; }
        public string cartId { get; set; }
        public string productId { get; set; }
        public int quantity { get; set; }
    }

    public class ReturnGenericStatus
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }

    public class ReturnGetCartList
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public List<ReturnGetCartListData> data { get; set; }
    }

    public class ReturnGetCartListData
    {

        [JsonIgnore]
        public string statusCode { get; set; }

        [JsonIgnore]
        public string statusMessage { get; set; }
        public string id { get; set; }
        public string productName { get; set; }
        public decimal cost { get; set; }
        public decimal quantity { get; set; }
        public decimal amount { get; set; }
    }
}
