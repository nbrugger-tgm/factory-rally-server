cd .\src\Tgm.Roborally.Api\
del bin /F /S /Q
del obj /F /S /Q
dotnet build
dotnet pack
"D:\Programme\JetBrains\apps\Rider\ch-0\193.6494.48\tools\MSBuild\Current\Bin\MSBuild.exe"
del "C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\Tgm.Roborally.Api" /F /S /Q
nuget add .\bin\Debug\Tgm.Roborally.Api.1.0.0.nupkg -Source "C:\Program Files (x86)\Microsoft SDKs\NuGetPackages"
pause
wait