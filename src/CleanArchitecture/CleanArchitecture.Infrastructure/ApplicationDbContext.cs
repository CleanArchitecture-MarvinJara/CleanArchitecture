using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure
{
    // Nota: El IUnitOfWork es el encargado de enviar todos los cambios guardados en la persistencia
    // de EntityFramework hacia la base de datos
    public sealed class ApplicationDbContext: DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
