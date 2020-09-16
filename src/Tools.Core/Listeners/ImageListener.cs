﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Xobject;
using Tools.Core.Models;

namespace Tools.Core.Listeners
{
    public class ImageListener : FilteredEventListener
    {
        private readonly SortedDictionary<float, IChunk> _chunkDictionary;
        private readonly Func<float> _increaseCounter;

        public ImageListener(SortedDictionary<float, IChunk> chunkDictionary, Func<float> increaseCounter)
        {
            this._chunkDictionary = chunkDictionary;
            this._increaseCounter = increaseCounter;
        }

        public override void EventOccurred(IEventData data, EventType type)
        {
            if (type != EventType.RENDER_IMAGE) return;

            float counter = _increaseCounter();

            var renderInfo = (ImageRenderInfo)data;

            var imageObject = renderInfo.GetImage();
            Bitmap image;

            try
            {
                var imageBytes = imageObject.GetImageBytes();
                image = new Bitmap(new MemoryStream(imageBytes));
            }
            catch
            {
                return;
            }

            var smask = imageObject.GetPdfObject().GetAsStream(PdfName.SMask);

            if (smask != null)
            {
                try
                {
                    var maskImageObject = new PdfImageXObject(smask);
                    var maskBytes = maskImageObject.GetImageBytes();
                    using (var maskImage = new Bitmap(new MemoryStream(maskBytes)))
                    {
                        image = GenerateMaskedImage(image, maskImage);
                    }
                }
                catch
                {
                }
            }

            var matix = renderInfo.GetImageCtm();

            var imageChunk = new ImageChunk
            {
                X = matix.Get(iText.Kernel.Geom.Matrix.I31),
                Y = matix.Get(iText.Kernel.Geom.Matrix.I32),
                W = matix.Get(iText.Kernel.Geom.Matrix.I11),
                H = matix.Get(iText.Kernel.Geom.Matrix.I22),
                Image = image
            };

            _chunkDictionary.Add(counter, imageChunk);

            base.EventOccurred(data, type);
        }

        private Bitmap GenerateMaskedImage(Bitmap image, Bitmap mask)
        {
            var output = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
            var rect = new Rectangle(0, 0, image.Width, image.Height);
            var bitsMask = mask.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bitsInput = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bitsOutput = output.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                for (int y = 0; y < image.Height; y++)
                {
                    byte* ptrMask = (byte*)bitsMask.Scan0 + y * bitsMask.Stride;
                    byte* ptrInput = (byte*)bitsInput.Scan0 + y * bitsInput.Stride;
                    byte* ptrOutput = (byte*)bitsOutput.Scan0 + y * bitsOutput.Stride;
                    for (int x = 0; x < image.Width; x++)
                    {
                        ptrOutput[4 * x] = ptrInput[4 * x];           // blue
                        ptrOutput[4 * x + 1] = ptrInput[4 * x + 1];   // green
                        ptrOutput[4 * x + 2] = ptrInput[4 * x + 2];   // red
                        ptrOutput[4 * x + 3] = ptrMask[4 * x];        // alpha
                    }
                }
            }

            mask.UnlockBits(bitsMask);
            image.UnlockBits(bitsInput);
            output.UnlockBits(bitsOutput);

            return output;
        }
    }
}
