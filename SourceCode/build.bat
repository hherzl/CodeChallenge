cls
set initialPath=%cd%
set authApiPath=%initialPath%\Backend\API\AuthAPI
set apiPath=%initialPath%\Backend\API\API
set clientPath=%initialPath%\Frontend\snacks
cd %authApiPath%
start dotnet restore
cd %apiPath%
start dotnet restore
cd %clientPath%
start npm install
cd %initialPath%
