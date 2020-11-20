FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app
# Install NodeJs
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
apt-get install -y build-essential nodejs
# End Install
# copy csproj and restore as distinct layers
COPY Bank-account-kata/*.sln ./Bank-account-kata/
COPY Bank-account-kata/*.csproj ./Bank-account-kata/
COPY BankAccount.Domain/*.csproj ./BankAccount.Domain/
COPY BankAccount.Data/*.csproj ./BankAccount.Data/
COPY BankTests/*.csproj ./BankTests/

WORKDIR /app/Bank-account-kata/
RUN dotnet restore

WORKDIR /app/
# copy everything else and build app
COPY Bank-account-kata/. ./Bank-account-kata/
COPY BankAccount.Domain/. ./BankAccount.Domain/
COPY BankAccount.Data/. ./BankAccount.Data/
COPY BankTests/. ./BankTests/

WORKDIR /app/Bank-account-kata
RUN dotnet build -c release  
RUN dotnet publish -c release --no-build -o ./out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build /app/Bank-account-kata/out ./
ENTRYPOINT ["dotnet", "Bank-account-kata.dll"]
