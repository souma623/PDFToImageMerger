using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("PDFファイルのパスをカンマ区切りで入力してください:");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("入力がありません。終了します。");
            return;
        }
        var pdfFiles = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine("画像の出力先フォルダを入力してください:");
        string? outputDir = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputDir))
        {
            Console.WriteLine("出力先がありません。終了します。");
            return;
        }
        if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);
        foreach (var pdf in pdfFiles)
        {
            string path = pdf.Trim();
            if (File.Exists(path))
            {
                Console.WriteLine($"処理中: {path}");
                PDFProcessor.ConvertPdfToA4Images(path, outputDir);
            }
            else
            {
                Console.WriteLine($"ファイルが見つかりません: {path}");
            }
        }
        Console.WriteLine("完了しました。");
    }
}
