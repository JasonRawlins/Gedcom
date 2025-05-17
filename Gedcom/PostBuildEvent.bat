echo off
setlocal enabledelayedexpansion

echo "############################"
echo Input file - %1
echo Output file - %2
echo Project directory - %3
echo Destination directory - %4
echo "############################"

echo %1 %2 %3 %4 > "c:\temp\PostBuildEvent results.txt"

set "inputFile=%1"
set "outputFile=%2"

:: Loop over each line in the input file, remove leading whitespace, and write to the output file
for /F "usebackq tokens=* delims=" %%i in ("%inputFile%") do (
    set "line=%%i"
    setlocal enabledelayedexpansion
    :: Remove leading whitespace
    for /L %%j in (1,1,31) do if "!line:~0,1!"==" " set "line=!line:~1!"
    echo !line! >> "%outputFile%"
    endlocal
)

endlocal