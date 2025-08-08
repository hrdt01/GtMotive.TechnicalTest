using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.Api.Models
{
    /// <summary>
    /// RentVehicleModel definition.
    /// </summary>
    public class RentVehicleModel
    {
        /// <summary>
        /// Gets or sets CustomerId.
        /// </summary>
        [JsonRequired]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets FleetId.
        /// </summary>
        [JsonRequired]
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets or sets VehicleId.
        /// </summary>
        [JsonRequired]
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets StartRent.
        /// </summary>
        [JsonRequired]
        public DateTime StartRent { get; set; }

        /// <summary>
        /// Gets or sets EndRent.
        /// </summary>
        [JsonRequired]
        public DateTime EndRent { get; set; }
    }
}
