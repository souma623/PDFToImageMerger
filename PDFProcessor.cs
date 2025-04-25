using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using PdfiumViewer;

public class PDFProcessor
{
    // PDFファイルを5ページごとにA4画像へ合成し保存
    public static void ConvertPdfToA4Images(string pdfPath, string outputDir)
    {
        using (var document = PdfDocument.Load(pdfPath))
        {
            int pageCount = document.PageCount;
            int groupSize = 5;
            int groupIndex = 0;
            for (int i = 0; i < pageCount; i += groupSize)
            {
                var images = new List<System.Drawing.Image>();
                for (int j = 0; j < groupSize && (i + j) < pageCount; j++)
                {
                    var img = document.Render(i + j, 2480, 3508, 96, 96, true); // A4@300dpi
                    images.Add(img);
                }
                var merged = MergeImagesA4(images, i / groupSize + 1);
                string outPath = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(pdfPath)}_group{groupIndex + 1}.png");
                merged.Save(outPath);
                foreach (var img in images) img.Dispose();
                merged.Dispose();
                groupIndex++;
            }
        }
    }

    // 4枚の画像をA4サイズ1枚にバランスよく配置（2段×2列グリッド）
    private static System.Drawing.Bitmap MergeImagesA4(List<System.Drawing.Image> images, int startPageNumber = 1)
    {
        int a4Width = 2480;  // A4@300dpi
        int a4Height = 3508; // A4@300dpi
        int gridRows = 2;
        int gridCols = 2;
        int cellWidth = a4Width / gridCols;
        int cellHeight = a4Height / gridRows;
        var merged = new System.Drawing.Bitmap(a4Width, a4Height);
        using (var g = System.Drawing.Graphics.FromImage(merged))
        {
            g.Clear(System.Drawing.Color.White);
            for (int i = 0; i < images.Count && i < 4; i++)
            {
                int col = i % gridCols;
                int row = i / gridCols;
                var img = images[i];
                float scale = Math.Min((float)cellWidth / img.Width, (float)cellHeight / img.Height);
                int drawW = (int)(img.Width * scale);
                int drawH = (int)(img.Height * scale);
                int offsetX = col * cellWidth + (cellWidth - drawW) / 2;
                int offsetY = row * cellHeight + (cellHeight - drawH) / 2;
                g.DrawImage(img, offsetX, offsetY, drawW, drawH);
                // 各画像の右下にページ番号を描画
                string pageText = $"{startPageNumber + i}";
                using (var font = new System.Drawing.Font("Arial", 48, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel))
                using (var sf = new System.Drawing.StringFormat())
                {
                    sf.Alignment = System.Drawing.StringAlignment.Far;
                    sf.LineAlignment = System.Drawing.StringAlignment.Far;
                    var margin = 20;
                    var rect = new System.Drawing.RectangleF(offsetX, offsetY + drawH - 60, drawW - margin, 60);
                    g.DrawString(pageText, font, System.Drawing.Brushes.Black, rect, sf);
                }
            }
        }
        return merged;
    }
}
