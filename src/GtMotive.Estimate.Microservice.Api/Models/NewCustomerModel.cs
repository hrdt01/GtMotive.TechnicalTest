using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.Api.Models
{
    /// <summary>
    /// NewCustomerModel definition.
    /// </summary>
    public class NewCustomerModel
    {
        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        [JsonRequired]
        public string CustomerName { get; set; } = null!;
    }
}
