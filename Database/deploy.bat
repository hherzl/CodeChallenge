cls
set server=(local)
set database=Store
sqlcmd -S %server% -i "database.sql"
pause
