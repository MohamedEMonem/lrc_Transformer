import cutlet
import fugashi
import unidic_lite
import re
import sys

class LyricConverter:
    def __init__(self):
        # 1. Setup Dictionary
        dic_dir = unidic_lite.DICDIR
        self.katsu = cutlet.Cutlet('hepburn')
        self.katsu.use_foreign_spelling = False
        
        # Force the tagger to use the correct dictionary
        try:
            self.katsu.tagger = fugashi.Tagger(f'-d "{dic_dir}"')
        except Exception:
            pass # Fallback to default if this fails

        # 2. Define Overrides
        self.overrides = {
            "私": "Watashi", "明日": "Ashita", "昨日": "Kinou", 
            "昨夜": "Yuube", "永遠": "Towa", "永久": "Towa", 
            "運命": "Sadame", "生命": "Inochi", "地球": "Hoshi", 
            "宇宙": "Sora", "奇跡": "Kiseki", "抱いて": "Daite", 
            "逢って": "Atte", "理由": "Wake", "本気": "Maji", 
            "瞬間": "Toki", "未来": "Mirai", "七色": "Nanairo", 
            "眼鏡": "Megane",
            "へ": "e", "を": "o", "君": "Kimi", 
            "一人": "Hitori", "ひとり": "Hitori"
        }

        self.phrase_fixes = {
            "hikari no kan": "hikari no wa", 
            "ana ga hiraita": "ana ga aita",
            "hitorikun": "hitori kimi", 
        }

    def _post_process(self, text):
        for wrong, right in self.phrase_fixes.items():
            text = re.sub(re.escape(wrong), right, text, flags=re.IGNORECASE)
        text = re.sub(r'\bwo\b', 'o', text, flags=re.IGNORECASE)
        text = text.replace("ō", "ou").replace("ū", "uu")
        return text[0].upper() + text[1:] if text else text

    def convert(self, text):
        text = text.strip()
        if not text: return ""
        if not re.search(r'[\u3040-\u30ff\u4e00-\u9faf]', text): return text

        try:
            # --- CRITICAL FIX: SAFETY WRAPPER ---
            # We wrap the tokenization in a try-block. 
            # If cutlet crashes (AttributeError), we catch it and use the simple method.
            tokens = self.katsu.romaji_tokens(text)
            romaji_list = []

            for token in tokens:
                # Handle both Objects and Strings safely
                word = getattr(token, 'surface', str(token))
                romaji = getattr(token, 'romaji', str(token))

                if word in self.overrides:
                    romaji = self.overrides[word]

                if romaji:
                    romaji_list.append(romaji)
            
            return self._post_process(" ".join(romaji_list))

        except Exception as e:
            # FALLBACK: If the advanced logic crashes, just do basic conversion
            # This prevents the app from stopping.
            return self._post_process(self.katsu.romaji(text))

def process_lrc_file(input_path, output_path):
    sys.stdout.reconfigure(encoding='utf-8')
    print(f"Processing: {input_path}")
    
    try:
        converter = LyricConverter()
        with open(input_path, 'r', encoding='utf-8') as f:
            lines = f.readlines()
        
        converted_lines = []
        lrc_pattern = re.compile(r'^((?:\[.*?\])+)(.*)$')

        for line in lines:
            line = line.strip()
            match = lrc_pattern.match(line)
            if match:
                timestamps, lyrics = match.groups()
                if lyrics.strip():
                    romaji = converter.convert(lyrics)
                    converted_lines.append(f"{timestamps} {romaji}")
                else:
                    converted_lines.append(line)
            else:
                converted_lines.append(line if line.startswith("[") else converter.convert(line))

        with open(output_path, 'w', encoding='utf-8') as f:
            f.write('\n'.join(converted_lines))
        print(f"Success! Saved to: {output_path}")
        
    except Exception as e:
        print(f"Error: {e}")
        sys.exit(1)

if __name__ == "__main__":
    if len(sys.argv) < 3:
        print("Usage: python main.py <input> <output>")
        sys.exit(1)
    process_lrc_file(sys.argv[1], sys.argv[2])
    print("DONE_SUCCESS")