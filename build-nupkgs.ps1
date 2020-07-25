$VersionSuffix = "0.0.1-nightly-20200725"

function dotnet-build {
    if ($VersionSuffix.Length -gt 0) {
        dotnet build -c Release --version-suffix $VersionSuffix
    }
    else {
        dotnet build -c Release
    }
}

function dotnet-pack {
    Get-ChildItem -Path "src\**\*.csproj" | ForEach-Object {
        if(!"$_".contains("Blazor")) {
            if ($VersionSuffix.Length -gt 0) {
                dotnet pack $_ -c Release --no-build --output nupkgs\$VersionSuffix --version-suffix $VersionSuffix
            }
            else {
                dotnet pack $_ -c Release --no-build --output nupkgs
            }
        }
    }
}

function dotnet-nuget-push {
    dotnet nuget push nupkgs\$VersionSuffix\*.nupkg -k "" -s https://nuget.cdn.azure.cn/v3/index.json -t 360 --skip-duplicate
}

@( "dotnet-build", "dotnet-pack", "dotnet-nuget-push" ) | ForEach-Object {
    echo ""
    echo "***** $_ *****"
    echo ""

    &$_
    if ($LastExitCode -ne 0) { Exit $LastExitCode }
}