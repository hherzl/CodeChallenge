cls
set initialPath=%cd%
set authApiPath=%initialPath%\Backend\API\AuthAPI
cd %authApiPath%
start dotnet run
echo AuthAPI is listening on port number 5600
set apiPath=%initialPath%\Backend\API\API
cd %apiPath%
start dotnet run
echo API is listening on port number 5700
cd %initialPath%
