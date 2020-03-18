cd .\src\Tgm.Roborally.Api\
rmdir bin -r
rmdir obj -r
dotnet build
"D:\Programme\JetBrains\apps\Rider\ch-0\193.6494.48\tools\MSBuild\Current\Bin\MSBuild.exe"
nuget add .\bin\Debug\Tgm.Roborally.Api.1.0.0.nupkg -Source "C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\Tgm.Roborally.Api"
pause
wait