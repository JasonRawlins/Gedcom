@echo off
setlocal enabledelayedexpansion

:: Copy the file "FormatedDeveloperTree.ged" to "DeveloperTree.ged"
copy "%~1" "%~2"

:: Remove leading whitespace
set "tempFile=%~2.tmp"
(
    for /F "usebackq tokens=* delims=" %%i in ("%~2") do (
        echo %%i
    )
) > "%tempFile%"
move /Y "%tempFile%" "%~2"

endlocal