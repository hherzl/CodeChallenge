cls
set initialPath=%cd%
set authApiPath=%initialPath%\Backend\API\AuthAPI
set clientPath=%initialPath%\Frontend\snacks
cd %authApiPath%
start dotnet run
set apiPath=%initialPath%\Backend\API\API
cd %apiPath%
start dotnet run
cd %clientPath%
start ng serve
cd %initialPath%
