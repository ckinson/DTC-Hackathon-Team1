@ECHO OFF

REM The following directory is for .NET 4.0
set DOTNETFX4=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX4%

pushd %~dp0

echo Installing HackIT Traffic simulator service...
echo ---------------------------------------------------
InstallUtil /i "HackIT-simulator.exe"
echo ---------------------------------------------------
echo Done.

net start "HackIT_simulator"

popd

pause>nul