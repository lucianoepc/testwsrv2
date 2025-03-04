Esto es un ejemplo para generar 1 imagen que exponse un servicio web y sera usado SOLO para pruebas.

# Estructura de la solución

- El proyecto de generación de una imagen es una solución, que está en git

- El proyecto principal de la solucion es lo que programa entry-point de la imagen.

- Los proyectos adicionales de la solucion son: acceso a datos, lógica de negocio, las definiciones comunes, etc.

- Crear la estructura de folderes

```bash
cd ~/code/lucianoepc_gmail/poc/net/webservice/testwsrv2

#Crear una un archivo solución 'testwsrv2.sln' en la ruta actual
dotnet new sln --name testwsrv2

#Crea el proyecto de tipo webapi en folder './webservices' (archivo 'webservices.csproj') y adicionarlo a la solucion
dotnet new webapi -o ./webservices
dotnet sln add ./webservices

#Crea el proyecto de tipo classlib en folder './common' (archivo 'common.csproj') y adicionarlo a la solucion
dotnet new classlib -o ./common
dotnet sln add ./common

#Crea el proyecto de tipo classlib en folder './data' (archivo 'data.csproj') y adicionarlo a la solucion
dotnet new classlib -o ./data
dotnet sln add ./data

#Crea el proyecto de tipo classlib en folder './businesslogic' (archivo 'businesslogic.csproj') y adicionarlo a la solucion
dotnet new classlib -o ./businesslogic
dotnet sln add ./businesslogic

#Listar la proyectos de la solucion
dotnet sln list

#Crear el .gitignore
dotnet new gitignore
```

- En cada proyecto `.csproj` adicionar sus proyectos dependientes entre si. Por ejemplo:

```xml
<ItemGroup>
    <ProjectReference Include="../uc.core.common/uc.core.common.csproj" />
    <ProjectReference Include="../common/common.csproj" />
    <ProjectReference Include="../data/data.csproj" />
    <ProjectReference Include="../businesslogic/businesslogic.csproj" />
</ItemGroup>
```

- En cada proyecto, cambiar el espacio de nombres por defecto y el nombre de salida del binario 
  - Se esta indicando que el nombre del binario coincida con el nampespace por defecto

```xml
<PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>        
    <RootNamespace>PoC.TestWSrv2.WebServices</RootNamespace>
    <AssemblyName>PoC.TestWSrv2.WebServices</AssemblyName>
</PropertyGroup>
```

# Configuraciones basica

- Archivo `.gutctags`

```bash
vim .gutctags
```

```
--exclude=commom/bin
--exclude=commom/obj
--exclude=businesslogic/bin
--exclude=businesslogic/obj
--exclude=data/bin
--exclude=data/obj
--exclude=webservices/bin
--exclude=webservices/obj
--exclude=webservices/log
--exclude=out
```

- Adicionar al principio del `.gitignore`:

```bash
vim .gitignore
```

```gitignore
#---------------------------------------------------------------------------
# Ignore for VIM
#---------------------------------------------------------------------------

# VIM - Archivos swap o temporales (formato '.filename.swp')
**/*.swp
**/*.swo

# VIM - Archivos de 'persistence undo' (formato '.filename.un~')
**/*.un~

# Log generados por: TMUX, ..
*.log

# Archivos internos de GIT
.git/*

# Archivos de tag generado por Universal CTags
tags
tags.lock

# Paquetes locales de NodeJS (binarios de NodeJs)
node_modules/*


# Mi carpeta de salida de binarios para pruebas internas
[Oo]ut/

#---------------------------------------------------------------------------
# Ignore for Visual Studio
#---------------------------------------------------------------------------
```

- Crear un repositorio git

```bash
git init
```

- Adicionar un Dockerfile

```bash
vim Dockerfile
```

```dockerfile
#
#1. Intermediate layer
#
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS lbuild

#1.1. Copy all csproj files and restore dependencies
WORKDIR /src
COPY ["webservices/webservices.csproj", "webservices/"]
RUN dotnet restore "webservices/webservices.csproj"

#1.2. Copy the rest of the code (use un ".dockerignore")
COPY . .

#1.3. Build the application
RUN dotnet build "webservices/webservices.csproj" -c Release -o /app/build

#1.4. Publish the application
RUN dotnet publish "webservices/webservices.csproj" -c Release -o /app/publish /p:UseAppHost=false



#
#2. Final layer
#
FROM mcr.microsoft.com/dotnet/aspnet:8.0

#2.1. Set container port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

#2.2. Create non-root user and set working directory
WORKDIR /app
#RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
#USER appuser

#2.2. Copy pubish files
COPY --from=lbuild /app/publish .

#2.3. Add others files and folders
RUN mkdir -p /app/log

#2.4. Only for testing
#RUN apt-get update && apt-get install sudo
#RUN echo "ALL ALL=(ALL) NOPASSWD: ALL" >> /etc/sudoers

#2.5. Set the entrypoint
ENTRYPOINT ["dotnet", "PoC.TestWSrv2.WebServices.dll"]
```

