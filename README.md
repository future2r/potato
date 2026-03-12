# Avalonia on .NET Platform

## Introduction

This is a template project for the development of a desktop application:

* .NET Platform
* Avalonia GUI Framework

It is the companion project of

* https://github.com/future2r/tomato
* https://github.com/future2r/gelato

## Prerequisites

Install VS Code:

    winget install --id Microsoft.VisualStudioCode

Install the C# Dev Kit extension:

    code --install-extension ms-dotnettools.csdevkit

Install the .NET platform:

    winget install --id Microsoft.DotNet.SDK.10

## Workspace

Open the workspace in VS Code:

    File > Open Workspace from File... > Potato.code-workspace

This workspace contains editor settings (format-on-save, organize-imports-on-save) and extension recommendations.

Three debug launch configurations are available (F5):

* **Potato** - Default locale
* **Potato (English)** - English locale
* **Potato (German)** - German locale

## Build, Test and Run

Build the project:

    dotnet build

Run the tests:

    dotnet test

Run the application:

    dotnet run --project Potato.Gui/Potato.Gui.csproj

Run the application with a specific language:

    dotnet run --project Potato.Gui/Potato.Gui.csproj -- --lang=en-US
    dotnet run --project Potato.Gui/Potato.Gui.csproj -- --lang=de-DE

Publish the application (including .NET):

    dotnet publish Potato.Gui/Potato.Gui.csproj -c Release -r win-x64 --self-contained true
    dotnet publish Potato.Gui/Potato.Gui.csproj -c Release -r win-arm64 --self-contained true

Find the deployable executable in:

    Potato.Gui/bin/Release/net10.0/win-x64/publish
    Potato.Gui/bin/Release/net10.0/win-arm64/publish

## Project Structure

The project is organized as a .NET solution with three projects:

* `Potato.Core/` - Core library with domain model (`Variety`) and in-memory database (`Database`)
* `Potato.Gui/` - GUI application with Avalonia views and MVVM view models
    * `Resources/` - Localization (English, German)
    * `Assets/` - Application icons
* `Potato.Gui.Tests/` - Unit tests (xUnit, FluentAssertions)
* `Design/` - Design assets (ICO and PNG in various sizes)

## Project Setup

These steps document how the project was created from scratch.

Install Avalonia templates:

    dotnet new install Avalonia.Templates

Create the solution:

    dotnet new sln -n Potato --format slnx

Create the module projects:

    dotnet new classlib -n Potato.Core -f net10.0
    dotnet new avalonia.mvvm -n Potato.Gui -f net10.0

Add module projects to the solution:

    dotnet sln Potato.slnx add Potato.Core/Potato.Core.csproj
    dotnet sln Potato.slnx add Potato.Gui/Potato.Gui.csproj

Add the reference to the Core to the Gui:

    dotnet add Potato.Gui/Potato.Gui.csproj reference Potato.Core/Potato.Core.csproj

### NuGet Packages

Add the required NuGet packages to the Gui project:

    dotnet add Potato.Gui/Potato.Gui.csproj package Avalonia.Controls.DataGrid
    dotnet add Potato.Gui/Potato.Gui.csproj package CommunityToolkit.Mvvm

### Test Project

Create the test project:

    dotnet new xunit -n Potato.Gui.Tests -f net10.0

Add the test project to the solution:

    dotnet sln Potato.slnx add Potato.Gui.Tests/Potato.Gui.Tests.csproj

Add project references to the test project:

    dotnet add Potato.Gui.Tests/Potato.Gui.Tests.csproj reference Potato.Gui/Potato.Gui.csproj
    dotnet add Potato.Gui.Tests/Potato.Gui.Tests.csproj reference Potato.Core/Potato.Core.csproj

Add the required NuGet packages to the test project:

    dotnet add Potato.Gui.Tests/Potato.Gui.Tests.csproj package FluentAssertions
