@echo off

REM Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
REM Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

REM This script builds documentation and packs the artifacts.

echo Finding DocFX...
if exist %ProgramData%\chocolatey\bin\docfx.exe goto :build
echo You don't have DocFX installed. Download and install Chocolatey and DocFX.
goto :finished

:build
echo Building Documentation...
%ProgramData%\chocolatey\bin\docfx.exe "..\DocGen\docfx.json"
if %errorlevel% == 0 goto :success
echo There was an error trying to build documentation (%errorlevel%).
goto :finished

:success
echo Build and pack successful.
:finished
