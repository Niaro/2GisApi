@echo off
cd %~dp0
SETLOCAL

:copynuget
IF EXIST .nuget\nuget.exe goto restore
echo Downloading latest version of NuGet.exe...
md .nuget
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://www.nuget.org/nuget.exe' -OutFile '.nuget\nuget.exe'"
copy %CACHED_NUGET% .nuget\nuget.exe > nul

:restore
IF EXIST packages\KoreBuild goto run
.nuget\NuGet.exe install KoreBuild -ExcludeVersion -o packages -nocache -pre
.nuget\NuGet.exe install Sake -version 0.2 -o packages -ExcludeVersion

IF "%SKIP_KRE_INSTALL%"=="1" goto run
CALL packages\KoreBuild\build\kvm upgrade -runtime CLR -x86
CALL packages\KoreBuild\build\kvm install default -runtime CoreCLR -x86

:run
CALL packages\KoreBuild\build\kvm use default -runtime CLR -x86
SET K_BUILD_VERSION=%PackageVersion%
packages\Sake\tools\Sake.exe -I packages\KoreBuild\build -f makefile.shade %*

:push
.nuget\NuGet.exe setApiKey 5abbdd88-9b30-4bbc-8b17-fb983a03c590 -Source https://www.myget.org/F/mindfor/
DEL /S *.symbols.nupkg
FOR /r src\ %%i in (*.nupkg) DO .nuget\NuGet.exe push "%%~i" -Source https://www.myget.org/F/mindfor/