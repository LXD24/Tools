using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Drawing;
using iText.IO.Font;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Tools.Core.Models;
using TextChunk = Tools.Core.Models.TextChunk;

namespace Tools.Core.Listeners
{
    public class TextListener : LocationTextExtractionStrategy
    {
        private readonly SortedDictionary<float, IChunk> _chunkDictionary;
        private readonly Func<float> _increaseCounter;

        public TextListener(SortedDictionary<float, IChunk> chunkDictionary, Func<float> increaseCounter)
        {
            this._chunkDictionary = chunkDictionary;
            this._increaseCounter = increaseCounter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fontNames"></param>
        /// <returns></returns>
        FontStyle GetFontStyle(FontNames fontNames)
        {
            var fontname = fontNames.GetFontName();
            var fontStyleRegex = Regex.Match(fontname, @"[-,][\w\s]+$");

            if (fontStyleRegex.Success)
            {
                var result = fontStyleRegex.Value.ToLower();
                if (result.Contains("bold"))
                {
                    return FontStyle.Bold;
                }
            }

            return FontStyle.Regular;
        }

        public override void EventOccurred(IEventData data, EventType type)
        {
            if (!type.Equals(EventType.RENDER_TEXT)) return;

            TextRenderInfo renderInfo = (TextRenderInfo)data;

            float counter = _increaseCounter();

            var font = renderInfo.GetFont().GetFontProgram();
            var originalFontName = font.ToString();
            var fontRegex = Regex.Match(originalFontName, @"(?<=\+)[a-zA-Z\s]+");

            string fontName = fontRegex.Success ? fontRegex.Value : originalFontName;

            var fontStyle = GetFontStyle(font.GetFontNames());

            float curFontSize = renderInfo.GetFontSize();

            float key = counter;

            IList<TextRenderInfo> text = renderInfo.GetCharacterRenderInfos();
            foreach (TextRenderInfo character in text)
            {
                key += 0.001f;

                var textRenderMode = character.GetTextRenderMode();
                var opacity = character.GetGraphicsState().GetFillOpacity();

                //if (textRenderMode != 0 || opacity != 1)
                //{

                //}

                string letter = character.GetText();

                Color color;

                var fillColor = character.GetFillColor();
                var colors = fillColor.GetColorValue();
                if (colors.Length == 1)
                {
                    color = Color.FromArgb((int)(255 * (1 - colors[0])), Color.Black);
                }
                else if (colors.Length == 3)
                {
                    color = Color.FromArgb((int)(255 * colors[0]), (int)(255 * colors[1]), (int)(255 * colors[2]));
                }
                else if (colors.Length == 4)
                {
                    color = Color.FromArgb((int)(255 * colors[0]), (int)(255 * colors[1]), (int)(255 * colors[2]), (int)(255 * colors[3]));
                }
                else
                {
                    color = Color.Black;
                }

                //if(letter == "A")
                //{

                //}

                if (string.IsNullOrWhiteSpace(letter)) continue;

                //Get the bounding box for the chunk of text
                var bottomLeft = character.GetDescentLine().GetStartPoint();
                var topRight = character.GetAscentLine().GetEndPoint();

                //Create a rectangle from it
                var rect = new iText.Kernel.Geom.Rectangle(
                                                        bottomLeft.Get(Vector.I1),
                                                        topRight.Get(Vector.I2),
                                                        topRight.Get(Vector.I1),
                                                        topRight.Get(Vector.I2)
                                                        );

                var currentChunk = new TextChunk()
                {
                    Text = letter,
                    Rect = rect,
                    FontFamily = fontName,
                    FontSize = (int)curFontSize,
                    FontStyle = fontStyle,
                    Color = color,
                    SpaceWidth = character.GetSingleSpaceWidth() / 2f
                };

                _chunkDictionary.Add(key, currentChunk);
            }

            base.EventOccurred(data, type);
        }
    }
}
