"""PyInstaller hook for fugashi"""
from PyInstaller.utils.hooks import get_module_file_attribute
import os

# Collect fugashi module
datas = []
binaries = []
hiddenimports = []

try:
    fugashi_path = os.path.dirname(get_module_file_attribute('fugashi'))
    if os.path.exists(fugashi_path):
        datas.append((fugashi_path, 'fugashi'))
except Exception:
    pass
