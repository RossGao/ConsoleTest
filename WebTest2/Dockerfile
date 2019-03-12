#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 44902
EXPOSE 44317

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["WebTest2/WebTest2.csproj", "WebTest2/"]
RUN dotnet restore "WebTest2/WebTest2.csproj"
COPY . .
WORKDIR "/src/WebTest2"
RUN dotnet build "WebTest2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebTest2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebTest2.dll"]