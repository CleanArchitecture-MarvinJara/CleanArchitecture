using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using Dapper;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    internal sealed class SearchVehiculosQueryHandler 
        : IQueryHandler<SearchVehiculosQuery, IReadOnlyList<VehiculoResponse>>
    {
        private static readonly int[] ActiveAlquilerStatuses =
        [
            (int)AlquilerStatus.Reservado,
            (int)AlquilerStatus.Confirmado,
            (int)AlquilerStatus.Completado
        ];

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SearchVehiculosQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(
            SearchVehiculosQuery request, 
            CancellationToken cancellationToken)
        {
            if (request.fechaInicio > request.fechaFin)
            {
                return new List<VehiculoResponse>();
            }

            using var connection = _sqlConnectionFactory.CreateConnection();

            // Esta condicion me trae aquellos vehiculos que no esten en el bloque de alquileres
            const string sql = """
                    SELECT
                        a.id AS Id,
                        a.modelo AS Modelo,
                        a.vin AS Vin,
                        a.precio_monto AS Precio,
                        a.precio_tipo_moneda AS TipoMoneda,
                        a.direccion_pais AS Pais,
                        a.direccion_departamento AS Departamento,
                        a.direccion_provincia AS Provincia,
                        a.direccion_ciudad AS Ciudad,
                        a.direccion_calle AS Calle
                    FROM vehiculos AS a
                    WHERE NOT EXISTS
                    (
                        SELECT 1
                        FROM alquileres AS b
                        WHERE
                            b.vehiculo_id = a.id AND
                            b.duracion_inicio <= @EndDate AND
                            b.duracion_fin >= @StartDate AND
                            b.status = ANY(@ActiveAlquilerStatuses)
                    )
                """;

            var vehiculos = await connection
                .QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>
                (
                    sql,
                    (vehiculo, direccion) =>
                    {
                        //Todos los resultados de direccion se van a agregar a la propiedad Direccion del objeto Vehiculo
                        vehiculo.Direccion = direccion;
                        return vehiculo;
                    },
                    new
                    {
                        StartDate = request.fechaInicio,
                        EndDate = request.fechaFin,
                        ActiveAlquilerStatuses
                    },
                    splitOn: "Pais"
                );

            return vehiculos.ToList();
        }
    }
}
