# test-app

clone the project and restore it, `dotnet restore`

later go to `TestApp` folder and excecute `dotnet run`

for the project need sqlServer running and go to `appsettings.json` and configure `DefaultConnection` with connection string of sqlServer later go to `Properties/launchSettings.json` and chnge `"commandLineArgs": "seed=False"` for ` "commandLineArgs": "seed=True"` to seed dataBase, after that revert change and run the app
