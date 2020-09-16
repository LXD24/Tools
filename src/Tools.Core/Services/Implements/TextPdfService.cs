using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iText.IO.Image;
using iText.IO.Source;
using iText.Kernel.Crypto.Securityhandler;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Filespec;
using iText.Layout;
using iText.Layout.Properties;
using Tools.Core.Extensions;
using Tools.Core.Listeners;
using Tools.Core.Models;
using Tools.Core.Services.Abstractions;
using Path = System.IO.Path;

namespace Tools.Core.Services.Implements
{
    /// <summary>
    /// PDF服务 IText7实现类
    /// </summary>
    public class TextPdfService : IPdfService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageSourceList"></param>
        /// <param name="outPath"></param>
        /// <returns></returns>
        public async Task ConvertImages2PdfAsync(IEnumerable<string> imageSourceList, string outPath)
        {
            await Task.Run(() =>
            {
                //var pdfWriter = new PdfWriter(outPath,
                //    new WriterProperties().SetStandardEncryption(user, owner,
                //        EncryptionConstants.ALLOW_PRINTING | EncryptionConstants.ALLOW_ASSEMBLY,
                //        EncryptionConstants.ENCRYPTION_AES_256));

                var pdfWriter = new PdfWriter(outPath);
                var pdfDocument = new PdfDocument(pdfWriter);
                var document = new Document(pdfDocument);

                foreach (var imageSource in imageSourceList)
                {
                    var imageData = ImageDataFactory.Create(imageSource);
                    var image = new iText.Layout.Element.Image(imageData);
                    var a4Width = PageSize.A4.GetWidth();
                    var a4Height = PageSize.A4.GetHeight();

                    if (image.GetImageHeight() > a4Height - 25)
                    {
                        image.ScaleToFit(a4Width - 25, a4Height - 25);
                    }
                    else if (image.GetImageWidth() > a4Width - 25)
                    {
                        image.ScaleToFit(a4Width - 25, a4Height - 25);
                    }

                    image.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                    document.Add(image);
                }
                document.Close();
            });
        }

        

        /// <summary>
        /// 从PDF中提取图片
        /// </summary>
        /// <param name="pdfSource"></param>
        /// <returns></returns>
        public async Task<Bitmap[]> ExtractBitmapsFromPdfAsync(string pdfSource)
        {
            return await Task.Run(() =>
            {
                var pdf = File.Open(pdfSource, FileMode.Open);
                var reader = new PdfReader(pdf);
                var pdfDocument = new PdfDocument(reader);
                var bitmaps = ConvertToBitmaps(pdfDocument);
                return bitmaps.ToArray();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task EncryptAsync(string pdfSource,string newPdfSavePath, string passWord)
        {
            await Task.Run(() =>
            {
                var reader = new PdfReader(pdfSource);
                using (var os = new FileStream(newPdfSavePath, FileMode.Create))
                {
                    var userPassword = Encoding.Default.GetBytes(passWord);
                    var ownerPassword = Encoding.Default.GetBytes(passWord);
                    var permissions =  EncryptionConstants.ALLOW_PRINTING | EncryptionConstants.ALLOW_ASSEMBLY;
                    PdfEncryptor.Encrypt(reader, os,
                        new EncryptionProperties().SetStandardEncryption(userPassword, ownerPassword, permissions,
                            EncryptionConstants.ENCRYPTION_AES_256));
                }
            });
        }



        #region 私有
        private static int _counter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfDocument"></param>
        /// <returns></returns>
        private IEnumerable<Bitmap> ConvertToBitmaps(PdfDocument pdfDocument)
        {
            _counter = 0;

            var numberOfPages = pdfDocument.GetNumberOfPages();

            for (var i = 1; i <= numberOfPages; i++)
            {
                var currentPage = pdfDocument.GetPage(i);

                yield return ConvertToBitmap(currentPage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfDocument"></param>
        /// <returns></returns>
        private IEnumerable<Stream> ConvertToJpgStreams(PdfDocument pdfDocument)
        {
            foreach (var bmp in ConvertToBitmaps(pdfDocument))
            {
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                    yield return ms;
                }

                bmp.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfPage"></param>
        /// <returns></returns>
        private Bitmap ConvertToBitmap(PdfPage pdfPage)
        {
            var rotation = pdfPage.GetRotation();

            var chunkDictionairy = new SortedDictionary<float, IChunk>();

            FilteredEventListener listener = new FilteredEventListener();
            listener.AttachEventListener(new TextListener(chunkDictionairy, _increaseCounter));
            listener.AttachEventListener(new ImageListener(chunkDictionairy, _increaseCounter));
            PdfCanvasProcessor processor = new PdfCanvasProcessor(listener);
            processor.ProcessPageContent(pdfPage);

            //var size = currentPage.GetPageSizeWithRotation();
            var size = pdfPage.GetPageSize();

            var width = size.GetWidth().PointsToPixels();
            var height = size.GetHeight().PointsToPixels();

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                foreach (var chunk in chunkDictionairy)
                {
                    g.ResetTransform();

                    g.RotateTransform(-rotation);

                    if (chunk.Value is ImageChunk imageChunk)
                    {
                        var imgW = imageChunk.W.PointsToPixels();
                        var imgH = imageChunk.H.PointsToPixels();
                        var imgX = imageChunk.X.PointsToPixels();
                        var imgY = (size.GetHeight() - imageChunk.Y - imageChunk.H).PointsToPixels();

                        g.TranslateTransform(imgX, imgY, MatrixOrder.Append);
                        g.DrawImage(imageChunk.Image, 0, 0, imgW, imgH);
                        imageChunk.Image.Dispose();
                    }
                    else if (chunk.Value is Models.TextChunk textChunk)
                    {
                        var chunkX = textChunk.Rect.GetX().PointsToPixels();
                        var chunkY = bmp.Height - textChunk.Rect.GetY().PointsToPixels();

                        var fontSize = textChunk.FontSize.PointsToPixels();

                        Font font;
                        try
                        {
                            font = new Font(textChunk.FontFamily, fontSize, textChunk.FontStyle, GraphicsUnit.Pixel);
                        }
                        catch (Exception ex)
                        {
                            //log error

                            font = new Font("Calibri", 11, textChunk.FontStyle, GraphicsUnit.Pixel);
                        }

                        g.TranslateTransform(chunkX, chunkY, MatrixOrder.Append);

                        //g.DrawString(textChunk.Text, font, new SolidBrush(textChunk.Color), chunkX, chunkY);
                        g.DrawString(textChunk.Text, font, new SolidBrush(textChunk.Color), 0, 0);
                    }
                }

                g.Flush();
            }

            return bmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfPage"></param>
        /// <returns></returns>
        public Stream ConvertToJpgStream(PdfPage pdfPage)
        {
            var bmp = ConvertToBitmap(pdfPage);
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                bmp.Dispose();
                return ms;
            }
        }

        private readonly Func<float> _increaseCounter = () => _counter = Interlocked.Increment(ref _counter);
        #endregion
    }
}
