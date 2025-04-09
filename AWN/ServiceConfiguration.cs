using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace AWN
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            var playwright = Playwright.CreateAsync().GetAwaiter().GetResult();
            var browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false })
                                    .GetAwaiter().GetResult();
            var page = browser.NewPageAsync().GetAwaiter().GetResult();

            services.AddSingleton(page);
            services.AddSingleton<CommandInvoker>();
            services.AddSingleton<ICommand, ExitCommand>();
            services.AddSingleton<ICommand, NavigateCommand>();
            services.AddSingleton<OpenAiService>();
            services.AddSingleton<ICommand, AiCommand>();
            services.AddSingleton<TextToSpeechService>();
            // Register other commands here
        }
    }
}
