@echo off

REM Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.
REM Copyright 2023-2024 - Aptivi. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

REM This script builds KS documentation and packs the artifacts. Use when you have VS installed.
for /f "tokens=* USEBACKQ" %%f in (`type version`) do set version=%%f

:pack
echo Packing documentation...
"%ProgramFiles%\WinRAR\rar.exe" a -ep1 -r -m5 %temp%/%version%-doc.rar "..\docs\"
if %errorlevel% == 0 goto :finalize
echo There was an error trying to pack documentation (%errorlevel%).
goto :finished

:finalize
rmdir /S /Q "..\DocGen\api\"
rmdir /S /Q "..\DocGen\obj\"
rmdir /S /Q "..\docs\"
move %temp%\%version%-doc.rar
echo Build and pack successful.
goto :finished

:finished
pause
