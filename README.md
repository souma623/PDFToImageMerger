# PDFToImageMerger

## 概要

複数PDFファイルを4ページごとにA4サイズ1枚の画像（PNG）として合成・保存するC#コンソールアプリケーションです。

## 使い方

### 1. ビルド

```
dotnet build
```

### 2. 実行

```
dotnet run --project PDFToImageMerger
```

### 3. 操作手順

- PDFファイルのフルパスをカンマ区切りで入力（例: `C:\Users\gogom\Downloads\sample.pdf`）
- 画像の出力先フォルダのフルパスを入力（例: `C:\Users\gogom\OneDrive\デスクトップ\output`）
- 4ページごとにA4画像1枚として出力され、各サブ画像右下に元のページ番号が入ります

## 依存DLLについて

- 本アプリは [PdfiumViewer](https://github.com/pvginkel/PdfiumViewer) を利用しています。
- 実行には [PdfiumBuild (pdfium.dll)](https://github.com/pvginkel/PdfiumBuild/releases) が必要です。
- pdfium.dllはライセンス（BSD-3-Clause）上、リポジトリには含めていません。各自でダウンロードし、`PDFToImageMerger/bin/Debug/net9.0/` など実行ファイルと同じフォルダに配置してください。

## ライセンス

- コード: MIT
- pdfium.dll: BSD-3-Clause（[LICENSE](https://github.com/pvginkel/PdfiumBuild/blob/master/LICENSE)）

## 注意事項

- Windows専用です。
- .NET 8/9で動作確認済み。
- PDFはA4サイズでレンダリングされます。
- 4ページ未満のグループも1枚の画像になります。

---

## GitHubへのアップロード手順

1. プロジェクトルートで以下を実行

```
git init
git add .
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/souma623/PDFToImageMerger.git
git push -u origin main
```

---

## 参考

- [PdfiumViewer](https://github.com/pvginkel/PdfiumViewer)
- [PdfiumBuild](https://github.com/pvginkel/PdfiumBuild)
