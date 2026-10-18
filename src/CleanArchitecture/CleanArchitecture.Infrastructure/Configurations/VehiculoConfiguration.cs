using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    internal sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.ToTable("vehiculos");
            builder.HasKey(vehiculo => vehiculo.Id);

            // A nivel de BD los componetes de la entidad Direccion pasaran a formar parte de la tabla vehiculos
            builder.OwnsOne(vehiculo => vehiculo.Direccion);

            builder.Property(vehiculo => vehiculo.Modelo)
                .HasMaxLength(200)
                .HasConversion(modelo => modelo!.Value, value => new Modelo(value));

            builder.Property(vehiculo => vehiculo.Vin)
                .HasMaxLength(500)
                .HasConversion(vin => vin!.Value, value => new Vin(value));

            // Como vehiculo tiene un atributo Moneda, y este a su vez tiene un atributo TipoMoneda
            // Hacemos la conversion de este tipo moneda dentro de un OwnsOne
            builder.OwnsOne(vehiculo => vehiculo.Precio, priceBuilder =>
            {
                priceBuilder.Property(moneda => moneda.TipoMoneda)
                    .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
            });

            builder.OwnsOne(vehiculo => vehiculo.Mantenimiento, priceBuilder =>
            {
                priceBuilder.Property(moneda => moneda.TipoMoneda)
                    .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
            });
        }
    }
}
