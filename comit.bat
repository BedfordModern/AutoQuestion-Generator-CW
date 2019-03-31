echo.
echo This will commit my git every 5 Minutes... 

:timer
  echo.
  gc.bat
  echo.
  TIMEOUT /T 300 /NOBREAK
goto:timer

pause >nul