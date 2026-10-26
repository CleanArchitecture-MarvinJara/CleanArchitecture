using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users.Events;

namespace CleanArchitecture.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        private User() { }

        private User(
            UserId id,
            Nombre nombre,
            Apellido apellido,
            Email email) : base(id)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
        }

        public Nombre? Nombre {  get; private set; }
        public Apellido? Apellido {  get; private set; }
        public Email? Email {  get; private set; }

        // Creamos un metodo estatico para crear un objeto User
        public static User Create(
            Nombre nombre,
            Apellido apellido,
            Email email)
        {
            var user = new User(UserId.New(),nombre,apellido,email);
            // Al crear un user se disparara este evento
            user.RaiseDomainEvent(new UserCreateDomainEvent(user.Id!));
            return user;
        }
    }
}
