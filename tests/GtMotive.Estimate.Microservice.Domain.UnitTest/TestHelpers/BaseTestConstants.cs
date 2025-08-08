namespace GtMotive.Estimate.Microservice.Domain.UnitTest.TestHelpers
{
    internal static class BaseTestConstants
    {
        internal const string CustomerNameTest = "CustomerNameTest";
        internal const string FleetNameTest = "FleetNameTest";
        internal const string BrandNameTest = "BrandNameTest";
        internal const string ModelNameTest = "ModelNameTest";

        internal static DateTime ManufacturedOnTest => DateTime.UtcNow.AddYears(-1);
    }
}
