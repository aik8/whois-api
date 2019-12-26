## Build Stage ##
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# Set the workdir.
WORKDIR /app

# Copy csproj and resotre as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

## Final Stage ##
FROM mcr.microsoft.com/dotnet/core/sdk:3.1

# Set the workdir.
WORKDIR /app

# Copy your built stuff over.
COPY --from=build /app/out .

# Expose the serivce.
EXPOSE 5000

# Define the entry point.
ENTRYPOINT ["/app/kow-whois-api"]

