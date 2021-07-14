@ REM Check DotNet installed
@ WHERE dotnet >nul 2>nul
@ IF %ERRORLEVEL% NEQ 0 goto DotNetNotFound

@ REM Check DotNet-Script available in Path
@ WHERE dotnet-script >nul 2>nul
@ IF %ERRORLEVEL% NEQ 0 goto DotNetScriptNotFound

@ REM Pack
@ dotnet-script AutoNest.csx

@ exit /b

@ REM ========================
@ REM DotNet is not Available
@ REM Throw an exception
:DotNetNotFound
@ echo DotNet is not found. 1>&2
@ echo Make sure DotNet is installed 1>&2
@ exit /b 1

@ REM ========================
@ REM DotNet-Script is not Available
@ REM Throw an exception
:DotNetScriptNotFound
@ echo DotNet-Script is not found. 1>&2
@ echo Install by 'dotnet tool uninstall dotnet-script -g' 1>&2
@ exit /b 1
