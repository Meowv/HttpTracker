$BlazorProjectPath = Get-ChildItem -Path "src\**\*.Blazor.csproj"
$Destination = ".\src\HttpTracker.Dashboard\Blazor"
$PublishPath = ".\src\HttpTracker.Dashboard.Blazor\publish"

function dotnet-build {
	dotnet build -c Release $BlazorProjectPath
}

function dotnet-publish {
	echo "***** $_ *****"
	dotnet publish -c Release $BlazorProjectPath -p:PublishDir=publish
}

function migration-file {
	Remove-Item $Destination -Force -Recurse

	Copy-Item -Path $PublishPath\wwwroot -Destination $Destination -Recurse -Force -Passthru

	Remove-Item $PublishPath -Force -Recurse
}

@( "dotnet-build", "dotnet-publish", "migration-file" ) | ForEach-Object {
    echo ""
    echo "***** $_ *****"
    echo ""

    &$_
    if ($LastExitCode -ne 0) { Exit $LastExitCode }
}