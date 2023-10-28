FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /app
COPY ./app ./
RUN dotnet restore
RUN dotnet publish -c Release -o /publish


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS migrate
WORKDIR /app
COPY ./app ./
RUN dotnet tool install --global dotnet-ef
RUN echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> $HOME/.bashrc
ENV PATH="/root/.dotnet/tools:${PATH}"
RUN dotnet ef database update


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 5028
ENV ASPNETCORE_URLS=http://+:5028
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_ENVIRONMENT=Development
COPY --from=publish /publish ./
COPY --from=migrate /app/transactions.db ./
ENTRYPOINT ["dotnet", "finance-api.dll"]
