using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos
{
    public class VehiculoErrors
    {
        public static Error NotFound = new(
            "Vehiculo.NotFound",
            "No existe un vehiculo con este id"
        );

    }
}