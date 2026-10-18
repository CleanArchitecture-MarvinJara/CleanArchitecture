using CleanArchitecture.Application.Abstractions.Clock;

namespace CleanArchitecture.Infrastructure.Clock
{
    // Clase implementación de la interface  IDateTimeProvider del proyecto Applicatioon
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime currentTime => DateTime.UtcNow;
    }
}
