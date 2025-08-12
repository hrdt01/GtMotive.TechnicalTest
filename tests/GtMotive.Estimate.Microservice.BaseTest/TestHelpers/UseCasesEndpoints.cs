namespace GtMotive.Estimate.Microservice.BaseTest.TestHelpers
{
    /// <summary>
    /// Static class with information used in tests classes.
    /// </summary>
    public static class UseCasesEndpoints
    {
        /// <summary>
        /// Gets NewCustomer endpoint.
        /// </summary>
        public static string NewCustomerEndpoint => "customer/newcustomer";

        /// <summary>
        /// Gets RentVehicle endpoint.
        /// </summary>
        public static string RentVehicleEndpoint => "customer/rentvehicle";

        /// <summary>
        /// Gets ReturnRentedVehicle endpoint.
        /// </summary>
        public static string ReturnRentedVehicleEndpoint => "customer/returnrentedvehicle";

        /// <summary>
        /// Gets GetAvailableVehicles endpoint.
        /// </summary>
        public static string GetAvailableVehiclesEndpoint => "fleet/{fleetId}/availablevehicles";

        /// <summary>
        /// Gets NewFleet endpoint.
        /// </summary>
        public static string NewFleetEndpoint => "fleet/newfleet";

        /// <summary>
        /// Gets AddNewVehicle endpoint.
        /// </summary>
        public static string AddNewVehicleEndpoint => "fleet/addvehicle";
    }
}
