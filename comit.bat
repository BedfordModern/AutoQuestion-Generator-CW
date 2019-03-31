echo.
echo This will commit my git every 5 Minutes... 
F:
cd "F:\CS Coursework\AutoQuestion-Generator-CW"
:timer
  echo.
  git add -A
  git commit -m "Autocommit"
  git push
  echo.
  TIMEOUT /T 300 /NOBREAK
GOTO timer

pause >nul