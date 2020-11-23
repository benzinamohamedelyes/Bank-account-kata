FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app

COPY ./out ./
ENTRYPOINT ["dotnet", "Bank-account-kata.dll"]
