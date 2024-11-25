Write-Output "Copy SharedCode"
Copy-Item -Path "./SharedCode/*" -Destination "../ProjectCoinClient/Assets/01. Scripts/SharedCode" -Recurse -Force