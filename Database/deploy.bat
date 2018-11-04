cls
set server=(local)
sqlcmd -S %server% -i "database.sql"
pause
