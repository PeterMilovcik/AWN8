using Microsoft.Extensions.DependencyInjection;
using AWN;
using System.Threading;

var serviceCollection = new ServiceCollection();
serviceCollection.AddServices();
var serviceProvider = serviceCollection.BuildServiceProvider();

var commandInvoker = serviceProvider.GetRequiredService<CommandInvoker>();
var textToSpeechService = serviceProvider.GetRequiredService<TextToSpeechService>();
var speechToTextService = serviceProvider.GetRequiredService<SpeechToTextService>();

while (true)
{
    Console.WriteLine("Choose input method: (1) Text (2) Voice");
    var inputMethod = Console.ReadLine();

    if (inputMethod == "1")
    {
        var input = Console.ReadLine();
        if (input != null)
        {
            var result = await commandInvoker.ExecuteCommandAsync(input);
            Console.WriteLine(result);

            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                var textToSpeechTask = textToSpeechService.ConvertTextToSpeechAsync(result, cancellationTokenSource.Token);

                Console.WriteLine("Press Enter to cancel text-to-speech playback.");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    cancellationTokenSource.Cancel();
                }

                await textToSpeechTask;
            }
        }
    }
    else if (inputMethod == "2")
    {
        Console.WriteLine("Press Enter to start recording...");
        Console.ReadLine();

        var recordingTask = Task.Run(() => speechToTextService.CaptureAudio("input.mp3"));

        Console.WriteLine("Press Enter to stop recording...");
        Console.ReadLine();

        await recordingTask;

        var transcription = await speechToTextService.TranscribeAudioAsync("input.mp3");
        Console.WriteLine($"Transcription: {transcription}");

        var result = await commandInvoker.ExecuteCommandAsync(transcription);
        Console.WriteLine(result);

        using (var cancellationTokenSource = new CancellationTokenSource())
        {
            var textToSpeechTask = textToSpeechService.ConvertTextToSpeechAsync(result, cancellationTokenSource.Token);

            Console.WriteLine("Press Enter to cancel text-to-speech playback.");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                cancellationTokenSource.Cancel();
            }

            await textToSpeechTask;
        }
    }
    else
    {
        Console.WriteLine("Invalid input method. Please choose (1) Text or (2) Voice.");
    }
}
