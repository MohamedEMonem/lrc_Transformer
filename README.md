# 🎵 LRC Transformer

A powerful Windows desktop application that transforms and enhances `.lrc` lyric files while preserving timestamp integrity. Whether you need to transliterate Japanese lyrics to Romaji, translate to other languages, or apply any custom text transformation, LRC Transformer has you covered.

## ✨ Features

- **Timestamp Preservation**: Guarantees [mm:ss.xx] timestamps remain untouched during transformations
- **Dual Processing Modes**:
  - **Offline Mode**: Fast, local Japanese-to-Romaji conversion using Cutlet and Fugashi libraries
  - **Gemini API Mode**: Advanced AI-powered transformations for translation, transliteration, and custom text editing
- **Flexible Prompts**: Customize transformations with your own system prompts for any text operation
- **Smart Defaults**: Pre-configured for Japanese Romaji, automatically applies default prompt if left empty
- **File Safety**: Automatic backup creation when replacing original files
- **Settings Persistence**: Remembers your custom prompts between sessions

## 🚀 Getting Started

### Prerequisites
- Windows 10 or later
- .NET 9.0 Runtime

### Setup (Gemini API Mode)

1. **Obtain API Key**: Get a free API key from [Google AI Studio](https://aistudio.google.com)

2. **Configure Environment Variable**:
   - Press `Win + X` → System → Advanced system settings → Environment Variables
   - Create a new User Variable: `GEMINI_API_KEY` = `your-api-key-here`
   - Restart the application for changes to take effect

3. **Launch**: Run the application and select "Gemini API" mode

### Setup (Offline Mode)

1. **Python Environment** (First time only):
   ```powershell
   cd romaji_Convertor
   python -m venv .venv
   .\.venv\Scripts\Activate.ps1
   pip install cutlet fugashi unidic-lite
   ```

2. **Launch**: Run the application and select "Offline" mode

## 📖 Usage

1. **Select Input File**: Click "Browse" and choose your `.lrc` file
2. **Choose Output Location**: 
   - Enable "Replace Original File" to overwrite (creates automatic `.bak` backup)
   - Or specify a custom output path
3. **Configure Transformation** (Optional):
   - Customize the system prompt for your needs
   - Default: "Transliterate Japanese to Romaji. Keep timestamps."
   - Examples: "Translate to English", "Fix grammar", etc.
4. **Transform**: Click "Process" and wait for completion

## 💻 Technical Stack

| Component | Technology |
|-----------|-----------|
| **Framework** | .NET 9.0 |
| **UI** | Windows Forms |
| **AI Model** | Google Gemini 2.5 Flash (API mode) |
| **Romaji Engine** | Cutlet + Fugashi (Offline mode) |
| **API Library** | Mscc.GenerativeAI |

## 📁 Project Structure

```
lrc_Transformer/
├── Form1.cs                    # Main UI and orchestration
├── GeminiService.cs            # Gemini API integration
├── PythonRomajiService.cs      # Python script runner
├── romaji_Convertor/
│   ├── main.py                 # Offline Romaji conversion
│   ├── requirements.txt        # Python dependencies
│   └── .venv/                  # Virtual environment (auto-created)
└── Properties/                 # Settings storage
```

## 🔧 Advanced Configuration

### Custom Prompts
Edit the system prompt text box to perform different transformations:
- `"Translate to English"` - JP → EN translation
- `"Fix romanization grammar"` - Grammar corrections
- `"Summarize lyrics"` - Extract key themes

### API Key Alternatives
For testing without environment variables, you can temporarily set the key in code:
```csharp
string key = "your-key-here"; // Not recommended for production
```

## 📝 License

MIT License - See LICENSE file for details

## 🐛 Troubleshooting

| Issue | Solution |
|-------|----------|
| "Python Failed" error | Ensure Python venv is set up in `romaji_Convertor/.venv` |
| API key not recognized | Verify `GEMINI_API_KEY` environment variable is set and app is restarted |
| Timestamps modified | This shouldn't happen - timestamps are protected by design |
| File permissions error | Ensure you have write permissions for the output directory |

## 📧 Support

For issues, suggestions, or contributions, please open an issue or submit a pull request.
