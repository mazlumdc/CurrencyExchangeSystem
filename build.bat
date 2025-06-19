@echo off
setlocal

:: Set environment variables
set MSBUILD="C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
set SOLUTION_DIR=%~dp0
set CONFIG=Debug

:: Build projects in correct order
echo Building CurrencyExchangeService...
%MSBUILD% "%SOLUTION_DIR%CurrencyExchangeService\CurrencyExchangeService.csproj" /p:Configuration=%CONFIG% /v:minimal
if errorlevel 1 (
    echo Error building CurrencyExchangeService
    exit /b 1
)

echo Building CurrencyExchangeUI...
%MSBUILD% "%SOLUTION_DIR%CurrencyExchangeUI\CurrencyExchangeUI.csproj" /p:Configuration=%CONFIG% /v:minimal
if errorlevel 1 (
    echo Error building CurrencyExchangeUI
    exit /b 1
)

echo Building CurrencyExchangeHost...
%MSBUILD% "%SOLUTION_DIR%CurrencyExchangeHost\CurrencyExchangeHost.csproj" /p:Configuration=%CONFIG% /v:minimal
if errorlevel 1 (
    echo Error building CurrencyExchangeHost
    exit /b 1
)

echo Build completed successfully!

:: Start the service host in a new window
echo Starting WCF Service Host...
start "WCF Service Host" "%SOLUTION_DIR%CurrencyExchangeHost\bin\Debug\CurrencyExchangeHost.exe"

:: Wait a moment for the service to start
timeout /t 5 /nobreak

:: Start the UI application
echo Starting Currency Exchange UI...
start "Currency Exchange UI" "%SOLUTION_DIR%CurrencyExchangeUI\bin\Debug\CurrencyExchangeUI.exe"

echo Applications started!
echo Press any key to exit...
pause > nul 