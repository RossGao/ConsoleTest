#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 46457
EXPOSE 44389

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["WebTest3/WebTest3.csproj", "WebTest3/"]
RUN dotnet restore "WebTest3/WebTest3.csproj"
COPY . .
WORKDIR "/src/WebTest3"
RUN dotnet build "WebTest3.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebTest3.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebTest3.dll"]