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

### ClickCommand
- **Purpose**: Clicks a specified element on the web page.
- **Usage**: Type a command containing the substring "click" followed by a description of the target element. The command will generate a locator string from the HTML content and target element description, and click the element using Playwright.

### TypeCommand
- **Purpose**: Types text into a specified HTML element.
- **Usage**: Type "type <text>" (case-insensitive) to type the specified text into the target HTML element. The command will remove the "type " prefix and use Playwright to type the text into the page.

### TextToSpeechService
- **Purpose**: Converts text to speech using OpenAI's text-to-speech API.
- **Usage**: The service is automatically used in the command loop to convert output text to speech while retaining console output. Press "Enter" to cancel playback.

### SpeechToTextService
- **Purpose**: Converts speech to text using OpenAI's whisper-1 model and NAudio for audio capture.
- **Usage**: The service is automatically used in the command loop to convert voice input to text. Press "Enter" to start and stop recording. The transcribed text is then used as command input.

## Installation Instructions

To install the Playwright CLI and browsers, use the following dotnet command:

```sh
dotnet tool install --global Microsoft.Playwright.CLI
dotnet playwright install
```
