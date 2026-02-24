# Avalonia on .NET Platform

## Prerequisites

Install the .NET platform:

    winget install --id Microsoft.DotNet.SDK.8

Install VS Code:

    winget install --id Microsoft.VisualStudioCode

## Setup Project

Install Avalonia templates:

    dotnet new install Avalonia.Templates

Create the solution:

    dotnet new sln -n Potato

Create the module projects:

    dotnet new classlib -n Potato.Core -f net8.0
    dotnet new avalonia.mvvm -n Potato.Gui -f net8.0

Add module projects to the solution:

    dotnet sln Potato.sln add Potato.Core/Potato.Core.csproj
    dotnet sln Potato.sln add Potato.Gui/Potato.Gui.csproj

Add the refence to the Core to the Gui:

    dotnet add Potato.Gui/Potato.Gui.csproj reference Potato.Core/Potato.Core.csproj

## Build and Run

Build the project:

    dotnet build

Run the application:

    dotnet run --project Potato.Gui/Potato.Gui.csproj

Publisch the application (including .NET):

    dotnet publish Potato.Gui/Potato.Gui.csproj -c Release -r win-x64 --self-contained true
    dotnet publish Potato.Gui/Potato.Gui.csproj -c Release -r win-arm64 --self-contained true

Find the deployable executable in:

    Potato.Gui/bin/Release/net8.0/win-arm64/publish
    Potato.Gui/bin/Release/net8.0/win-arm64/publish
