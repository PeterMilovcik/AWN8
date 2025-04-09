# Accessible Web Navigator

## Available Commands

### ExitCommand
- **Purpose**: Exits the application.
- **Usage**: Type "exit" (case-insensitive) to exit the application.

### NavigateCommand
- **Purpose**: Navigates to a specified URL.
- **Usage**: Type "navigate to <URL>" (case-insensitive) to navigate to the specified URL. If the URL does not start with "http://" or "https://", "https://" will be prepended automatically.

### AiCommand
- **Purpose**: Generates a response based on a prompt and the web page content.
- **Usage**: Type any non-empty input to generate a response based on the prompt and the web page content.

### TextToSpeechService
- **Purpose**: Converts text to speech using OpenAI's text-to-speech API.
- **Usage**: The service is automatically used in the command loop to convert output text to speech while retaining console output. Press "Enter" to cancel playback.

## Installation Instructions

To install the Playwright CLI and browsers, use the following dotnet command:

```sh
dotnet tool install --global Microsoft.Playwright.CLI
dotnet playwright install
```
