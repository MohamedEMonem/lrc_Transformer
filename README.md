🎵 Lrc_Transformer

Lrc_Transformer is a Windows desktop application built with .NET 9 that uses the Gemini AI API to transform and edit .lrc lyric files while strictly preserving their timestamp integrity.

Its primary use case is automatically transliterating Japanese lyrics into Romaji, but thanks to customizable system prompts, it can be used for translation, summarization, or any text-based transformation of lyric files.
✨ Key Features:

   - Timestamp Safe: Guaranteed preservation of [mm:ss.xx] tags. The AI is strictly instructed to modify only the lyric text.

   - AI-Powered Transliteration: Uses Google's Gemini 2.5 Flash model for high-speed, accurate Japanese-to-Romaji conversion.

   - Customizable Prompts: The default mode handles Romaji, but you can edit the system prompt to perform translations (e.g., JP -> EN) or style changes.

   - Smart Fallback: If you leave the prompt empty, it automatically applies the default "Japanese to Romaji" instruction.

   - File Management: Option to "Replace Original File" (with automatic .bak backup creation) or save to a new custom location.

   - Persistent Settings: Remembers your custom system prompts between sessions.

🚀 How to Run

   - Get an API Key: Obtain a free API key from Google AI Studio.

   - Set Environment Variable:

       - Add a User Environment Variable named GEMINI_API_KEY with your key value.

       - (Alternatively, you can hardcode it in Form1.cs for local testing).

   - Launch the App: Open the .exe and you are ready to go.

🛠️ Usage

   - Select Input: Click "Browse" to choose your .lrc file.

   - Choose Output: Check "Replace Original File" to overwrite (creates a backup), or choose a new save location.

   - Customize Prompt (Optional): The text box is pre-filled with Romaji instructions. You can change this to "Translate to English" or any other command.

   - Transform: Click the TRANSFORM button. The progress bar will indicate activity, and a success message will appear when done.

💻 Tech Stack

   - Framework: .NET 9.0

   - GUI: Windows Forms (WinForms)

   - AI Model: Google Gemini 2.5 Flash

   - Library: Mscc.GenerativeAI

📄 License

This project is open source and available under the MIT License.
