cls
set initialPath=%cd%
set authApiPath=%initialPath%\Backend\API\AuthAPI
set apiPath=%initialPath%\Backend\API\API
set clientPath=%initialPath%\Frontend\snacks
cd %authApiPath%
start dotnet run
cd %apiPath%
start dotnet run
cd %clientPath%
start ng serve
cd %initialPath%
