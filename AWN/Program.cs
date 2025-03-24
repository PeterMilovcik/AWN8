using Microsoft.Extensions.DependencyInjection;
using AWN;

var serviceCollection = new ServiceCollection();
serviceCollection.AddServices();
var serviceProvider = serviceCollection.BuildServiceProvider();

var commandInvoker = serviceProvider.GetRequiredService<CommandInvoker>();

while (true)
{
    var input = Console.ReadLine();
    if (input != null)
    {
        var result = await commandInvoker.ExecuteCommandAsync(input);
        Console.WriteLine(result);
    }
}
