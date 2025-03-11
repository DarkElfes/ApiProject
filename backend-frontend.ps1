Start-Process "dotnet" "run --project ApiProject.Server/ApiProject.Server.csproj --launch-profile https" 
Start-Process "dotnet" "run --project ApiProject.Client.Web/ApiProject.Client.Web.csproj --launch-profile https"
