using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AWN
{
    public class AiCommand : ICommand
    {
        private readonly OpenAiService _openAiService;
        private readonly IPage _page;

        public AiCommand(OpenAiService openAiService, IPage page)
        {
            _openAiService = openAiService;
            _page = page;
        }

        public bool CanExecute(string commandInput)
        {
            return !string.IsNullOrEmpty(commandInput);
        }

        public async Task<string> ExecuteAsync(string commandInput)
        {
            string pageText = await _page.InnerTextAsync("body");
            return await _openAiService.GenerateResponseAsync(commandInput, pageText);
        }
    }
}
