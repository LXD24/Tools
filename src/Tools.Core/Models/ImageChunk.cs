﻿using System.Drawing;

namespace Tools.Core.Models
{
    public class ImageChunk : IChunk
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float W { get; set; }
        public float H { get; set; }
        public Image Image { get; set; }
    }
}
