set /p serverName="Enter DB server name: "
sqlcmd -S %serverName% -i create.sql 
dotnet restore ..\\src\\PresentationLayer\\PresentationLayer.csproj
dotnet build --configuration Development --output ..\\TaskManagerBuild ..\\src\\PresentationLayer\\PresentationLayer.csproj
cd ..\\TaskManagerBuild
start PresentationLayer.exe
start https://localhost:5001