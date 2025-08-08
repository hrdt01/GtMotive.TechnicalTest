using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.Api.Models
{
    /// <summary>
    /// ReturnRentedVehicleModel definition.
    /// </summary>
    public class ReturnRentedVehicleModel
    {
        /// <summary>
        /// Gets or sets CustomerId.
        /// </summary>
        [JsonRequired]
        public Guid RentedVehicleId { get; set; }

        /// <summary>
        /// Gets or sets CustomerId.
        /// </summary>
        [JsonRequired]
        public Guid CustomerId { get; set; }
    }
}
