@echo off

REM Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
REM Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

for /f "tokens=* USEBACKQ" %%f in (`type version`) do set version=%%f
set releaseconfig=%1
if "%releaseconfig%" == "" set releaseconfig=Release

:packbin
echo Packing binary...
"%ProgramFiles%\7-Zip\7z.exe" a -tzip %temp%/%version%-bin.zip "..\Figletize\bin\%releaseconfig%\netstandard2.0\*"
"%ProgramFiles%\7-Zip\7z.exe" a -tzip %temp%/%version%-demo.zip "..\Figletize.Cmd\bin\%releaseconfig%\net6.0\*"
if %errorlevel% == 0 goto :complete
echo There was an error trying to pack binary (%errorlevel%).
goto :finished

:complete
move %temp%\%version%-bin.zip
move %temp%\%version%-demo.zip

echo Pack successful.
:finished
