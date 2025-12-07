"""PyInstaller hook for unidic_lite"""
from PyInstaller.utils.hooks import get_module_file_attribute
import os

# Collect unidic_lite module and data
datas = []
binaries = []
hiddenimports = []

try:
    unidic_path = os.path.dirname(get_module_file_attribute('unidic_lite'))
    if os.path.exists(unidic_path):
        datas.append((unidic_path, 'unidic_lite'))
        # Also collect the dictionary data if it exists
        dicdir = os.path.join(unidic_path, 'dicdir')
        if os.path.exists(dicdir):
            datas.append((dicdir, 'unidic_lite/dicdir'))
except Exception:
    pass
