using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel.AI.TextCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.Tokenizers;
using ServiceStack.Text;
//using RepoUtils;

namespace Data_Streamin_Open_AI
{
    public class Streaming
    {
        public static async Task RunAsync(string input_prompt)
        {
            await AzureOpenAITextCompletionStreamAsync(input_prompt);
            
        }

        private static async Task AzureOpenAITextCompletionStreamAsync(string input_prompt)
        {
            Console.WriteLine("======== Azure OpenAI - Text Completion - Raw Streaming ========");

            var textCompletion = new AzureTextCompletion("text-davinci-003",
                    "",
                    "");

            await TextCompletionStreamAsync(textCompletion, input_prompt);
        }

        private static async Task OpenAITextCompletionStreamAsync()
        {
            Console.WriteLine("======== Open AI - Text Completion - Raw Streaming ========");

            var textCompletion = new OpenAITextCompletion("text-davinci-003", "OPENAI_API_KEY");

            //await TextCompletionStreamAsync(textCompletion);
        }

        private static async Task TextCompletionStreamAsync(ITextCompletion textCompletion, string input_prompt)
        {
            var requestSettings = new CompleteRequestSettings()
            {
                MaxTokens = 500,
                FrequencyPenalty = 0,
                PresencePenalty = 0,
                Temperature = 1,
                TopP = 0.5
            };

            var prompt = input_prompt;
            List<int> tokens=new List<int>();
            Console.WriteLine("Prompt: " + prompt);
            int token = 0;
            await foreach (string message in textCompletion.CompleteStreamAsync(prompt, requestSettings))
            {
                Console.Write(message);
                token += GPT3Tokenizer.Encode(message).Count();
                tokens.Add(token);
                await Task.Delay(200);
            }

            Console.WriteLine(String.Join(", ", tokens));
        }
    }
}