- Adicionar un `.dockerignore`

```bash
vim .dockerignore 
```

```
**/obj
**/bin
**/node_modules
**/charts
**/.vs
**/.vscode
**/.classpath
**/.dockerignore
**/.env
**/.git
**/.gitignore
**/.project
**/.settings
**/.toolstarget
**/*.*proj.user
**/*.dbmdl
**/*.jfm
**/docker-compose*
**/compose*
**/Dockerfile*
**/npm-debug.log
**/secrets.dev.yaml
**/values.dev.yaml
README.md
```

# Habilitar `NLog` (no se esta usando)

- Consideraciones para habilitar NLog para la solución
  
  - La mayoria de los proyectos solo requieren acceder a MEL (capa de abstracion de Microsoft)
  - El proyecto principal (el webservice) requier configurar NLog, por ello este siempre requiere adiciona la libreria de NLog.
  - El archivo de configuracion de NLog se realiza solo en el proyecto principal

- En el proyecto principal, crear el archivo de configuracion para NLog

```bash
mkdir webservices/log
vim webservices/nlog.config
```

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xsi:schemaLocation="NLog NLog.xsd"
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    autoReload="true"
    throwExceptions="false"
    internalLogLevel="Info"
    internalLogFile="${basedir}/log/internal.log" >

    <variable name="defaultLayout2" value="${longdate} ${level} [${threadname:whenEmpty=${threadid}}] ${logger} - ${message} ${exception:format=tostring}"/>
    <variable name="defaultLayout" value="${longdate} ${level} [${threadid}] ${logger} - ${message} ${exception:format=tostring}"/>

    <!-- the targets to write to -->
    <targets>
        <!-- write logs to file -->
        <target xsi:type="File" name="logfile" fileName="${basedir}/log/myapp.log"
            layout="${var:defaultLayout}" />
        <!-- write logs to console -->
        <target xsi:type="Console" name="logconsole"
            layout="${var:defaultLayout}" />
    </targets>

    <!-- rules to map from logger name to target -->
    <rules>
        <logger name="UC.Core.Job.*" minlevel="Trace" writeTo="logconsole" />
        <logger name="*" minlevel="Debug" writeTo="logfile,logconsole" />
    </rules>
</nlog>
```

- Adicionar el paquete en el de proyecto '.csproj' que lo requieren (generalmente el proyecto principal)

```bash
dotnet add ./webservices package NLog.Extensions.Logging
```

- Tambien puede adicionar (colocar el principal y este resolver y descargara todos las referencias que requiere):

```xml
<ItemGroup>
    <PackageReference Include="NLog.Extensions.Logging" Version="5.3.5" />
</ItemGroup>
```

```bash
dotnet restore
```

- En el proyecto principal, indicar que el archivo de configuracion siempre se copia a la salida de los binarios compilados.

```xml
<ItemGroup>
   <None Update="nlog.config" CopyToOutputDirectory="Always" />
</ItemGroup>
```

- En el proyecto principal, en el archivo 'Program.cs' indicar el log por defecto que debe usar MEL

```cs
//Si usas WebHost:

using NLog.Extensions.Logging;

//. . . . . . . . . . . . . . . . . . . 
//. . . . . . . . . . . . . . . . . . . 


builder.Service.AddLogging(loggingBuilder =>
    {
        //Eliminar los proveedores de logging configurados por defecto por el default builder
        loggingBuilder.ClearProviders();

        loggingBuilder.AddNLog();

    });
```

```cs
//Si usas GenericHost:

using NLog.Extensions.Logging;

//. . . . . . . . . . . . . . . . . . . 
//. . . . . . . . . . . . . . . . . . . 


builder.ConfigureLogging(loggingBuilder =>
    {
        //Eliminar los proveedores de logging configurados por defecto por el default builder
        loggingBuilder.ClearProviders();

        loggingBuilder.AddNLog();

    });
```

- Opcionalmente, Si desea tener control de log antes y despues que el servio Host de .Net este funcionando, cree un logger estatico solo para este escenarios

```cs
using NLog;
using NLog.Extensions.Logging;
using UC.Core.Job;

public class Program
{
    private static NLog.Logger sLogger= NLog.LogManager.CreateNullLogger();

