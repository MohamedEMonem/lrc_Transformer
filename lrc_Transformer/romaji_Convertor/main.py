import cutlet
import fugashi
import unidic_lite
import re
import sys

class LyricConverter:
    def __init__(self):
        # Initialize Cutlet
        # Note: We rely on Cutlet's default tagger setup to avoid path issues
        self.katsu = cutlet.Cutlet('hepburn')
        self.katsu.use_foreign_spelling = False
        
        # --- DICTIONARY OVERRIDES ---
        # Combined song-specific readings AND grammar fixes into one fast lookup
        self.overrides = {
            # Song Specifics
            "私": "Watashi", "明日": "Ashita", "昨日": "Kinou", 
            "昨夜": "Yuube", "永遠": "Towa", "永久": "Towa", 
            "運命": "Sadame", "生命": "Inochi", "地球": "Hoshi", 
            "宇宙": "Sora", "奇跡": "Kiseki", "抱いて": "Daite", 
            "逢って": "Atte", "理由": "Wake", "本気": "Maji", 
            "瞬間": "Toki", "未来": "Mirai", "七色": "Nanairo", 
            "眼鏡": "Megane",
            # Particles & Grammar Rules
            "へ": "e", 
            "を": "o", 
            "君": "Kimi", 
            "一人": "Hitori", 
            "ひとり": "Hitori"
        }

        # --- PHRASE CORRECTIONS ---
        # Fixes context-based errors that happen after tokenization
        self.phrase_fixes = {
            "hikari no kan": "hikari no wa", 
            "ana ga hiraita": "ana ga aita",
            "hitorikun": "hitori kimi",  # Fixes the parsing error from your previous file
        }

    def _post_process(self, text):
        """Final text cleanup"""
        # 1. Apply Phrase Fixes (Case-insensitive)
        for wrong, right in self.phrase_fixes.items():
            text = re.sub(re.escape(wrong), right, text, flags=re.IGNORECASE)

        # 2. Safety: Ensure 'wo' particle is always 'o'
        text = re.sub(r'\bwo\b', 'o', text, flags=re.IGNORECASE)
        
        # 3. Long Vowels (Karaoke Style: ō -> ou)
        text = text.replace("ō", "ou").replace("ū", "uu")
        
        # 4. Capitalize first letter
        return text[0].upper() + text[1:] if text else text

    def convert(self, text):
        text = text.strip()
        if not text: return ""

        # 1. Skip non-Japanese lines
        if not re.search(r'[\u3040-\u30ff\u4e00-\u9faf]', text):
            return text

        try:
            # 2. Tokenize
            tokens = self.katsu.romaji_tokens(text)
            romaji_list = []

            for token in tokens:
                # Optimized safety check:
                # getattr gets 'surface' if it exists, otherwise converts the token to string
                word = getattr(token, 'surface', str(token))
                romaji = getattr(token, 'romaji', str(token))

                # Apply Override if exists
                if word in self.overrides:
                    romaji = self.overrides[word]

                if romaji:
                    romaji_list.append(romaji)
            
            return self._post_process(" ".join(romaji_list))

        except Exception:
            # Fallback: prevents crashes on rare edge cases
            return self.katsu.romaji(text)

# --- FILE PROCESSING ---

def process_lrc_file(input_path, output_path):
    # Fix encoding for Windows Console
    sys.stdout.reconfigure(encoding='utf-8')
    
    print(f"Processing: {input_path}")
    converter = LyricConverter()
    
    try:
        with open(input_path, 'r', encoding='utf-8') as f:
            lines = f.readlines()
        
        converted_lines = []
        lrc_pattern = re.compile(r'^((?:\[.*?\])+)(.*)$')

        for line in lines:
            line = line.strip()
            match = lrc_pattern.match(line)
            
            if match:
                timestamps = match.group(1)
                lyrics = match.group(2)
                
                if lyrics.strip():
                    romaji = converter.convert(lyrics)
                    converted_lines.append(f"{timestamps} {romaji}")
                else:
                    converted_lines.append(line)
            else:
                # Handle Metadata or plain text
                if line.startswith("["):
                    converted_lines.append(line)
                else:
                    converted_lines.append(converter.convert(line))

        with open(output_path, 'w', encoding='utf-8') as f:
            f.write('\n'.join(converted_lines))
            
        print(f"Success! Saved to: {output_path}")
        
    except Exception as e:
        print(f"Error: {e}")
        import traceback
        traceback.print_exc()
        sys.exit(1)

if __name__ == "__main__":
    if len(sys.argv) < 3:
        print("Usage: python main.py <input> <output>")
        sys.exit(1)

    process_lrc_file(sys.argv[1], sys.argv[2])
    print("DONE_SUCCESS")