using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Roles
{
    public sealed class Role : Enumeration<Role>
    {

        /** Todos los objetos de tipo Role creados dentro de la clase Role
         *  se van a agregar dentro del diccionario de Enumeration
         */
        public static readonly Role Cliente = new(1, "Cliente");
        public static readonly Role Admin = new(2, "Admin");

        public Role(int id, string name) : base(id, name)
        {
        }

        public ICollection<Permission>? Permissions { get; set; }
    }
}
