using Microsoft.Extensions.Logging;

namespace GtMotive.Estimate.Microservice.Infrastructure.Logging
{
    /// <summary>
    /// LogMessages implementation.
    /// </summary>
    public static partial class LogMessages
    {
        /// <summary>
        /// Log warning implementation.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        /// <param name="prefix">Prefix for message.</param>
        /// <param name="id">Identifier.</param>
        [LoggerMessage(Level = LogLevel.Warning, Message = "{prefix} Not found fleet by its identifier: {id}")]
        public static partial void LogWarningNotFoundFleet(this ILogger logger, string prefix, string id);

        /// <summary>
        /// Log warning implementation.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        /// <param name="prefix">Prefix for message.</param>
        /// <param name="id">Identifier.</param>
        [LoggerMessage(Level = LogLevel.Warning, Message = "{prefix} Not found rented vehicle by its identifier: {id}")]
        public static partial void LogWarningNotFoundRentedVehicle(this ILogger logger, string prefix, string id);

        /// <summary>
        /// Log info implementation.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        /// <param name="prefix">Prefix for message.</param>
        /// <param name="id">Identifier.</param>
        [LoggerMessage(Level = LogLevel.Information, Message = "{prefix} Handling request for identifier: {id}")]
        public static partial void LogInfoHandlingGetRequest(this ILogger logger, string prefix, string id);

        /// <summary>
        /// Log error implementation.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        /// <param name="prefix">Prefix for message.</param>
        /// <param name="reason">Identifier.</param>
        /// <param name="stackTrace">Stack trace.</param>
        [LoggerMessage(Level = LogLevel.Error, Message = "{prefix} Exception raised: {reason}. {stackTrace}")]
        public static partial void LogErrorProcessingRequest(this ILogger logger, string prefix, string reason, string? stackTrace);

        /// <summary>
        /// Log info implementation.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> instance.</param>
        /// <param name="prefix">Prefix for message.</param>
        /// <param name="method">Method requested.</param>
        [LoggerMessage(Level = LogLevel.Information, Message = "{prefix} Handling request for: {method}")]
        public static partial void LogInfoProcessRequest(this ILogger logger, string prefix, string method);
    }
}
