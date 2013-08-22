﻿using System.Drawing;

namespace tw.patw.ImageResizer
{
    public interface IDecorator
    {
        Image Operation();
        void Save(string file, string path);
        void Output();
    }
}
