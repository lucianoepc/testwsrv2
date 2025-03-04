#
#1. Intermediate layer
#
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

#1.1. Copy csproj files and restore dependencies
WORKDIR /src
COPY ["webservices/webservices.csproj", "webservices/"]
COPY ["businesslogic/businesslogic.csproj", "businesslogic/"]
COPY ["common/common.csproj", "common/"]
COPY ["data/data.csproj", "data/"]
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
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

#2.1. Set container port
EXPOSE 8090
ENV ASPNETCORE_URLS=http://+:8090

#2.2. Create non-root user and set working directory
WORKDIR /app
#RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
#USER appuser

#2.2. Copy pubish files
COPY --from=build /app/publish .

#2.3. Add others files and folders
#RUN mkdir -p /app/log
COPY ["webservices/people.db", "."]

#2.4. Only for testing
#RUN apt-get update && apt-get install sudo
#RUN echo "ALL ALL=(ALL) NOPASSWD: ALL" >> /etc/sudoers

#2.5. Set the entrypoint
ENTRYPOINT ["dotnet", "PoC.TestWSrv2.WebServices.dll"]

