Param (
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    $dbServerName=$(Throw "Connection string required."),
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    $dbAdminUserName=$(Throw "Admin name required."),
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    $dbAdminPassword=$(Throw "Admin password required."),
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    [string]$loginName=$(Throw "Login name required."),
    [Parameter(Mandatory=$true)]
    [ValidateNotNullOrEmpty()]
    [string]$password=$(Throw "Password required.")
)
Install-Module SQLServer -Confirm:$False -Force
Import-Module SqlServer -DisableNameChecking

$connectionString = "Server=tcp:${dbServerName}.database.windows.net,1433;Persist Security Info=False;User ID=${dbAdminUserName};Password=${dbAdminPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
$sqlConn = New-Object System.Data.SqlClient.SqlConnection $connectionString

$sqlcmd = New-Object System.Data.SqlClient.SqlCommand
$sqlcmd.Connection = $sqlConn
$query = "IF NOT EXISTS (SELECT name FROM sys.sql_logins WHERE name='${loginName}')
    BEGIN
        CREATE LOGIN testLogin WITH PASSWORD='${password}';
    END
ELSE
    BEGIN
        ALTER LOGIN testLogin WITH PASSWORD='${password}';
    END;"
$sqlcmd.CommandText = $query

$sqlConn.Open()
$sqlcmd.ExecuteNonQuery()
$sqlConn.Close()
