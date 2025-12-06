using Mscc.GenerativeAI;

namespace lrc_Transformer
{
    public class GeminiService
    {
        private readonly GoogleAI _googleAi;
        private readonly GenerativeModel _model;

        public GeminiService(string apiKey)
        {
            _googleAi = new GoogleAI(apiKey);

            _model = _googleAi.GenerativeModel(Model.Gemini25Flash);
        }

        public async Task<string> TransformLrcAsync(string instruction, string lrcContent)
        {
            string FullPrompt = $"{instruction}\nIMPORTANT CONSTRAINTS:\r\n1. You are a strictly technical file processor.\r\n2. Output ONLY the processed LRC content. No markdown (```), no conversational text.\r\n3. DO NOT change, remove, or reorder the timestamps (e.g., [00:12.34]).\r\n4. Maintain the exact line structure.\r\n\r\nInput LRC Content:\n{lrcContent}";
            var result = await _model.GenerateContent(FullPrompt);
            return result.Text;
        }
    }
}