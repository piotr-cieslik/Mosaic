using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mosaic.ColorSpaces;

namespace Mosaic.Tests.ColorSpaces
{
    [TestClass]
    public class HsvToRgbConverterTests
    {
        [TestMethod]
        public void ShouldConvertHsvToRgb()
        {
            for (int r = 0; r < 256; r++)
                for (int g = 0; g < 256; g++)
                    for (int b = 0; b < 256; b++)
                    {
                        var rgb = new Rgb((byte)r, (byte)g, (byte)b);
                        var hsv = rgb.ToHsv();
                        var rgb2 = hsv.ToRgb();
                        Assert.AreEqual(rgb, rgb2);
                    }
        }
    }
}