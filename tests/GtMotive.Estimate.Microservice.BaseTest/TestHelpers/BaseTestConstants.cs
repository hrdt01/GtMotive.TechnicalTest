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
        /// Gets a generic ManufacturedOn value.
        /// </summary>
        public static DateTime ManufacturedOnTest => DateTime.UtcNow.AddYears(-1);
    }
}
