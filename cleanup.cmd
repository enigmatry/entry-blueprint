@echo off
dotnet sln remove Enigmatry.Blueprint.Template.csproj
(goto) 2>nul & del "%~f0"