    public static async Task Main(string[] pArgs)
    {
        //1. Crear logger para la clase principal 'Program'
        sLogger = LogManager.GetCurrentClassLogger();


        try
        {
            sLogger.Log(NLog.LogLevel.Debug, "Iniciando el programa...");

            IHost host= Program.sCreateHost(pArgs);

            await Program.sRunHost(host);

            sLogger.Log(NLog.LogLevel.Debug, "El programa Finalizó...");
        }
        catch (Exception ex)
        {
            sLogger.Error(ex, "Ocurrio un error al iniciar el programa");
            //throw ex;
        }
        finally
        {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
        }

    }

    private static IHost sCreateHost(string[] pArgs)
    {
        //1. Creando el HostBuilder
        var builder= Host.CreateDefaultBuilder(pArgs);

        //2. Configuraciones del Host
        //builder.ConfigureHostConfiguration(hostConfig =>
        //    {
        //        hostConfig.SetBasePath(Directory.GetCurrentDirectory());
        //        hostConfig.AddJsonFile("hostsettings.json", optional: true);
        //        hostConfig.AddEnvironmentVariables(prefix: "PREFIX_");
        //        hostConfig.AddCommandLine(args);
        //    });

        //builder.UseContentRoot("/path/to/content/root");
        //builder.UseEnvironment("Development");

        //2. Configurando el Logging
        builder.ConfigureLogging(loggingBuilder =>
            {
                //Eliminar los proveedores de logging configurados por defecto por el default builder
                loggingBuilder.ClearProviders();

                //Adicionar lo proveedor de log deseados
                //loggingBuilder.AddSimpleConsole(options =>
                //    {
                //        options.SingleLine= true;
                //        options.TimestampFormat= "yyyy/MM/dd HH:mm:ss.ffffff ";
                //    });

                //loggingBuilder.AddSystemdConsole(options =>
                //    {
                //        options.TimestampFormat= "yyyy/MM/dd HH:mm:ss.ffffff ";
                //    });

                loggingBuilder.AddNLog();

            });


        //3. Configuracion de los servicios adicionales del HostBuilder
        builder.ConfigureServices((context, services) =>
            {
                //Configuración de opciones del host para el servicio
                services.Configure<HostOptions>(options =>
                    {
                        //Periodo de gracia para terminar todos los servicios hospedados (por defecto es 5s) para un cierro controlado del host.
                        options.ShutdownTimeout= TimeSpan.FromSeconds(15);

                        //Comportamiento del host en caso que ocurra una excepción en el servicio hospedados (por defecto: StopHost)
                        //options.BackgroundServiceExceptionBehavior= BackgroundServiceExceptionBehavior.Ignore
                    });

                //Adicionando servicios: Los hosted services
                services.AddSingleton<JobGroupBuilder>(pServiceProvider => {
                        //Creando el objeto
                        return new MssqlJobBuilder("Group01",
                            pServiceProvider.GetRequiredService<ILogger<MssqlJobBuilder>>(),
                            pServiceProvider.GetRequiredService<ILoggerFactory>(),
                            pServiceProvider.GetRequiredService<IConfiguration>()
                            );
                    });

                //Adicionando servicios: Los que se inyectaran (como parametros del contructor) del host services
                services.AddHostedService<HostedJobGroup>();

                //services.AddScoped();
            });

        //3. Instanciando el host y ejecutando los servicios hospedados
        sLogger.Log(NLog.LogLevel.Debug, "Construyendo Host con el builder...");
        IHost host= builder.Build();
        return host;
    }

    private static async Task sRunHost(IHost pHost)
    {
        sLogger.Log(NLog.LogLevel.Debug, "Ejecutando asincronamente el Host y esperando el resultado ...");
        await pHost.RunAsync();
    }

}
```

# Compilar y Ejecutar localmente

```bash
cd ~/code/lucianoepc_gmail/poc/net/webservice/testwsrv2


#Limpiar y reconstuir toda la solución
dotnet clean
dotnet restore

#Compilar la solucion
dotnet build

#Ejecutar en debug el proyecto principal y sus dependencias
dotnet run --project webservices

#Publicar en realease y ejecutar
rm out/*
mkdir -p out/log
dotnet publish webservices -c Release -o out
dotnet ./out/PoC.EasySrvs.WebServices.dll
```

# Creacion de la imagen

```bash
#Ir al directorio de contexto (o worker directory)
cd ~/code/lucianoepc_gmail/poc/net/webservice/testwsrv2

#Crear la imagen usando el docker file en "webservices/Dockerfile"
podman build -f ./Dockerfile -t lucianoepc/testwsrv:1.0-net9_t02 .
podman build -t lucianoepc/testwsrv:1.0-net9_t02 .

#Subir la imagen al DockerHub
podman login -u lucianoepc
podman push lucianoepc/testwsrv:1.0-net9_t02
```

# Uso de la imagen

- Para ver los metodos HTTP expuestos, vease `./webservices/examples_curl.md` y `./webservices/examples_rest.http`.
