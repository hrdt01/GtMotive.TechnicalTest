namespace GtMotive.Estimate.Microservice.BaseTest.TestHelpers
{
    /// <summary>
    /// Static class with information used in tests classes.
    /// </summary>
    public static class BaseTestConstants
    {
        /// <summary>
        /// Gets a generic customer name.
        /// </summary>
        public const string CustomerNameTest = "CustomerNameTest";

        /// <summary>
        /// Gets a generic fleet name.
        /// </summary>
        public const string FleetNameTest = "FleetNameTest";

        /// <summary>
        /// Gets a generic brand name.
        /// </summary>
        public const string BrandNameTest = "BrandNameTest";

        /// <summary>
        /// Gets a generic model name.
        /// </summary>
        public const string ModelNameTest = "ModelNameTest";

        /// <summary>
        /// Gets a moment in time.
        /// </summary>
        public static readonly DateTime Moment = DateTime.UtcNow;

        /// <summary>
        /// Gets a generic FleetId.
        /// </summary>
        public static Guid FleetIdTest => Guid.Parse("D9AD8F87-0DC5-4D6E-8034-6FD9915EA8BE");

        /// <summary>
        /// Gets a generic VehicleId.
        /// </summary>
        public static Guid VehicleIdTest => Guid.Parse("20F93B03-E9E3-4790-9D49-2971A1076D91");

        /// <summary>
        /// Gets a generic CustomerId.
        /// </summary>
        public static Guid CustomerIdTest => Guid.Parse("23493B03-E9E3-1111-9D49-2971A1076D91");

        /// <summary>
        /// Gets a generic RentedVehicleId.
        /// </summary>
        public static Guid RentedVehicleIdTest => Guid.Parse("B435B674-08FA-4DF0-8FCF-D1F8485E10DC");

        /// <summary>
        /// Gets a generic ManufacturedOn value.
        /// </summary>
        public static DateTime ManufacturedOnTest => Moment.AddYears(-1);

        /// <summary>
        /// Gets a generic RentStartedOn value.
        /// </summary>
        public static DateTime RentStartedOn => Moment;

        /// <summary>
        /// Gets a generic RentFinishedOn value.
        /// </summary>
        public static DateTime RentFinishedOn => Moment.AddHours(1);
    }
}
