FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CalendarEvent/CalendarEvents.csproj", "CalendarEvent/"]
COPY ["CalendarEvents.Services/CalendarEvents.Services.csproj", "CalendarEvents.Services/"]
COPY ["CalendarEvents.DataAccess/CalendarEvents.DataAccess.csproj", "CalendarEvents.DataAccess/"]
COPY ["CalendarEvents.Models/CalendarEvents.Models.csproj", "CalendarEvents.Models/"]
COPY ["CalendarEvents.Scrapper/CalendarEvents.Scrapper.csproj", "CalendarEvents.Scrapper/"]
RUN dotnet restore "CalendarEvent/CalendarEvents.csproj"
COPY . .
WORKDIR "/src/CalendarEvent"
RUN dotnet build "CalendarEvents.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CalendarEvents.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
# Add wait-for-it for db startup waitning
# ADD https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh /app/wait-for-it.sh
COPY "wait-for-it.sh" "wait-for-it.sh"
RUN chmod +x ./wait-for-it.sh

# ENTRYPOINT ["dotnet", "CalendarEvents.dll"]
