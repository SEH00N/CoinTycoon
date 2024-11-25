Write-Output "Copy SharedCode"

$source_folder = "./src/SharedCode/"
$target_folder = "../ProjectCoinClient/Assets/01. Scripts/SharedCode/"

# 대상 폴더 삭제
Remove-Item -Path $target_folder -Recurse -Force

# 폴더 복사
Copy-Item -Path $source_folder -Destination $target_folder -Recurse -Force
