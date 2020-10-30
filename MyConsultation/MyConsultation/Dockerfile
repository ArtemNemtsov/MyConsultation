#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MyConsultation/Consultation.csproj", "MyConsultation/"]
COPY ["MyConsultationService/ConsultationService.csproj", "MyConsultationService/"]
COPY ["PostgresDBContext/DBContext.csproj", "PostgresDBContext/"]
RUN dotnet restore "MyConsultation/Consultation.csproj"
COPY . .
WORKDIR "/src/MyConsultation"
RUN dotnet build "Consultation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Consultation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consultation.dll"]