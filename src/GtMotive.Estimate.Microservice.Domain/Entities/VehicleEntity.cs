using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <inheritdoc />
    public class VehicleEntity : Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleEntity"/> class.
        /// </summary>
        /// <param name="brandName">Vehicle's brand name.</param>
        /// <param name="modelName">Vehicle's model name.</param>
        /// <param name="manufacturedOn">Vehicle's manufacture date.</param>
        public VehicleEntity(Brand brandName, Model modelName, ManufacturedOn manufacturedOn)
        {
            Id = new VehicleId(Guid.NewGuid());
            Brand = brandName;
            Model = modelName;
            ManufacturedOn = manufacturedOn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleEntity"/> class.
        /// </summary>
        /// <param name="id">Vehicle's identifier.</param>
        /// <param name="brandName">Vehicle's brand name.</param>
        /// <param name="modelName">Vehicle's model name.</param>
        /// <param name="manufacturedOn">Vehicle's manufacture date.</param>
        public VehicleEntity(VehicleId id, Brand brandName, Model modelName, ManufacturedOn manufacturedOn)
        {
            Id = id;
            Brand = brandName;
            Model = modelName;
            ManufacturedOn = manufacturedOn;
        }
    }
}
