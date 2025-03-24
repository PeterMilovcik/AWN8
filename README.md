# Accessible Web Navigator

## Available Commands

### ExitCommand
- **Purpose**: Exits the application.
- **Usage**: Type "exit" (case-insensitive) to exit the application.

### NavigateCommand
- **Purpose**: Navigates to a specified URL.
- **Usage**: Type "navigate to <URL>" (case-insensitive) to navigate to the specified URL. If the URL does not start with "http://" or "https://", "https://" will be prepended automatically.

## Installation Instructions

To install the Playwright CLI and browsers, use the following dotnet command:

```sh
dotnet tool install --global Microsoft.Playwright.CLI
dotnet playwright install
```
