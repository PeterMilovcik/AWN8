using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenAI;
using OpenAI.Chat;

namespace AWN
{
    public class OpenAiService
    {
        private readonly ChatClient _chatClient;

        public OpenAiService()
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAI API key is not set.");
            }

            _chatClient = new ChatClient("gpt-4o-mini", apiKey);
        }

        public async Task<string> GenerateResponseAsync(string commandInput, string pageTextContent)
        {
            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a knowledgeable assistant for web page content."),
                new UserChatMessage($"Generate a clear, concise response based on the prompt:\n\n{commandInput}\n\nWeb Page Content:\n\n{pageTextContent}")
            };

            try
            {
                ChatCompletion completion = await _chatClient.CompleteChatAsync(messages);
                return completion.Content[0].Text;
            }
            catch (Exception ex)
            {
                return $"Error generating response: {ex.Message}";
            }
        }

        public async Task<string> GenerateLocatorStringAsync(string htmlContent, string elementDescription)
        {
            var prompt = $"Given the HTML content: {htmlContent} and a target element described as {elementDescription}, identify the most precise and unique selector string that can be used with page.Locator in Playwright. Output only the locator string as plain text, ensuring it is not wrapped in any formatting or code blocks.";
            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a knowledgeable assistant for web page content."),
                new UserChatMessage(prompt)
            };

            try
            {
                ChatCompletion completion = await _chatClient.CompleteChatAsync(messages);
                return completion.Content[0].Text.Trim();
            }
            catch (Exception ex)
            {
                return $"Error generating locator string: {ex.Message}";
            }
        }
    }
}
