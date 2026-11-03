# Version
- La versión de SDK de este proyecto corresponde a la versión 8.0.301
- Obtenida de la pagina oficial de Microsoft de .NET 8.0 versión 8.0.6

# Levantar contenedores
Para levantar los contenedores es necesario crear un archivo .env en la raíz del proyecto con las siguientes variables de entorno:
```
DB_NAME=
DB_USER=
DB_PASSWORD=
```

Agregas los valores correspondientes a tu base de datos y luego ejecutas el siguiente comando:
```
docker-compose up --build -d
```

Luego de ejecutar el comando anterior, revisas que los contenedores esten corriendo correctamente, el contenedor del proyecto asp.net core
estara detenido a la espera del funcionamiento de la base de datos, usas la aplicacion de docker para ejecutar el contenedor del proyecto y 
este se levantara correctamente.

# Migraciones de base de datos
Para la migracion de base de datos se tendra una carpeta migrations en la solucion Infrastructure, en donde estaran los archivos 
ApplicationDbContextModelSnapshot e InitialCreate, responsables de la construccion de las tablas en la base de datos a partir de los models
de la carpeta Domain, asi como la insercion de data de prueba.
Para agregar nuevos cambios en la base de datos se recomienda eliminar la carpeta migrations y generar una nueva con el comando:
```
dotnet ef --verbose migrations add InitialCreate -p src/CleanArchitecture/CleanArchitecture.Infrastructure -s src/CleanArchitecture/CleanArchitecture.Api
```
Tambien se recomienda eliminar todos los contenedores del proyecto y volver a construirlos, segun se explico en el paso previo.
Es entonces que podemos ejecutar el proyecto en local.

# Ejecutar el proyecto en local
Para correr el proyecto en local, es necesario tener las credenciales de conexion a la base de datos en el archivo appsettings.json, o en un 
secreto de usuario, **no subas el connection string a un repositorio publico, ya que contiene información sensible**.
La estructura del connection string es la siguiente:
```
"ConnectionStrings": {
  "ConnectionString": "Host={HOST};Port={PORT};Database={DATABASE};Username={USERNAME};Password={PASSWORD}"
}
```
Reemplace los valores entre llaves con los correspondientes a su base de datos.
Ejecute el proyecto con el IDE de su preferencia o ubiquese en la carpeta root del proyecto y use el siguiente comando:
```
dotnet run --project src/CleanArchitecture/CleanArchitecture.Api
```