using GtMotive.Estimate.Microservice.Domain.DbEntities;
using GtMotive.Estimate.Microservice.Domain.DTO;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Entities.ValueObjects;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.Mappers
{
    /// <summary>
    /// Entity Mapper.
    /// </summary>
    public static class EntityMapper
    {
        /// <summary>
        /// Db entity Mapper.
        /// </summary>
        /// <param name="source">Dto entity.</param>
        /// <returns>Db entity.</returns>
        public static VehicleDto FromDbEntityToDto(this FleetVehicle source)
        {
            ArgumentNullException.ThrowIfNull(source);
            var result = new VehicleDto
            {
                Brand = source.Vehicle?.Brand!,
                Model = source.Vehicle?.Model!,
                ManufacturedOn = source.Vehicle!.ManufacturedOn,
                VehicleId = source.VehicleId
            };

            return result;
        }

        /// <summary>
        /// Db entity Mapper.
        /// </summary>
        /// <param name="source">Dto entity.</param>
        /// <returns>Db entity.</returns>
        public static IEnumerable<VehicleDto> FromDbEntityToDto(this IEnumerable<FleetVehicle>? source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var collection = source.Select(fleetVehicle => fleetVehicle.FromDbEntityToDto()).ToList();

            return collection;
        }

        /// <summary>
        /// Dto Mapper.
        /// </summary>
        /// <param name="source">Dto entity.</param>
        /// <returns>Db entity.</returns>
        public static IList<IVehicle> FromDtoToDomain(this IEnumerable<VehicleDto> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var collection = source.Select(fleetVehicle => fleetVehicle.FromDtoToDomain()).Cast<IVehicle>().ToList();

            return collection;
        }

        /// <summary>
        /// Db entity Mapper.
        /// </summary>
        /// <param name="source">Dto entity.</param>
        /// <returns>Db entity.</returns>
        public static VehicleEntity FromDtoToDomain(this VehicleDto source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var result = new VehicleEntity(
                new VehicleId(source.VehicleId),
                new Brand(source.Brand!),
                new Model(source.Model!),
                new ManufacturedOn(source.ManufacturedOn));

            return result;
        }

        /// <summary>
        /// Entity Mapper.
        /// </summary>
        /// <param name="source">Dto entity.</param>
        /// <returns>Db entity.</returns>
        public static Domain.DbEntities.Vehicle FromDomainToDbEntity(this Domain.Entities.Vehicle source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var result = new Domain.DbEntities.Vehicle
            {
                VehicleId = Guid.Parse(source.Id.ToString()),
                Model = source.Model.ToString(),
                Brand = source.Brand.ToString(),
                ManufacturedOn = source.ManufacturedOn.ToDateTime()
            };

            return result;
        }

        /// <summary>
        /// Entity Mapper.
        /// </summary>
        /// <param name="source">Dto entity.</param>
        /// <param name="fleetId">Fleet identifier.</param>
        /// <returns>Db entity.</returns>
        public static ICollection<FleetVehicle>? FromDomainToDbEntity(this IReadOnlyCollection<IVehicle> source, Guid fleetId)
        {
            ArgumentNullException.ThrowIfNull(source);
            var result = new List<FleetVehicle>(
                source.Select(vehicle => ((Domain.Entities.Vehicle)vehicle).FromDomainToDbEntity())
                    .Select(item => new FleetVehicle { FleetId = fleetId, VehicleId = item.VehicleId }));
            return result;
        }

        /// <summary>
        /// Db entity Mapper.
        /// </summary>
        /// <param name="source">Dto entity.</param>
        /// <param name="entityFactoryInstance">Instance of <see cref="IFleetEntityFactory"/>.</param>
        /// <returns>Db entity.</returns>
        public static Domain.DbEntities.Fleet ToDbEntity(this FleetDto source, IFleetEntityFactory entityFactoryInstance)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(entityFactoryInstance);

            var domainEntity = source.FleetId != Guid.Empty
                ? entityFactoryInstance.NewFleet(
                    new FleetId(source.FleetId),
                    new FleetName(source.FleetName!),
                    new FleetVehicles(source.Vehicles!.FromDtoToDomain()))
                : entityFactoryInstance.NewFleet(
                    new FleetName(source.FleetName!),
                    new FleetVehicles(source.Vehicles!.FromDtoToDomain()));

            var result = new Domain.DbEntities.Fleet
            {
                FleetId = Guid.Parse(((FleetEntity)domainEntity).Id.ToString()),
                FleetName = ((FleetEntity)domainEntity).FleetName.ToString(),
                FleetVehicles = ((FleetEntity)domainEntity).FleetVehicles.Vehicles.FromDomainToDbEntity(Guid.Parse(((FleetEntity)domainEntity).Id.ToString()))
            };

            return result;
        }

        /// <summary>
        /// Db entity Mapper.
        /// </summary>
        /// <param name="source">IFleet entity.</param>
        /// <returns>Db entity.</returns>
        public static Domain.DbEntities.Fleet ToDbEntity(this IFleet source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var result = new Domain.DbEntities.Fleet
            {
                FleetId = Guid.Parse(((FleetEntity)source).Id.ToString()),
                FleetName = ((FleetEntity)source).FleetName.ToString(),
                FleetVehicles = ((FleetEntity)source).FleetVehicles.Vehicles.FromDomainToDbEntity(Guid.Parse(((FleetEntity)source).Id.ToString()))
            };

            return result;
        }

        /// <summary>
        /// Db entity Mapper.
        /// </summary>
        /// <param name="source">IFleet entity.</param>
        /// <returns>Db entity.</returns>
        public static Domain.DbEntities.Customer ToDbEntity(this ICustomer source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var result = new Domain.DbEntities.Customer
            {
                CustomerId = Guid.Parse(((CustomerEntity)source).Id.ToString()),
                CustomerName = ((CustomerEntity)source).CustomerName.ToString()
            };

            return result;
        }

        /// <summary>
        /// Db entity Mapper.
        /// </summary>
        /// <param name="source">IVehicle entity.</param>
        /// <returns>Db entity.</returns>
        public static Domain.DbEntities.Vehicle ToDbEntity(this IVehicle source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var result = new Domain.DbEntities.Vehicle
            {
                VehicleId = Guid.Parse(((VehicleEntity)source).Id.ToString()),
                Model = ((VehicleEntity)source).Model.ToString(),
                Brand = ((VehicleEntity)source).Brand.ToString(),
                ManufacturedOn = ((VehicleEntity)source).ManufacturedOn.ToDateTime()
            };

            return result;
        }

        /// <summary>
        /// Db entity Mapper.
        /// </summary>
        /// <param name="source">Dto entity.</param>
        /// <param name="entityFactoryInstance">Instance of <see cref="IVehicleEntityFactory"/>.</param>
        /// <returns>Db entity.</returns>
        public static Domain.DbEntities.Vehicle ToDbEntity(this VehicleDto source, IVehicleEntityFactory entityFactoryInstance)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(entityFactoryInstance);

            var domainEntity = source.VehicleId != Guid.Empty
                ? entityFactoryInstance.NewVehicle(
                    new VehicleId(source.VehicleId),
                    new Brand(source.Brand!),
                    new Model(source.Model!),
                    new ManufacturedOn(source.ManufacturedOn))
                : entityFactoryInstance.NewVehicle(
                    new Brand(source.Brand!),
                    new Model(source.Model!),
                    new ManufacturedOn(source.ManufacturedOn));

            var result = new Domain.DbEntities.Vehicle
            {
                VehicleId = Guid.Parse(((VehicleEntity)domainEntity).Id.ToString()),
                Model = ((VehicleEntity)domainEntity).Model.ToString(),
                Brand = ((VehicleEntity)domainEntity).Brand.ToString(),
                ManufacturedOn = ((VehicleEntity)domainEntity).ManufacturedOn.ToDateTime()
            };

            return result;
        }

        /// <summary>
        /// Db entity Mapper.
        /// </summary>
        /// <param name="source">Dto entity.</param>
        /// <param name="entityFactoryInstance">Instance of <see cref="IRentedVehicleEntityFactory"/>.</param>
        /// <returns>Db entity.</returns>
        public static Domain.DbEntities.RentedVehicle ToDbEntity(this RentedVehicleDto source, IRentedVehicleEntityFactory entityFactoryInstance)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(entityFactoryInstance);

            var domainEntity = source.RentedVehicleId != Guid.Empty
                ? entityFactoryInstance.NewRentedVehicle(
                    new RentedVehicleId(source.RentedVehicleId),
                    new VehicleId(source.VehicleId),
                    source.StartRent,
                    source.EndRent,
                    new FleetId(Guid.Parse(source.FleetId.ToString())),
                    new CustomerId(Guid.Parse(source.CustomerId.ToString())))
                : entityFactoryInstance.NewRentedVehicle(
                    new VehicleId(source.VehicleId),
                    source.StartRent,
                    source.EndRent,
                    new FleetId(Guid.Parse(source.FleetId.ToString())),
                    new CustomerId(Guid.Parse(source.CustomerId.ToString())));

            var result = new Domain.DbEntities.RentedVehicle
            {
                RentedVehicleId = Guid.Parse(((RentedVehicleEntity)domainEntity).Id.ToString()),
                VehicleId = Guid.Parse(((RentedVehicleEntity)domainEntity).VehicleId.ToString()),
                CustomerId = Guid.Parse(((RentedVehicleEntity)domainEntity).CustomerId.ToString()),
                FleetId = Guid.Parse(((RentedVehicleEntity)domainEntity).FleetId.ToString()),
                RentStartedOn = ((RentedVehicleEntity)domainEntity).RentStartedOn.ToDateTime(),
                RentFinishedOn = ((RentedVehicleEntity)domainEntity).RentFinishedOn.ToDateTime()
            };

            return result;
        }
    }
}
