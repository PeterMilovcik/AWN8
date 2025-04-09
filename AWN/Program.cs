using Microsoft.Extensions.DependencyInjection;
using AWN;
using System.Threading;

var serviceCollection = new ServiceCollection();
serviceCollection.AddServices();
var serviceProvider = serviceCollection.BuildServiceProvider();

var commandInvoker = serviceProvider.GetRequiredService<CommandInvoker>();
var textToSpeechService = serviceProvider.GetRequiredService<TextToSpeechService>();

while (true)
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
