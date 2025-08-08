using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// IVehicleEntityFactory definition.
    /// </summary>
    public interface IVehicleEntityFactory
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IVehicle"/> class.
        /// </summary>
        /// <param name="brandName">Brand name.</param>
        /// <param name="modelName">Model name.</param>
        /// <param name="manufacturedOn">Manufacture date.</param>
        /// <returns>Instance of <see cref="IVehicle"/>.</returns>
        IVehicle NewVehicle(Brand brandName, Model modelName, ManufacturedOn manufacturedOn);

        /// <summary>
        /// Initializes a new instance of <see cref="IVehicle"/> class.
        /// </summary>
        /// <param name="id">Vehicle identifier.</param>
        /// <param name="brandName">Brand name.</param>
        /// <param name="modelName">Model name.</param>
        /// <param name="manufacturedOn">Manufacture date.</param>
        /// <returns>Instance of <see cref="IVehicle"/>.</returns>
        IVehicle NewVehicle(VehicleId id, Brand brandName, Model modelName, ManufacturedOn manufacturedOn);
    }
}
