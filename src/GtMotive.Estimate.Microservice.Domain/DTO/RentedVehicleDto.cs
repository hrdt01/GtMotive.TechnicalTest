namespace GtMotive.Estimate.Microservice.Domain.DTO
{
    /// <summary>
    /// DTO Entity RentedVehicle.
    /// </summary>
    public class RentedVehicleDto : IDtoEntity
    {
        /// <summary>
        /// Gets or sets RentedVehicleId.
        /// </summary>
        public Guid RentedVehicleId { get; set; }

        /// <summary>
        /// Gets or sets CustomerId.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets FleetId.
        /// </summary>
        public Guid FleetId { get; set; }

        /// <summary>
        /// Gets or sets VehicleId.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets StartRent.
        /// </summary>
        public DateTime StartRent { get; set; }

        /// <summary>
        /// Gets or sets EndRent.
        /// </summary>
        public DateTime EndRent { get; set; }
    }
}
