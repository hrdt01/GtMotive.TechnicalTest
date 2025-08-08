using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.Api.Models
{
    /// <summary>
    /// NewFleetModel definition.
    /// </summary>
    public class NewFleetModel
    {
        /// <summary>
        /// Gets or sets the fleet name.
        /// </summary>
        [JsonRequired]
        public string FleetName { get; set; } = null!;
    }
}
