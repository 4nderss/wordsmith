$ErrorActionPreference="SilentlyContinue"
Stop-Transcript | out-null # if something is running already..
$ErrorActionPreference = "Stop"


Start-Transcript -path $PSScriptRoot\Deploy.log

Write-Output "Building application"
dotnet.exe build $PSScriptRoot --configuration Release 2>&1 | out-host

Write-Output "Testing application"
dotnet.exe test $PSScriptRoot\..\..\test\Core.UnitTests --configuration Release 2>&1 | out-host
dotnet.exe test $PSScriptRoot\..\..\test\Service.UnitTests --configuration Release 2>&1 | out-host


If(!$?) {
    Write-Output "Code not ready for deployment.."
    for($i=1; $i -le $error.Count; $i++){ Write-Output $error[$i]}
    Stop-Transcript
    exit 1;
}

dotnet.exe publish $PSScriptRoot --output $PSScriptRoot/dist/ --configuration Release --no-build 2>&1 | Out-Host