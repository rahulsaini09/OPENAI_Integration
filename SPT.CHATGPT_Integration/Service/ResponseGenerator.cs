using OpenAI_API;

namespace SPT.CHATGPT_Integration.Service
{
    public class ResponseGenerator
    {
        public async Task<List<string>> GenerateContent(string query)
        {
            //https://platform.openai.com/account/api-keys

            var apiKey = "enter-your-api-key";
            var apiModel = "text-davinci-003";
            List<string> rq = new();

            OpenAIAPI api = new(new APIAuthentication(apiKey));
            var completionRequest = new OpenAI_API.Completions.CompletionRequest()
            {
                Prompt = query,
                Model = apiModel,
                Temperature = 0.5, //more varied responses
                MaxTokens = 300, // maximum length of the generated text.
                TopP = 1.0,
                FrequencyPenalty = 0.0,
                PresencePenalty = 0.0,
            };

            try
            {
                var result = await api.Completions.CreateCompletionsAsync(completionRequest);
                foreach (var response in result.Completions)
                {
                    if (!string.IsNullOrEmpty(response.Text))
                    {
                        response.Text = response.Text.TrimStart('\n');
                    }
                    rq.Add(response.Text);
                }
            }
            catch (Exception ex)
            {
                rq.Add($"Error:{ex.Message}");
            }
            return rq;
        }
    }
}
