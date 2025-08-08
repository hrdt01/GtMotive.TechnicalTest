using FluentValidation;
using GtMotive.Estimate.Microservice.Application.UseCases.Validators;

namespace GtMotive.Estimate.Microservice.Application.UseCases.Fleet.GetAvailableVehiclesInFleet
{
    /// <summary>
    /// GetAvailableVehiclesInFleetValidator definition.
    /// </summary>
    public class GetAvailableVehiclesInFleetValidator : AbstractValidator<GetAvailableVehiclesInFleetRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesInFleetValidator"/> class.
        /// </summary>
        public GetAvailableVehiclesInFleetValidator()
        {
            Include(new SuppliedFleetIdInRequestValidator<GetAvailableVehiclesInFleetRequest>());
        }
    }
}
