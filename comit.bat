echo.
echo This will commit my git every 5 Minutes... 
cd "F:\CS Coursework\AutoQuestion-Generator-CW"
:timer
  echo.
  gc.bat
  echo.
  TIMEOUT /T 300 /NOBREAK
GOTO timer

pause >nul