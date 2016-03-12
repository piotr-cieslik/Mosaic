﻿using System.Collections.Generic;
using System.Drawing;

namespace mosaic.Directories
{
    internal interface ITemporaryDirectory
    {
        IReadOnlyCollection<TemporaryImage> Get();

        void Save(Image image, string name, float averageHsvValue);
    }
}