$BlazorPath = Get-ChildItem -Path "src\**\*.Blazor.csproj"
$PublishPath = ".\src\HttpTracker.Dashboard.Blazor\publish"
$Destination = ".\src\HttpTracker.Dashboard\Blazor"

echo "***** 发布项目 *****"
dotnet publish  $BlazorPath -p:PublishDir=publish

echo "***** 迁移发布文件 *****"
Copy-Item -Path $PublishPath\wwwroot -Destination $Destination -Recurse -Force -Passthru

echo "***** 移除发布文件 *****"
Remove-Item $PublishPath -Force -Recurse

echo "***** done *****"