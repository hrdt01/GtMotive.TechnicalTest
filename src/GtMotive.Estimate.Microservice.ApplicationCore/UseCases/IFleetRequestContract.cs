namespace GtMotive.Estimate.Microservice.Application.UseCases
{
    /// <summary>
    /// IFleetRequestContract definition.
    /// </summary>
    public interface IFleetRequestContract
    {
        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        string FleetId { get; set; }
    }
}
