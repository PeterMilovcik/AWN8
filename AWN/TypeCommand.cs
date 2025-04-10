using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace AWN
{
    public class TypeCommand : ICommand
    {
        private readonly IPage _page;

        public TypeCommand(IPage page)
        {
            _page = page;
        }

        public bool CanExecute(string commandInput)
        {
            return commandInput.StartsWith("type ", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> ExecuteAsync(string commandInput)
        {
            string textToType = commandInput.Substring(5).Trim();
            await _page.Keyboard.TypeAsync(textToType);
            return "Text typed successfully.";
        }
    }
}
