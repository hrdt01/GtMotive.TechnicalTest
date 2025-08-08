using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.Api.Models
{
    /// <summary>
    /// AddNewVehicleModel definition.
    /// </summary>
    public class AddNewVehicleModel
    {
        /// <summary>
        /// Gets or sets fleet identifier.
        /// </summary>
        [JsonRequired]
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets or sets vehicle's brand.
        /// </summary>
        [JsonRequired]
        public string VehicleBrand { get; set; } = null!;

        /// <summary>
        /// Gets or sets vehicle's model.
        /// </summary>
        [JsonRequired]
        public string VehicleModel { get; set; } = null!;

        /// <summary>
        /// Gets or sets vehicle's manufacturedOn date.
        /// </summary>
        [JsonRequired]
        public DateTime VehicleManufacturedOn { get; set; }
    }
}
