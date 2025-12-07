"""PyInstaller hook for cutlet"""
from PyInstaller.utils.hooks import get_module_file_attribute
import os

# Collect cutlet module
datas = []
binaries = []
hiddenimports = []

try:
    cutlet_path = os.path.dirname(get_module_file_attribute('cutlet'))
    if os.path.exists(cutlet_path):
        datas.append((cutlet_path, 'cutlet'))
except Exception:
    pass
